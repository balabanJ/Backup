/*
 * Пространство имен System содержит фундаментальные и базовые классы, 
 * определяющие часто используемые типы значений и ссылочных данных, события и обработчики событий, 
 * интерфейсы, атрибуты и исключения обработки.
 */
using System;
/*
 * Пространство имен System.Collections.Generic содержит интерфейсы и классы, определяющие универсальные коллекции, 
 * которые позволяют пользователям создавать строго типизированные коллекции, обеспечивающие повышенную 
 * производительность и безопасность типов по сравнению с неуниверсальными строго типизированными коллекциями.
 */
using System.Collections.Generic;
/*
 * Пространство имен System.Linq содержит классы и интерфейсы, которые поддерживают LINQ.
 */
using System.Linq;
/*
 * Пространство имен System.IO содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
 * а также типы для базовой поддержки файлов и папок.
 */
using System.IO;
/*
 * Пространство имен System.Windows.Forms содержит классы для создания приложений Windows, которые позволяют 
 * наиболее эффективно использовать расширенные возможности пользовательского интерфейса, 
 * доступные в операционной системе Microsoft Windows.
 */
using System.Windows.Forms;
/* 
 * Пространство имен System.IO.Compression содержит классы, предоставляющие основные службы сжатия и распаковки для потоков.
 */
using System.IO.Compression;

namespace Backup
{
    // Класс "Создание копии"
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

        static public List<string> Log = new List<string>();

        // Сравнить два потока
        static bool DifferentStreams(Stream srcs, Stream dsts, string srcname, string dstname)
        {
            bool result = false;
            // Если TRUE, они различаются, значит различия запротоколировать в MakeCopyLog
            try
            {
                if (srcs.Length != dsts.Length)
                { 
                    Log.Add("Длина " + srcname + " не равна длине " + dstname);
                    return true;
                }
            }
            catch
            {
                return true;
            }

            // Проверить на побайтовое совпадение
            try
            { 
                while (srcs.Position != srcs.Length)
                    if (srcs.ReadByte() != dsts.ReadByte())
                    {
                        result = true;
                        break;
                    }
                if (result)
                    Log.Add("Содержимое " + srcname + " не совпадает с " + dstname);

                // Не испортить позицию в потоке
                srcs.Position = 0;
                dsts.Position = 0;
            }
            catch
            {
                return true;
            }
            return result;
        }

