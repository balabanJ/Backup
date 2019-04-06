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

        public static bool CopyFull(ref Scenario scenario)
        {
            //создает отдельную копию 
            //Определить последнюю версию
            int version = GetLastVersion(scenario.Destination);
            //сделать зеркальную копию в новое место
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


        public static bool CopyIncremental(ref Scenario scenario)
        {
            //начальное состояние + изменения БЛОКОВ (отдельно по версиям)
            return false;
        }

        public static bool CopyDifferential(ref Scenario scenario)
        {
            //начальное состояние +изменения относительно начального состояния
            return false;
        }

        public static bool RestoreIncremental(ref Scenario scenario)
        {
            return false;
        }

        public static bool RestoreDifferential(ref Scenario scenario)
        {
            return false;
        }


    }
}
