using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Windows.Forms;
using System.IO;

using Microsoft.Win32;

//Сервисные функции

namespace Backup
{
    class Service
    {
        const string ApplicationName = "Backup";
        static public bool SetAutorunValue(bool autorun)
        {
            string ExePath = Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(ApplicationName, ExePath);
                else
                    reg.DeleteValue(ApplicationName);
                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        static public void Minimize()
        {
            ///!!!Application.
            ///Показать Tray
        }

        static public void Restore()
        {
            ///!!!
        }


        static string CurrentFileName="default.scenario"; 

        static public void SaveList(ScenarioList list)
        {
            list.Save(CurrentFileName);
        }

        static public void LoadList(ScenarioList list)
        {
            list.list.Clear();

            if (!File.Exists(CurrentFileName))
            {
                Scenario s = new Scenario();
                s.Title = "Демо 1";
                s.Zip = true;
                s.Destination = @"C:\Users\Юлия\Desktop\Backup";
                s.Source.Add(@"C:\Users\Юлия\Documents\Python\program1");
                s.Source.Add(@"C:\Users\Юлия\Pictures\Wallpapers");
                list.Add(s);

                s = new Scenario();
                s.Title = "Демо 2";
                s.scenarioType = ScenarioType.зеркальный;
                s.Destination = @"H:\Backup";
                s.Source.Add(@"D:\Films");
                list.Add(s);

                s = new Scenario();
                s.Title = "Демо 3";
                s.scenarioType = ScenarioType.полный;
                s.Destination = @"\\User2\Backup";
                s.Source.Add(@"\\User2\Documents\");
                list.Add(s);
            }
            else
                list.Load(CurrentFileName);
        }
    }
}
