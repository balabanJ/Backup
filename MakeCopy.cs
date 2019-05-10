using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
/* 
 * Пространство имен System.IO.Compression содержит классы, предоставляющие основные службы сжатия и распаковки для потоков.
 */
using System.IO.Compression;
//using System.Data.SQLite;

namespace Backup
{
    class MakeCopy
    {
        static bool ZipSource = false;
        static bool ZipTarget = false;
        static ZipArchive zip;

        /*
         * Скопировать каталог с заданным именем в место, определяемое root
         * 
         * Определить имя файла - назначения для выбранного файла - исходного
         * 
         * Имя выходного файла формируется следующим образом:
         * Z:\Example\File.txt --> Backup\Version\Z\Example\File.txt
         * Backup\Version\Z\Example\File.txt --> Z:\Example\File.txt
         * 
         * Копирование файла
         */

        // Преобразовать Windows-путь в ZIP-путь
        static string ToZIPpath(string path)
        {
            string result = path.Substring(3).Replace(@"\", "/");
            return result;
        }

        // Оба файла не являются частью архива
        static bool CopyFile(string src, string dst)
        {
            Stream srcs;
            Stream dsts;

            // Определиться с srcs
            if (ZipSource)
                srcs = zip.GetEntry(ToZIPpath(src)).Open();
            else
                srcs = new FileStream(src, FileMode.Open);

            if (ZipTarget)
                dsts = zip.CreateEntry(ToZIPpath(dst)).Open();
            else
            {
                if (!Directory.Exists(Path.GetDirectoryName(dst)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dst));
                dsts = new FileStream(dst, FileMode.Create);
            }

            if (!ZipTarget)
                if (!Directory.Exists(Path.GetDirectoryName(dst)))
                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(dst));
                    }
                    catch
                    {
                        MessageBox.Show(dst, "CopyFile: не могу создать каталог назначения");
                        return false;
                    };

            try
            {
                srcs.CopyTo(dsts);
                srcs.Close();
                dsts.Close();
                return true;
            }
            catch
            {
                MessageBox.Show(src + "->" + dst, "CopyFile: не могу скопировать");
                return false;
            }
        }