        // Скопировать файл
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
            {
                // Проверить на совпадение
                try
                {
                    ZipArchiveEntry zae = zip.GetEntry(ToZIPpath(dst));
                    dsts = zae.Open();
                    DifferentStreams(srcs, dsts, src, ToZIPpath(dst));
                    dsts.Dispose();    
                }
                catch 
                {
                    Log.Add("Файл " + dst + " создан");
                }
                // Создать файл
                dsts = zip.CreateEntry(ToZIPpath(dst)).Open();
            }
            else
            {
                if (!Directory.Exists(Path.GetDirectoryName(dst)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dst));

                if (!File.Exists(dst))
                    Log.Add("Файл " + dst + " создан");
                else
                {
                    dsts = new FileStream(dst, FileMode.Open);
                    DifferentStreams(srcs, dsts, src, dst);
                    dsts.Dispose();
                }
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

        // Скопировать каталог
        static bool CopyDir(string src, string dst, bool CreateSHA, bool CheckSHA)
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
                        filename = filename + z.Name; //z.ToString().Substring(src.Length);
                        if (File.Exists(filename)) File.Delete(filename);
                        z.ExtractToFile(filename);
                    }
                }
                return true;
            }

            // Получить список файлов и подкаталогов
            bool result = true;
            DirectoryInfo dir = new DirectoryInfo(src);

            try
            {
                // Скопировать файлы
                var dirGetFiles = dir.GetFiles();
                foreach (var file in dirGetFiles)
                {
                    if (Path.GetExtension(file.Name) == ".sha512")
                        continue; // Не копировать контрольные суммы
                    string newdst = dst + "\\" + file.FullName.Substring(src.Length);
                    newdst = newdst.Replace(@"\\", @"\");

                    if (CheckSHA)
                        if (!ZipSource || SHA512X.CheckSHA512(file.FullName))
                        {
                            result &= CopyFile(file.FullName, newdst); // Все файлы
                            continue;
                        }
                    result &= CopyFile(file.FullName, newdst); // Все файлы
                    if (CreateSHA && !ZipTarget)
                        SHA512X.CreateSHA512(newdst);
                }

                // Скопировать подкаталоги
                foreach (var d in dir.GetDirectories())
                {
                    string newdst = dst + "\\" + d.FullName.Substring(src.Length);
                    newdst = newdst.Replace(@"\\", @"\");
                    result &= CopyDir(d.FullName, newdst, CreateSHA, CheckSHA); // Все каталоги
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(src + " -> " + dst, "CopyDir: не могу скопировать");
                MessageBox.Show(ee.Message);
            }
            return result;
        }

        // "Заплатка"
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

            if (F1.Length != F2.Length) // Если TRUE - полная перезапись
            {
                Patch p = new Patch();
                p.Override = true;
                p.Offset = 0;
                p.Data = F1.ToArray();
                patches.Add(p);
                return false;
            }

            int index= 0;

            //Для каждого байта
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
                while (len + index < F1.Length && F1[len+index] != F2[len+index])
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
            FileStream r = new FileStream(patchname, FileMode.Open);
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
                if (p.Override)
                {
                    F2 = p.Data.ToArray();
                    continue;
                }
                for (int k = 0; k < p.Data.Length; k++)
                    F2[p.Offset + k] = p.Data[k];
            }
            File.WriteAllBytes(f2, F2);
            return true;
        }

        static List<Patch> lastPatch;

        // Простая проверка на изменения в файле
        static bool IsIdenticalFile(string f1, string f2, bool deep=false)
        {
            f1 = f1.Replace("*", "").Replace(@"\\", @"\");
            f2 = f2.Replace("*", "").Replace(@"\\", @"\");

            if (!File.Exists(f1))
                return false;
            if (!File.Exists(f2))
                return false;

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
        }

        // Простая проверка на изменения в каталоге
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
                MessageBox.Show(ex.Message, d1 + " Get files");
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

            try
            {
                DirectoryInfo[] test = dir1.GetDirectories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, d1 + " Get directories");
                return false;
            }

            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");

                result &= IsIdenticalDir(d.FullName, newdst, deep); // Все каталоги

                if (!result)
                    return false;
            }
            return true;
        }

        // Скопировать файл
        private static bool CopyFile(string sourcefilename, string root, string version, bool CreateCheckSum)
        {
            string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
            destinationfilename = destinationfilename.Replace(@"\\", "\\");

            bool result = CopyFile(sourcefilename, destinationfilename);

            if (!ZipTarget)
                SHA512X.CreateSHA512(destinationfilename);

            return result;
        }

        // Восстановить файл
        static bool RestoreFile(string sourcefilename, string root, string version, bool AlarmCheckSum)
        // filename - имя восстанавливаемого файла
        {
            string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
            destinationfilename = destinationfilename.Replace(@"\\", "\\");

            bool OK = !AlarmCheckSum; // Не проверять контрольную сумму
            OK |= ZipSource; // Архив это
            if (!OK)
                OK = SHA512X.CheckSHA512(destinationfilename); // Проверить контрольную сумму
            if (!OK)
                OK = MessageBox.Show("Выполнять восстановление?", "Не совпадает или отсутствует контрольная сумма", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes;
            if (OK)
                return CopyFile(destinationfilename, sourcefilename);

            return false;            
        }

        // Копировать каталог
        private static bool CopyDir(string  sourcepath, string root, string version, bool CreateCheckSum)
        {
            string destinationpath = root + "\\" + version + "\\" + sourcepath.Replace(@":\", @"\");
            destinationpath = destinationpath.Replace(@"\\", "\\");

            return CopyDir(sourcepath, destinationpath, true, false);
        }

        // Восстановить каталог
        static bool RestoreDir(string sourcepath, string root, string version)
        {
            string destinationpath = root + "\\" + version + "\\" + sourcepath.Replace(@":\", @"\");
            destinationpath = destinationpath.Replace(@"\\", "\\");

            return CopyDir(destinationpath, sourcepath, false, true);
        }

        // Исключить недопустимые для файловой системы символы
        public static string exclude(string s)
        {
            s = s.Replace("*", "");
            s = s.Replace("?", "");
            s = s.Replace(":", "");
            s = s.Replace("/", "");
            s = s.Replace(@"\", "");
            s = s.Replace("«", "");
            s = s.Replace("<", "");
            s = s.Replace(">", "");
            s = s.Replace("|", "");
            if (s[s.Length-1]!='\\')
                s += @"\";
            return s;
        }

        //-----------------------------------------------------------------------------------------//

        // Копировать файлы/каталоги с помощью зеркального типа резервирования
        public static bool CopyMirror(ref Scenario scenario, string version="")
        {
            string Destination = scenario.Destination + exclude(scenario.Title);

            if (scenario.Zip)
                Destination = Path.GetDirectoryName(scenario.Destination) + "\\" + MakeCopy.exclude(scenario.Title) + version + "\\" + Path.GetFileName(scenario.Destination);

            ZipSource = false;
            zip = null;
            if (scenario.Zip)
            {
                ZipTarget = true;
                if (File.Exists(scenario.Destination))
                    File.Delete(scenario.Destination);
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Destination));
                    zip = ZipFile.Open(Destination, ZipArchiveMode.Create);
                }
                catch
                {
                    return false;
                }
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
                    result &= CopyDir(filename.Substring(1), Destination, version, true);
                else
                    result &= CopyFile(filename, Destination, version, true);
            }
            if (zip!=null)
                zip.Dispose();
            try
            {
                if (scenario.Zip) // Создать контрольную сумму для всего архива
                                SHA512X.CreateSHA512(scenario.Destination);
            }
            catch
            {
                return false;
            }
            
            return result;
        }

        // Восстановить файлы/каталоги с помощью зеркального типа резервирования
        public static bool RestoreMirror(ref Scenario scenario, string version = "")
        {
            string Destination = scenario.Destination + exclude(scenario.Title);

            if (scenario.Zip)
                Destination = Path.GetDirectoryName(scenario.Destination) + "\\" + MakeCopy.exclude(scenario.Title) + version + "\\" + Path.GetFileName(scenario.Destination);

            ZipTarget = false;
            if (scenario.Zip)
            {
                ZipSource = true;

                // Проверить контрольную сумму
                if (!SHA512X.CheckSHA512(scenario.Destination))
                    if (MessageBox.Show("Продолжить?", "Контрольная сумма архива повреждена или отсутствует", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
                try
                {
                    zip = ZipFile.Open(scenario.Destination, ZipArchiveMode.Read);
                }
                catch
                {
                    MessageBox.Show("Файл копии не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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
                    result &= RestoreFile(filename, Destination, version, true);
            }

            if (zip != null)
                zip.Dispose();

            return result;
        }

        //-----------------------------------------------------------------------------------------//

        // Получить номер самой что ни на есть последней версии.
        // Внутри path создаются подкаталоги с именами 0,1,2 и т.д.
        // (собственно, это значение и есть номер сохраненной версии)
        static int GetLastVersion(string path)
        {
            int last = 0;
            DirectoryInfo dir;

            try
            { 
                dir = new DirectoryInfo(path);
            }
            catch
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    MessageBox.Show(path, "GetLastVersion: нет доступа к каталогу");
                }
                return 0;
            }

            DirectoryInfo[] dirDirectories;

            try { 
               dirDirectories = dir.GetDirectories();
            }
            catch
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    MessageBox.Show(path, "GetLastVersion: нет доступа к каталогу");
                }
                return 0;
            }

            foreach (DirectoryInfo d in dirDirectories)
            {
                string x = Path.GetFileName(d.FullName);
                int vindex = 0;

                if (!int.TryParse(x, out vindex))
                    continue;
                if (vindex > last)
                    last = vindex;
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

        //-----------------------------------------------------------------------------------------//

        // Копировать файлы/каталоги с помощью полного типа резервирования
        public static bool CopyFull(ref Scenario scenario)
        {
            string Destination;
            int version;

            if (scenario.Zip)
            {
                Destination = Path.GetDirectoryName(scenario.Destination) + "\\" + MakeCopy.exclude(scenario.Title);
            }
            else
            {
                Destination = scenario.Destination + exclude(scenario.Title);
            }
            // Определить последнюю версию
            version = GetLastVersion(Destination);

            if (IsIdentical(ref scenario, (version).ToString(), false))
            {
                return false;
            }

            // Сделать зеркальную копию в новое место
            return CopyMirror(ref scenario, (version + 1).ToString());
        }

        // Восстановить файлы/каталоги с помощью полного типа резервирования
        public static bool RestoreFull(ref Scenario scenario)
        {
            string Destination;
            int version;

            if (scenario.Zip)
            { 
                Destination = Path.GetDirectoryName(scenario.Destination) + "\\" + MakeCopy.exclude(scenario.Title);
            }
            else
            { 
                Destination = scenario.Destination + exclude(scenario.Title);
            }
            // Определить самую последнюю копию
            version = GetLastVersion(Destination);

            // При необходимости переспросить у пользователя, какую версию использовать
            if (version > 1)
            {
                FormVersion FV = new FormVersion();
                for (int i = 0; i < version; i++)
                    FV.comboBox.Items.Add(i + 1);
                FV.comboBox.SelectedIndex = version - 1;
                FV.ShowDialog();
                version = FV.comboBox.SelectedIndex + 1;
            }

            // Сделать зеркальное восстановление
            return RestoreMirror(ref scenario, version.ToString());
        }

        //-----------------------------------------------------------------------------------------//

        static bool PatchFile(string f1, string f2, string basefilename, bool deep = true)
        {
            if (!File.Exists(f1))
                return false;
            if (!File.Exists(basefilename))
                return false;

            // Требуется сравнение по содержимому
            CreateBinaryPatch(f1, basefilename, out lastPatch);
            if (lastPatch.Count == 0)
                return true;

            //Сохранить его под именем f2:patch
            //Удалить, если он уже есть
            Log.Add(lastPatch.Count.ToString() + " изменений в " + f1);

            if (File.Exists(f2 + ";patch"))
                File.Delete(f2 + ";patch");

            //Убедиться в существовании подкаталога
            Directory.CreateDirectory(Path.GetDirectoryName(f2)); 

            FileStream w = new FileStream(f2 + ";patch", FileMode.CreateNew);
            BinaryWriter b = new BinaryWriter(w);

            foreach (Patch p in lastPatch)
                p.Save(b);
            w.Close();

            return true;
        }

        static bool PatchFileRestore(string f1, string f2)
        {
            if (!File.Exists(f1))
                return false;
            if (!File.Exists(f2))
                return false;
            //Требуется сравнение по содержимому
            string patchname = f2 + ";patch";
            if (!File.Exists(patchname))
                return true;

            //Применить к файлу f2 patchname и сохранить в f1
            ApplyBinaryPatch(f2, f1, patchname);

            return true;
        }

        static bool PatchDirRestore(string d1, string d2)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);

            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                PatchFileRestore(file.FullName, newdst);
            }

            //Скопировать подкаталоги
            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                PatchDirRestore(d.FullName, newdst); //Все каталоги
            }

            return true;
        }


        static bool PatchDir(string d1, string d2, string basefilename)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);

            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                PatchFile(file.FullName, newdst, basefilename);
            }

            //Скопировать подкаталоги
            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                PatchDir(d.FullName, newdst, basefilename); //Все каталоги
            }

            return true;
        }

        //-----------------------------------------------------------------------------------------//

        // Копировать файлы с помощью инкрементального типа резервирования
        /*static bool IncrementalFile(string f1, string f2,string basefilename, bool deep = true)
        {
            if (!File.Exists(f1))
                return false;
            if (!File.Exists(basefilename))
                return false;

            // Требуется сравнение по содержимому
            CreateBinaryPatch(f1, basefilename, out lastPatch);
            if (lastPatch.Count == 0)
                return true;

            // Сохранить его под именем "f2:patch"
            // Удалить, если он уже есть
            Log.Add(lastPatch.Count.ToString() + " изменений в " + f1);

            if (File.Exists(f2 + ";patch"))
                File.Delete(f2 + ";patch");

            // Убедиться в существовании подкаталога
            Directory.CreateDirectory(Path.GetDirectoryName(f2)); 

            FileStream w = new FileStream(f2 + ";patch",FileMode.CreateNew);
            BinaryWriter b = new BinaryWriter(w);

            foreach (Patch p in lastPatch)
                p.Save(b);
            w.Close();

            return true;
        }*/

        // Восстановить файлы с помощью инкрементального типа резервирования
        /*static bool IncrementalFileRestore(string f1, string f2)
        {
            if (!File.Exists(f1)) return false;
            if (!File.Exists(f2)) return false;
            //Требуется сравнение по содержимому
            string patchname = f2 + ";patch";
            if (!File.Exists(patchname)) return true;
            //Применить к файлу f2 patchname и сохранить в f1
            ApplyBinaryPatch(f2, f1, patchname);
            return true;
        }*/

        // Копировать каталоги с помощью инкрементального типа резервирования
        /*static bool IncrementalDir(string d1, string d2, string basefilename)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);
            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalFile(file.FullName, newdst, basefilename);
            }

            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalDir(d.FullName, newdst, basefilename); // Все каталоги
            }
            return true;
        }*/

        // Восстановить каталоги с помощью инкрементального типа резервирования
        /*static bool IncrementalDirRestore(string d1, string d2)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);

            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalFileRestore(file.FullName, newdst);
            }

            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                IncrementalDirRestore(d.FullName, newdst); // Все каталоги
            }
            return true;
        }*/

        public static bool CopyIncremental(ref Scenario scenario)
        {
            // Начальное состояние + изменения БЛОКОВ (отдельно по версиям)
            // Есть ли зеркальная копия?
            int Version = GetLastVersion(scenario.Destination + exclude(scenario.Title));

            if (0 == Version)
            {
                //Если нет, то сделать ее
                CopyFull(ref scenario);

                return true; // Собственно говоря, копия готова
            }

            // Для всех файлов:
            // Сделать сравнение и сохранить patch-и (под именем файла)
            string root = scenario.Destination + exclude(scenario.Title);
            string version = (Version).ToString();

            foreach (var sourcefilename in scenario.Source)
            {
                if (sourcefilename[0] == '*')
                {
                    string basefilename = root + "\\" + "1" + "\\" + sourcefilename.Substring(1).Replace(@":\", @"\");
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Substring(1).Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    basefilename = basefilename.Replace(@"\\", "\\");
                    PatchDir(sourcefilename.Substring(1), destinationfilename, basefilename);
                }
                else
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    string basefilename = root + "\\" + "1" + "\\" + sourcefilename.Replace(@":\", @"\");
                    basefilename = basefilename.Replace(@"\\", "\\");
                    PatchFile(sourcefilename, destinationfilename, basefilename);
                }
            }
            return true;
        }

        public static bool RestoreIncremental(ref Scenario scenario, int  Version=-1)
        {
            // Есть ли зеркальная копия?
            if (Version < 0)
                Version = GetLastVersion(scenario.Destination + exclude(scenario.Title));

            if (0 == Version)
            {
                MessageBox.Show("Нет ни одной сохраненной копии");
                return false; //Собственно говоря, копии то и нет
            }

            // Начальное состояние + история изменений
            // Для всех файлов
            // Если есть patch - провести восстановление
            string root = scenario.Destination + exclude(scenario.Title);
            string version = Version.ToString();

            foreach (var sourcefilename in scenario.Source)
            {
                if (sourcefilename[0] == '*')
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Substring(1).Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    PatchDirRestore(sourcefilename.Substring(1), destinationfilename);
                }
                else
                {
                    string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
                    destinationfilename = destinationfilename.Replace(@"\\", "\\");
                    PatchFileRestore(sourcefilename, destinationfilename);
                }
            }
            return true;
        }

        //-----------------------------------------------------------------------------------------//

        // Копировать файлы/каталоги с помощью дифференциального типа резервирования
        public static bool CopyDifferential(ref Scenario scenario)
        {
            int version = GetLastVersion(scenario.Destination + exclude(scenario.Title));
            Directory.CreateDirectory(scenario.Destination + exclude(scenario.Title)+(version+1).ToString());

            // Начальное состояние + изменения относительно начального состояния
            return CopyIncremental(ref scenario);
        }

        // Восстановить файлы/каталоги с помощью дифференциального типа резервирования
        public static bool RestoreDifferential(ref Scenario scenario)
        {
            int version = GetLastVersion(scenario.Destination + exclude(scenario.Title));

            // При необходимости переспросить у пользователя, какую версию использовать
            if (version > 1)
            {
                FormVersion FV = new FormVersion();
                for (int i = 0; i < version; i++)
                    FV.comboBox.Items.Add(i + 1);
                FV.comboBox.SelectedIndex = version - 1;
                FV.ShowDialog();
                version = FV.comboBox.SelectedIndex + 1;
            }

            //Восстановить ВСЕ версии, включая указанную
            bool result = true;
            for (int recover = 1; recover <= version; recover++)
                result &= RestoreIncremental(ref scenario, recover);
            return result;
        }
    }
}
