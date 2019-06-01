/*
 * Пространство имен System.Windows.Forms содержит классы для создания приложений Windows, которые позволяют 
 * наиболее эффективно использовать расширенные возможности пользовательского интерфейса, 
 * доступные в операционной системе Microsoft Windows.
 */
using System.Windows.Forms;
/*
 * Пространство имен System.IO содержит типы, позволяющие осуществлять чтение и запись в файлы и потоки данных, 
 * а также типы для базовой поддержки файлов и папок.
 */
using System.IO;
/* 
 * Пространство имен System.Data обеспечивает доступ к классам, представляющим архитектуру ADO.NET. 
 * ADO.NET позволяет создавать компоненты, эффективно управляющие данными из нескольких источников данных.
 */
using System.Data;
using System.Data.SQLite;
/*
 * Пространство имен Microsoft.Win32 предоставляет два типа классов: те, которые обрабатывают события, 
 * вызванные операционной системой, и те, которые управляют системным реестром.
 */
using Microsoft.Win32;

// Сервисные функции

namespace Backup
{
    // Класс "Сервис"
    class Service
    {   
        // Название приложения
        const string ApplicationName = "Backup";

        // Запуск приложения вместе с Windows
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
                MessageBox.Show("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", "Нет доступа к реестру");
                return false;
            }
            return true;
        }

        public static string CurrentFileName = "default.scenario";
        // Cоединение с БД SQLite
        static SQLiteConnection m_dbConn;
        // Инструкция SQL для выполнения в БД SQLite
        static SQLiteCommand m_sqlCmd;

        // Сохранить сценарии в файл на диске
        static public void SaveListToText(ScenarioList list)
        {
            list.Save(CurrentFileName);
        }

        // Сохранить сценарии в SQLite
        static public void SaveListToSQLite(ScenarioList list)
        {
            string[] temp = new string[0];
            list.Save(ref temp);

            // Если база не существует - создать базу
            if (!File.Exists(CurrentFileName))
            {
                SQLiteConnection.CreateFile(CurrentFileName);
                SQLiteConnection.Shutdown(true, true);

                // Пересоздать таблицу
                m_dbConn = new SQLiteConnection("Data Source = " + CurrentFileName + "; Version = 3;");
                m_dbConn.Open();

                m_sqlCmd = new SQLiteCommand(m_dbConn);
                m_sqlCmd.Connection = m_dbConn;

                //m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Scenario (id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT)";
                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Scenario (id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT," +
                    "ScenarioName TEXT," +
                    "ScenarioType TEXT," +
                    "ScenarioPack TEXT," +
                    "ScenarioDestination TEXT" +
                    ")";

                m_sqlCmd.ExecuteNonQuery();
                m_dbConn.Close();
            }

            m_dbConn = new SQLiteConnection("Data Source = " + CurrentFileName + "; Version = 3;");
            m_dbConn.Open();

            // Очистить таблицу
            m_sqlCmd = new SQLiteCommand(m_dbConn);
            m_sqlCmd.Connection = m_dbConn;
            m_sqlCmd.CommandText = "DELETE FROM Scenario";
            m_sqlCmd.ExecuteNonQuery();

            // Сохранить все temp
            int index = 0;
            foreach (string s in temp)
            {
                //m_sqlCmd.CommandText = "INSERT INTO Scenario (Title) VALUES ('" + s + "')";
                m_sqlCmd.CommandText = "INSERT INTO Scenario " +
                    "(" +
                    "Title," +
                    "ScenarioName," +
                    "ScenarioType," +
                    "ScenarioPack," +
                    "ScenarioDestination" +
                    ") values (" +
                    "'" + s + "'," +
                    "'" + list.list[index].Title + "'," +
                    "'" + list.list[index].scenarioType.ToString() + "'," +
                    "'" + list.list[index].Zip.ToString() + "'," +
                    "'" + list.list[index].Destination + "'" +
                    ")";
                m_sqlCmd.ExecuteNonQuery();
            }
            m_dbConn.Close();
        }

        static public void SaveList(ScenarioList list, bool AsText)
        {
            if (AsText)
                SaveListToText(list);
            else
                SaveListToSQLite(list);
        }

        // Загрузить сценарии из файла на диске
        static public void LoadListFromText(ScenarioList list)
        {
            if (File.Exists(CurrentFileName))
                list.Load(CurrentFileName);
        }

        // Занрузить сценарии из SQLite
        static public void LoadListFromSQLite(ScenarioList list)
        {
            list.list.Clear();

            // Прочитать таблицу
            m_dbConn = new SQLiteConnection("Data Source = " + CurrentFileName + "; Version = 3;");
            m_dbConn.Open();
            m_sqlCmd = new SQLiteCommand(m_dbConn);
            m_sqlCmd.Connection = m_dbConn;
            string sqlQuery = "SELECT Title FROM Scenario";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            DataTable dTable = new DataTable();

            // Получить данные
            adapter.Fill(dTable);
            string[] temp = new string[dTable.Rows.Count];

            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                temp[i] = dTable.Rows[i].ItemArray[0].ToString();
            }
            list.Load(ref temp);
        }

        static public void LoadList(ScenarioList list, bool AsText)
        {
            list.list.Clear();
            if (AsText)
                LoadListFromText(list);
            else
                LoadListFromSQLite(list);
        }
    }
}
