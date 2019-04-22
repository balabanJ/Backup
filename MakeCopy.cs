using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace Backup
{
    class MakeCopy
    {
        //Скопировать каталог с заданным именем в место, определяемое root
        //Определить имя файла - назначения для выбранного файла - исходного
        //Имя выходного файла формируется следующим образом:
        //Z:\Example\File.txt --> Backup\Version\Z\Example\File.txt
        //Backup\Version\Z\Example\File.txt --> Z:\Example\File.txt 
        //Копирование файла
        static bool copyfile(string src, string dst)
        {
            if (!Directory.Exists(Path.GetDirectoryName(dst)))
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dst));
                }
                catch { }; //Что то не то
            try
            {
                if (File.Exists(dst)) File.Delete(dst);
                File.Copy(src, dst);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Копирование каталога с подкаталогами
        static bool copydir(string src, string dst)
        { 
        //Получить список файлов и подкаталогов
        bool result = true;
        DirectoryInfo dir = new DirectoryInfo(src);
        //Скопировать файлы
        foreach (var file in dir.GetFiles())
            {
                string newdst = dst + "\\" +file.FullName.Substring(src.Length);
                newdst = newdst.Replace(@"\\",@"\");
                result &= copyfile(file.FullName, newdst); //Все файлы
            }
            //Скопировать подкаталоги
            foreach (var d in dir.GetDirectories())
            {
                string newdst = dst + "\\" + d.FullName.Substring(src.Length);
                newdst = newdst.Replace(@"\\", @"\");
                result &= copydir(d.FullName, newdst); //Все каталоги
            }
            return result;
        }

        struct Patch
        {
            public long Offset; //Смещение в файле
            public byte[] Data; //Данные
            public bool Override; //Сигнал о необходимости полной замены файла
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
            //предполагается, что файлы одинаковой длины
            patches = new List<Patch>();
            byte[] F1 = File.ReadAllBytes(f1);
            byte[] F2 = File.ReadAllBytes(f2);
            if (F1.Length != F2.Length)
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
            while (index<F1.Length)
            {
                //если байты совпадают
                //просто продолжить
                if (F1[index] == F2[index]) { index++;  continue; }
                //если не совпадают 
                //найти место, где снова совпали
                int len = 0;
                while (len+index<F1.Length && F1[len+index] != F2[len+index]) len++;
                //и выделить очередную заплатку
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

        //Применить заплатку к f1, чтобы получить новый f2
        public static bool ApplyBinaryPatch(string f1, string f2, string patchname)
        {

            if (!File.Exists(patchname)) return false;
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
                //Тяжелый случай
                if (p.Override)
                {
                    F2 = p.Data.ToArray();
                    continue;
                }
                //Нормальная заплатка
                for (int k = 0; k < p.Data.Length; k++)
                    F2[p.Offset + k] = p.Data[k];
            }
            File.WriteAllBytes(f2, F2);
            return true;
        }

        static List<Patch> lastPatch;

        //простая проверка на изменения. 
        static bool IsIdenticalFile(string f1, string f2, bool deep=false)
        {
            if (!File.Exists(f1)) return false;
            if (!File.Exists(f2)) return false;
            FileInfo fi1 = new FileInfo(f1);
            FileInfo fi2 = new FileInfo(f2);
            //Если нет требования на "глубокое сканирование"
            bool result = (fi1.LastWriteTime == fi2.LastWriteTime) && (fi1.Length == fi2.Length);
            if (!deep || !result)
                return result;
            //Если требуется сравнение по содержимому
            CreateBinaryPatch(f1, f2, out lastPatch);
            return lastPatch.Count > 0;
            //Побочный эффект - создание набора заплаток
        }

        static bool IsIdenticalDir(string d1, string d2, bool deep = false)
        {
            DirectoryInfo dir1 = new DirectoryInfo(d1);
            DirectoryInfo dir2 = new DirectoryInfo(d2);
            bool result = true;

            foreach (var file in dir1.GetFiles())
            {
                string newdst = d2 + "\\" + file.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                result &= IsIdenticalFile(file.FullName, newdst, deep); //Все файлы
                if (!result) return false;
            }
            //Скопировать подкаталоги
            foreach (var d in dir1.GetDirectories())
            {
                string newdst = d2 + "\\" + d.FullName.Substring(d1.Length);
                newdst = newdst.Replace(@"\\", @"\");
                result &= IsIdenticalDir(d.FullName, newdst, deep); //Все каталоги
                if (!result) return false;
            }
            return true;
        }


        private static bool CopyFile(string sourcefilename, string root, string version)
        {
            string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
            destinationfilename = destinationfilename.Replace(@"\\", "\\");
            return copyfile(sourcefilename, destinationfilename);
        }

        static bool RestoreFile(string sourcefilename, string root, string version)
        //filename - имя восстанавливаемого файла
        {
            string destinationfilename = root + "\\" + version + "\\" + sourcefilename.Replace(@":\", @"\");
            destinationfilename = destinationfilename.Replace(@"\\", "\\");
            return copyfile(destinationfilename, sourcefilename);
        }


        private static bool CopyDir(string  sourcepath, string root, string version)
        {
            string destinationpath = root + "\\" + version + "\\" + sourcepath.Replace(@":\", @"\");
            destinationpath = destinationpath.Replace(@"\\", "\\");
            return copydir(sourcepath, destinationpath);
        }

        static bool RestoreDir(string sourcepath, string root, string version)
        {
            string destinationpath = root + "\\" + version + "\\" + sourcepath.Replace(@":\", @"\");
            destinationpath = destinationpath.Replace(@"\\", "\\");
            return copydir(destinationpath, sourcepath);
        }


        public static bool CopyMirror(ref Scenario scenario, string version="")
        {
            //Зеркально копирование. Резервная копия заменяет прошлую

            //Для всех файлов и каталогов из списка 
            //сделать точную копию (с созданием соответствующего подкаталога)
            bool result = true;
            foreach (var filename in scenario.Source)
            {
                if (filename[0] == '*')
                    result &= CopyDir(filename.Substring(1), scenario.Destination, version);
                else
                    result &= CopyFile(filename, scenario.Destination, version);
            }
            return result;
        }

        public static bool RestoreMirror(ref Scenario scenario, string version = "")
        {
            //Для всех файлов и каталогов из списка 
            //сделать точную копию (с созданием соответствующего подкаталога)
            bool result = true;
            foreach (var filename in scenario.Source)
            {
                if (filename[0] == '*')
                    result &= RestoreDir(filename.Substring(1), scenario.Destination,  version);
                else
                    result &= RestoreFile(filename, scenario.Destination, version);
            }
            return result;
        }


        static int GetLastVersion(string path)
        {
            //Получить номер самой что ни на есть последней версии.
            //Внутри path создаются подкаталоги с именами 0,1,2 и так далее
            //собственно, это значение и есть номер сохраненной версии
            int last = 0;
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                string x = Path.GetFileName(d.FullName);
                int vindex = 0;
                if (!int.TryParse(x, out vindex)) continue; //Это что то другое, а не версия
                if (vindex > last) last = vindex;
            }
            return last;
        }

        //Одинаковы ли все описанные в сценарии объекты с объектами из копии
        public static bool IsIdentical(ref Scenario scenario, string version, bool deep)
        {
            //Для всех файлов и каталогов из списка 
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
                if (!result) return false;
            }
            return true;
        }

        //*************************************************************************************//

        public static bool CopyFull(ref Scenario scenario)
        {
            //создает отдельную копию 
            //Определить последнюю версию
            int version = GetLastVersion(scenario.Destination);
            //сделать зеркальную копию в новое место

            if (IsIdentical(ref scenario, (version).ToString(), false))
            {
                return false;
            }

            return CopyMirror(ref scenario, (version + 1).ToString());
        }

        public static bool RestoreFull(ref Scenario scenario)
        {
            //Определить самую последнюю копию
            int version = GetLastVersion(scenario.Destination);
            //Сделать зеркальное восстановление
            RestoreMirror(ref scenario, version.ToString());
            return false;
        }

        //*************************************************************************************//

        static bool IncrementalFile(string f1, string f2, bool deep = true)
        {
            if (!File.Exists(f1)) return false;
            if (!File.Exists(f2)) return false;
            //Требуется сравнение по содержимому
            CreateBinaryPatch(f1, f2, out lastPatch);
            if (lastPatch.Count == 0) return true;
            //Побочный эффект - создание набора заплаток
            //Сохранить его под именем f2:patch

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
            //Требуется сравнение по содержимому
            string patchname = f2 + ";patch";
            if (!File.Exists(patchname)) return true;
            //Применить к файлу f2 patchname и сохранить в f1
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
                IncrementalDir(d.FullName, newdst); //Все каталоги
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
            //Скопировать подкаталоги
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
            //начальное состояние + изменения БЛОКОВ (отдельно по версиям)
            //Есть ли зеркальная копия?
            if (0 == GetLastVersion(scenario.Destination))
            {
                //Если нет, то сделать ее
                CopyFull(ref scenario);
                return true; //Собственно говоря, копия готова
            }

            //Для всех файлов
            //Сделать сравнение и сохранить patch-и (под именем файла)
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
            //Есть ли зеркальная копия?
            if (0 == GetLastVersion(scenario.Destination))
            {
                return false; //Собственно говоря, копии то и нет
            }
            //начальное состояние +история изменений
            //Для всех файлов
            //Если есть patch
            //Провести восстановление
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
            //начальное состояние +изменения относительно начального состояния
            return false;
        }

        public static bool RestoreDifferential(ref Scenario scenario)
        {
            return false;
        }


    }
}