        // Копирование каталога с подкаталогами
        static bool CopyDir(string src, string dst)
        { 
            if (ZipSource)
            {
                // Получить список всех файлов из ZIP, которые находятся "внутри" src
                // (в зависимости от их сорта CopyDir или CopyFile)
                src = ToZIPpath(src) + "/";
                foreach (var z in zip.Entries)
                {
                    if (z.ToString().IndexOf(src) >=0)
                    {
                        string filename = dst + @"\";
                        filename = filename + z.ToString().Substring(src.Length);
                        if (File.Exists(filename)) File.Delete(filename);
                        z.ExtractToFile(filename);
                    }
                }
                return true;
            }

            // Получить список файлов и подкаталогов
            bool result = true;
            DirectoryInfo dir = new DirectoryInfo(src);

            //Скопировать файлы
            try
            {
                foreach (var file in dir.GetFiles())
                {
                    string newdst = dst + "\\" + file.FullName.Substring(src.Length);
                    newdst = newdst.Replace(@"\\", @"\");
                    result &= CopyFile(file.FullName, newdst); // Все файлы
                }
                // Скопировать подкаталоги
                foreach (var d in dir.GetDirectories())
                {
                    string newdst = dst + "\\" + d.FullName.Substring(src.Length);
                    newdst = newdst.Replace(@"\\", @"\");
                    result &= CopyDir(d.FullName, newdst); //Все каталоги
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(src + " -> " + dst, "CopyDir: Не могу скопировать");
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        struct Patch
        {
            public long Offset; // Смещение в файле
            public byte[] Data; // Данные
            public bool Override; // Сигнал о необходимости полной замены файла
            public void Save(BinaryWriter b)
            {
                b.Write(Override);
                b.Write(Offset);
                int Len = Data.Length;
                b.Write(Len);
                b.Write(Data);
            }

            public void Load(BinaryReader b)
            {
                Override = b.ReadBoolean();
                Offset = b.ReadInt64();
                int Len = b.ReadInt32();
                Data = b.ReadBytes(Len);
            }
        }

        static bool CreateBinaryPatch(string f1, string f2, out List<Patch> patches)
        {
            // Предполагается, что файлы одинаковой длины
            patches = new List<Patch>();
            byte[] F1 = File.ReadAllBytes(f1);
            byte[] F2 = File.ReadAllBytes(f2);

            if (F1.Length != F2.Length) // Тогда - полная перезапись
            {
                Patch p = new Patch();
                p.Override = true;
                p.Offset = 0;
                p.Data = F1.ToArray();
                patches.Add(p);
                return false;
            }

            // Для каждого байта
            int index = 0;
            while (index < F1.Length)
            {
                // Если байты совпадают - просто продолжить
                if (F1[index] == F2[index])
                {
                    index++;
                    continue;
                }

                // Если не совпадают - найти место, где снова совпали
                int len = 0;
                while (len + index < F1.Length && F1[len  +index] != F2[len + index])
                    len++;
                // И выделить очередную заплатку
                Patch p = new Patch();
                p.Override = false;
                p.Offset = index;
                p.Data = new byte[len];
                for (int k = 0; k < len; k++)
                    p.Data[k] = F2[index + k];
                patches.Add(p);
                index += len;
            }
            return true;
        }

        // Применить заплатку к f1, чтобы получить новый f2
        public static bool ApplyBinaryPatch(string f1, string f2, string patchname)
        {
            if (!File.Exists(patchname))
                return false;
            List<Patch> patches = new List<Patch>();
            FileStream r = new FileStream(patchname,FileMode.Open);
            BinaryReader b = new BinaryReader(r);

            while (r.Position < r.Length)
            {
                Patch p = new Patch();
                p.Load(b);
                patches.Add(p);
            }
            r.Close();

            byte[] F1 = File.ReadAllBytes(f1);
            byte[] F2 = File.ReadAllBytes(f2);

            foreach (Patch p in patches)
            {
                // Тяжелый случай
                if (p.Override)
                {
                    F2 = p.Data.ToArray();
                    continue;
                }
                // Нормальная заплатка
                for (int k = 0; k < p.Data.Length; k++)
                    F2[p.Offset + k] = p.Data[k];
            }
            File.WriteAllBytes(f2, F2);
            return true;
        }

        static List<Patch> lastPatch;

        // Простая проверка на изменения
        static bool IsIdenticalFile(string f1, string f2, bool deep=false)
        {
            f1 = f1.Replace("*", "").Replace(@"\\", @"\");
            f2 = f2.Replace("*", "").Replace(@"\\", @"\");

            if (!File.Exists(f1)) return false;
            if (!File.Exists(f2)) return false;

            FileInfo fi1, fi2;

            try
            { 
              fi1 = new FileInfo(f1);
            }
            catch
            {
                MessageBox.Show(f1, "Нет доступа");
                return false;
            }

            try
            {
                fi2 = new FileInfo(f2);
            }
            catch
            {
                MessageBox.Show(f2, "Нет доступа");
                return false;
            }

            // Если нет требования на "глубокое сканирование"
            bool result = (fi1.LastWriteTime == fi2.LastWriteTime) && (fi1.Length == fi2.Length);
            if (!deep || !result)
                return result;
            // Если требуется сравнение по содержимому
            CreateBinaryPatch(f1, f2, out lastPatch);
            return lastPatch.Count > 0;
            //Побочный эффект - создание набора заплаток
        }

        static bool IsIdenticalDir(string d1, string d2, bool deep = false)
        {
            DirectoryInfo dir1, dir2;

            d1 = d1.Replace("*", "").Replace(@"\\", @"\");
            d2 = d2.Replace("*", "").Replace(@"\\", @"\");

            try
            { 
              dir1 = new DirectoryInfo(d1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,d1);
                return false;
            }

            try
            { 
                dir2 = new DirectoryInfo(d2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, d2);
                return false;
            }

            bool result = true;
            try
            { 
               FileInfo[] test = dir1.GetFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, d1+" GetFiles");
                return false;
            }

            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                result &= IsIdenticalFile(file.FullName, newdst, deep); // Все файлы
                if (!result)
                    return false;
            }

            // Скопировать подкаталоги
            try
            {
                DirectoryInfo[] test = dir1.GetDirectories();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, d1 + " get directories");
                return false;
            }

            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                result &= IsIdenticalDir(d.FullName, newdst, deep); //Все каталоги
                if (!result)
                    return false;
            }
            return true;
        }


        private static bool CopyFile(string sourcefilename, string root, string version)
        {
            string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
            destinationfilename = destinationfilename.Replace(@"\\", "\\");
            return CopyFile(sourcefilename, destinationfilename);
        }

        static bool RestoreFile(string sourcefilename, string root, string version)
        //filename - имя восстанавливаемого файла
        {
            string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
            destinationfilename = destinationfilename.Replace(@"\\", "\\");
            return CopyFile(destinationfilename, sourcefilename);
        }


        private static bool CopyDir(string  sourcepath, string root, string version)
        {
            string destinationpath = root + "\\" + version + "\\" + sourcepath.Replace(@":\", @"\");
            destinationpath = destinationpath.Replace(@"\\", "\\");
            return CopyDir(sourcepath, destinationpath);
        }

        static bool RestoreDir(string sourcepath, string root, string version)
        {
            string destinationpath = root + "\\" + version + "\\" + sourcepath.Replace(@":\", @"\");
            destinationpath = destinationpath.Replace(@"\\", "\\");
            return CopyDir(destinationpath, sourcepath);
        }

        //*************************************************************************************//

        public static bool CopyMirror(ref Scenario scenario, string version="")
        {
            // Зеркальное копирование. Резервная копия заменяет прошлую
            // Настроить доступ к архиву, если это необходимо
            string Destination = scenario.Destination;
            ZipSource = false;
            zip = null;
            if (scenario.Zip)
            {
                ZipTarget = true;
                if (File.Exists(scenario.Destination)) File.Delete(scenario.Destination);
                zip = ZipFile.Open(scenario.Destination, ZipArchiveMode.Create);
                Destination = "";
            }
            else
            {
                ZipTarget = false;
            };

            // Для всех файлов и каталогов из списка сделать точную копию (с созданием соответствующего подкаталога)
            bool result = true;
            foreach (var filename in scenario.Source)
            {
                if (filename[0] == '*')
                    result &= CopyDir(filename.Substring(1), Destination, version);
                else
                    result &= CopyFile(filename, Destination, version);
            }
            if (zip!=null)
                zip.Dispose();
            return result;
        }

        public static bool RestoreMirror(ref Scenario scenario, string version = "")
        {
            string Destination = scenario.Destination;
            ZipTarget = false;
            if (scenario.Zip)
            {
                ZipSource = true;
                zip = ZipFile.Open(scenario.Destination, ZipArchiveMode.Read);
                Destination = "";
            }
            else
            {
                ZipSource = false;
            };

            // Для всех файлов и каталогов из списка сделать точную копию (с созданием соответствующего подкаталога)
            bool result = true;
            foreach (var filename in scenario.Source)
            {
                if (filename[0] == '*')
                    result &= RestoreDir(filename.Substring(1), Destination,  version);
                else
                    result &= RestoreFile(filename, Destination, version);
            }

            if (zip != null)
                zip.Dispose();

            return result;
        }

        //*************************************************************************************//

        static int GetLastVersion(string path)
        {
            // Получить номер самой что ни на есть последней версии.
            // Внутри path создаются подкаталоги с именами 0,1,2 и т.д.
            // (собственно, это значение и есть номер сохраненной версии)
            int last = 0;
            DirectoryInfo dir;

            try
            { 
                dir = new DirectoryInfo(path);
            }
            catch
            {
                MessageBox.Show(path, "GetLastVersion: нет доступа к каталогу");
                return 0;
            }

            DirectoryInfo[] dirDirectories;

            try
            { 
               dirDirectories = dir.GetDirectories();
            }
            catch
            {
                MessageBox.Show(path, "GetLastVersion: нет доступа к каталогу");
                return 0;
            }

            foreach (DirectoryInfo d in dirDirectories)
            {
                string x = Path.GetFileName(d.FullName);
                int vindex = 0;
                if (!int.TryParse(x, out vindex)) continue; // Это что-то другое, а не версия
                if (vindex > last) last = vindex;
            }
            return last;
        }

        // Одинаковы ли все описанные в сценарии объекты с объектами из копии?
        public static bool IsIdentical(ref Scenario scenario, string version, bool deep)
        {
            // Для всех файлов и каталогов из списка 
            bool result = true;
            string root = scenario.Destination;
            foreach (var sourcefilename in scenario.Source)
            {
                if (sourcefilename[0] == '*')
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    result &= IsIdenticalDir(sourcefilename.Substring(1), destinationfilename);
                }
                else
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    result &= IsIdenticalFile(sourcefilename, destinationfilename);
                }
                if (!result)
                    return false;
            }
            return true;
        }

        //*************************************************************************************//

        public static bool CopyFull(ref Scenario scenario)
        {
            // Создает отдельную копию 
            // Определить последнюю версию
            int version = GetLastVersion(scenario.Destination);
            // Сделать зеркальную копию в новое место

            if (IsIdentical(ref scenario, (version).ToString(), false))
            {
                return false;
            }

            return CopyMirror(ref scenario, (version + 1).ToString());
        }

        public static bool RestoreFull(ref Scenario scenario)
        {
            // Определить самую последнюю копию
            int version = GetLastVersion(scenario.Destination);
            // Сделать зеркальное восстановление
            RestoreMirror(ref scenario, version.ToString());
            return false;
        }

        //*************************************************************************************//

        static bool IncrementalFile(string f1, string f2, bool deep = true)
        {
            if (!File.Exists(f1))
                return false;
            if (!File.Exists(f2))
                return false;
            // Требуется сравнение по содержимому
            CreateBinaryPatch(f1, f2, out lastPatch);
            if (lastPatch.Count == 0)
                return true;
            // Побочный эффект - создание набора заплаток
            // Сохранить его под именем f2:patch
            // Удалить, если он уже есть
            if (File.Exists(f2 + ";patch"))
                File.Delete(f2 + ";patch");

            FileStream w = new FileStream(f2 + ";patch",FileMode.CreateNew);
            BinaryWriter b = new BinaryWriter(w);
            foreach (Patch p in lastPatch)
                p.Save(b);
            w.Close();
            return true;
        }

        static bool IncrementalFileRestore(string f1, string f2)
        {
            if (!File.Exists(f1)) return false;
            if (!File.Exists(f2)) return false;

            // Требуется сравнение по содержимому
            string patchname = f2 + ";patch";
            if (!File.Exists(patchname))
                return true;
            // Применить к файлу f2 patchname и сохранить в f1
            ApplyBinaryPatch(f2, f1, patchname);
            return true;
        }

        static bool IncrementalDir(string d1, string d2)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);
            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalFile(file.FullName, newdst);
            }

            //Скопировать подкаталоги
            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalDir(d.FullName, newdst); // Все каталоги
            }
            return true;
        }

        static bool IncrementalDirRestore(string d1, string d2)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);
            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalFileRestore(file.FullName, newdst);
            }
            // Скопировать подкаталоги
            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalDirRestore(d.FullName, newdst); //Все каталоги
            }
            return true;
        }


        public static bool CopyIncremental(ref Scenario scenario)
        {
            // Начальное состояние + изменения БЛОКОВ (отдельно по версиям)
            // Есть ли зеркальная копия?
            if (0 == GetLastVersion(scenario.Destination))
            {
                // Если нет, то сделать ее
                CopyFull(ref scenario);
                return true; // Собственно говоря, копия готова
            }

            // Для всех файлов
            // Сделать сравнение и сохранить patch-и (под именем файла)
            string root = scenario.Destination;
            string version = "1";
            foreach (var sourcefilename in scenario.Source)
            {
                if (sourcefilename[0] == '*')
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Substring(1).Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    IncrementalDir(sourcefilename.Substring(1), destinationfilename);
                }
                else
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    IncrementalFile(sourcefilename, destinationfilename);
                }
            }
            return true;
        }

        public static bool RestoreIncremental(ref Scenario scenario)
        {
            // Есть ли зеркальная копия?
            if (0 == GetLastVersion(scenario.Destination))
            {
                return false; // Собственно говоря, копии то и нет
            }
            // Начальное состояние + история изменений
            // Для всех файлов
            // Если есть patch - провести восстановление
            string root = scenario.Destination;
            string version = "1";
            foreach (var sourcefilename in scenario.Source)
            {
                if (sourcefilename[0] == '*')
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Substring(1).Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    IncrementalDirRestore(sourcefilename.Substring(1), destinationfilename);
                }
                else
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    IncrementalFileRestore(sourcefilename, destinationfilename);
                }
            }
            return true;
        }

        //*************************************************************************************//

        public static bool CopyDifferential(ref Scenario scenario)
        {
            //Начальное состояние + изменения относительно начального состояния
            return CopyIncremental(ref scenario); /// Заглушка
        }

        public static bool RestoreDifferential(ref Scenario scenario)
        {
            return RestoreIncremental(ref scenario); ///Заглушка
        }
    }
}
