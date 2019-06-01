using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Data.SQLite;

using System.IO.Compression;

namespace Backup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Service.SetAutorunValue(checkBoxAutoStart.Checked);
        }

        private void checkBoxYideToTray_CheckedChanged(object sender, EventArgs e)
        {
            // Если выбрать, то программа будет запускаться свернутая в значок в системном Tray
            notifyIcon.Visible = checkBoxYideToTray.Checked;
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            // Если нажать, то программа свернется в значок в системном Tray
            notifyIcon.Visible = true;
            if (checkBoxYideToTray.Checked)
                Hide();
            else
                WindowState = FormWindowState.Minimized;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            // А-ля "Help"
            Process.Start("Описание.html");
        }

        bool CheckValidPath(ref Scenario s)
        {
            //Типичные зловредности обычного пользователя
            if (s.Destination == "")
            {
                MessageBox.Show("Не задано место размещения резервной копии", s.Title);
                return false;
            }

            if (!s.Zip)
            {
                if (s.Destination[s.Destination.Length - 1] != '\\')
                    s.Destination += "\\";

                string Destination = s.Destination + MakeCopy.exclude(s.Title);

                if (Directory.Exists(Destination))
                    return true;

                try
                {
                    Directory.CreateDirectory(Destination);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Это какое-то неправильное место: " + Destination, s.Title);
                    return false;
                }
            }
            else
            {
                if (s.Destination[s.Destination.Length - 1] == '\\')
                {
                    MessageBox.Show("Не задан файл для размещения резервной копии", s.Title);
                    return false;
                }

                string Destination = Path.GetDirectoryName(s.Destination) + "\\" + MakeCopy.exclude(s.Title) + Path.GetFileName(s.Destination);

                if (File.Exists(Destination))
                    return true;

                try
                {
                    File.WriteAllText(Destination, "Test File Create");
                    File.Delete(Destination);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Это какой-то неправильный файл: " + Destination, s.Title);
                    return false;
                }
            }
        }

        bool NextStep = false;

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            // Произведено резервное копирование по выбранному сценарию
            if (listBoxScenario.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];

            labelState.Text = "Копирование " + s.Title;

            switch (s.scenarioType)
            {
                case ScenarioType.зеркальный:
                    MakeCopy.CopyMirror(ref s);
                    break;
                case ScenarioType.полный:
                    if (!MakeCopy.CopyFull(ref s)) MessageBox.Show("Копия не сделана");
                    break;
                case ScenarioType.инкрементальный:
                    MakeCopy.CopyIncremental(ref s);
                    break;
                case ScenarioType.дифференциальный:
                    MakeCopy.CopyDifferential(ref s);
                    break;
            };
            NextStep = true;
            list[listBoxScenario.SelectedIndex].LastTime = DateTime.Now;
            listBoxScenario_SelectedIndexChanged(null, null);
            labelState.Text = "Копирование завершено";

            // Добавить в журнал сообщение о копировании
            using (StreamWriter sw = new StreamWriter(new FileStream(textBoxLogFile.Text, FileMode.Append)))
            {
                foreach (string log in MakeCopy.Log) sw.WriteLine(log);
                MakeCopy.Log.Clear();
                sw.Write(DateTime.Now.ToString()+" ");
                sw.Write(s.ToString()+" ");
                sw.Write(" копирование завершено");
                sw.WriteLine();
            }

            if (checkBoxMail.Checked)
            try
            {
                Mailer.SendMail(
                    textBoxSMTP.Text,
                    textBoxSender.Text,
                    textBoxPassword.Text,
                    textBoxEMail.Text,
                    "Резервное копирование: " + DateTime.Now.ToString() + " создание копии",
                    s.ToString(),
                    textBoxLogFile.Text
                    );

                labelState.Text += Environment.NewLine + "Почта отправлена!";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            // Восстановление данных по выбранному сценарию
            if (listBoxScenario.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];

            labelState.Text = "Восстановление " + s.Title;

            switch (s.scenarioType)
            {
                case ScenarioType.зеркальный:
                    MakeCopy.RestoreMirror(ref s);
                    break;
                case ScenarioType.полный:
                    MakeCopy.RestoreFull(ref s);
                    break;
                case ScenarioType.инкрементальный:
                    MakeCopy.RestoreIncremental(ref s);
                    break;
                case ScenarioType.дифференциальный:
                    MakeCopy.RestoreDifferential(ref s);
                    break;
            };
            NextStep = true;
            labelState.Text = "Восстановление завершено";

            // Добавить в журнал сообщение о восстановлении
            using (StreamWriter sw = new StreamWriter(new FileStream(textBoxLogFile.Text, FileMode.Append)))
            {
                foreach (string log in MakeCopy.Log)
                    sw.WriteLine(log);
                MakeCopy.Log.Clear();
                sw.Write(DateTime.Now.ToString() + " ");
                sw.Write(s.ToString() + " ");
                sw.Write(" восстановление завершено");
                sw.WriteLine();
            }

            if (checkBoxMail.Checked)
                try
                {
                Mailer.SendMail(
                    textBoxSMTP.Text,
                    textBoxSender.Text,
                    textBoxPassword.Text,
                    textBoxEMail.Text,
                    "Резервное копирование: " + DateTime.Now.ToString()+" восстановление ",
                    s.ToString(),
                    textBoxLogFile.Text
                    );

                labelState.Text += Environment.NewLine + "Почта отправлена!";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        // Добавить файл
        private void buttonAddDataFile_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];

            foreach (string filename in openFileDialog.FileNames)
            { 
                s.Source.Add(filename);
                listBoxData.Items.Add(filename);
            }

            Service.SaveList(list, !checkBoxSQLite.Checked);

            NextStep = true;
        }

        // Добавить каталог
        private void buttonAddDataPath_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];

            string filename = "*" + folderBrowserDialog.SelectedPath;
            s.Source.Add(filename);
            listBoxData.Items.Add(filename);

            Service.SaveList(list, !checkBoxSQLite.Checked);

            NextStep = true;
        }

        // Удалить файл/каталог
        private void buttonRemoveData_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;
            if (listBoxData.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];

            s.Source.RemoveAt(listBoxData.SelectedIndex);
            listBoxData.Items.RemoveAt(listBoxData.SelectedIndex);

            Service.SaveList(list, !checkBoxSQLite.Checked);
        }

        // Выбрать место для хранения копии
        private void buttonDestination_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];
            FormSelectDestination FSD = new FormSelectDestination();
            FSD.textBox1.Text = s.Destination;
            FSD.scenario = s;
            FSD.ShowDialog();
            s.Destination = FSD.textBox1.Text;

            Service.SaveList(list, !checkBoxSQLite.Checked);

            NextStep = true;

            listBoxScenario_SelectedIndexChanged(sender, e);
        }

        // Расписание
        private void buttonShedule_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;

            FormShedule FS = new FormShedule();
            Scenario s = list[listBoxScenario.SelectedIndex];
            FS.listBox.Items.Clear();

            foreach (var x in s.Shedule)
                FS.listBox.Items.Add(x);

            if (FS.ShowDialog() != DialogResult.OK)
                return;

            s.Shedule.Clear();

            foreach (var x in FS.listBox.Items)
                s.Shedule.Add(x.ToString());

            Service.SaveList(list, !checkBoxSQLite.Checked);

            NextStep = true;
        }

        // Использовать архивирование
        private void checkBoxPack_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];
            bool wasZip = s.Zip;
            s.Zip = comboBoxZip.SelectedIndex > 0;
            ShowScenarioList();

            Service.SaveList(list, !checkBoxSQLite.Checked);

            NextStep = true;

            if (s.Zip != wasZip)
                MessageBox.Show("Измените место хранения!", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //-----------------------------------------------------------------------------------------//

        // Создать сценарий
        private void buttonAddScenario_Click(object sender, EventArgs e)
        {
            list.Add(new Scenario());
            ShowScenarioList();
            listBoxScenario.SelectedIndex = list.Count - 1;
        }

        // Удалить сценарий
        private void buttonDeleteScanario_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;

            list.RemoveAt(listBoxScenario.SelectedIndex);

            ShowScenarioList();

            if (list.Count == 0)
                listBoxScenario_SelectedIndexChanged(null, null);

            Service.SaveList(list, !checkBoxSQLite.Checked);
        }

        ScenarioList list = new ScenarioList();

        // Показать список сценариев
        void ShowScenarioList()
        {
            int index = listBoxScenario.SelectedIndex;

            listBoxScenario.Items.Clear();

            for (int k=0; k<list.Count; k++)
                listBoxScenario.Items.Add(list[k].ToString());

            try
            { 
                if (index >= 0) listBoxScenario.SelectedIndex = index;
            }
            catch
            {
                try
                {
                    listBoxScenario.SelectedIndex = index - 1;
                }
                catch
                {
                    listBoxScenario.SelectedIndex = -1;
                    listBoxScenario_SelectedIndexChanged(null, null);
                }
            }
        }

        //При запуске прогаммы
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadOptions();
            Service.LoadList(list, !checkBoxSQLite.Checked);
            ShowScenarioList();
            try
            {
                listBoxScenario.SelectedIndex = 0;
            }
            catch { };
        }

        private void listBoxScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Заполнить 
            if (listBoxScenario.SelectedIndex < 0)
            {
                textBoxScenarioTitle.Text = "";
                comboBoxScenarioType.SelectedIndex = 0;
                comboBoxZip.SelectedIndex = 0;
                labelDestination.Text = "";
                listBoxData.Items.Clear();
                return;
            }

            Scenario s = list[listBoxScenario.SelectedIndex];
            textBoxScenarioTitle.Text = s.Title;
            comboBoxScenarioType.SelectedIndex = (int)s.scenarioType;
            comboBoxZip.SelectedIndex = (s.Zip) ? 1 : 0;
            labelDestination.Text = s.Destination;

            if (s.LastTime.Year < 2000)
                labelLastTime.Text = " Сценарий не запускался";
            else
                labelLastTime.Text = s.LastTime.ToString();

            listBoxData.Items.Clear();

            foreach (string t in s.Sources)
                listBoxData.Items.Add(t);
        }

        private void textBoxScenarioTitle_TextChanged(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];
            s.Title = textBoxScenarioTitle.Text;
            ShowScenarioList();
        }

        private void comboBoxScenarioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0)
                return;

            Scenario s = list[listBoxScenario.SelectedIndex];
            s.scenarioType = (ScenarioType)comboBoxScenarioType.SelectedIndex;
            ShowScenarioList();

            Service.SaveList(list, !checkBoxSQLite.Checked);

            NextStep = true;
        }

        

        //// Мастер сценариев
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Подсказчик по созданию нового сценария");
        //    FormMaster FM = new FormMaster(); FM.Show();
        //    buttonAddScenario_Click(sender, e);
        //    Color Saved = textBoxScenarioTitle.BackColor;

        //    FM.label.Text = "Дайте название новому сценарию. Выберите тип сохранения";
        //    textBoxScenarioTitle.BackColor = Color.Red;
        //    comboBoxScenarioType.BackColor = Color.Red;
        //    NextStep = false;
        //    while (FM.Visible && !NextStep) Application.DoEvents();
        //    textBoxScenarioTitle.BackColor = Saved;
        //    comboBoxScenarioType.BackColor = Saved;

        //    FM.label.Text = "Выберите сжатие";
        //    comboBoxZip.BackColor = Color.Red;
        //    NextStep = false;
        //    while (FM.Visible && !NextStep) Application.DoEvents();
        //    comboBoxZip.BackColor = Saved;

        //    Saved = buttonDestination.BackColor;

        //    FM.label.Text = "Выберите место хранения";
        //    buttonDestination.BackColor = Color.Red;
        //    NextStep = false;
        //    while (FM.Visible && !NextStep) Application.DoEvents();
        //    buttonDestination.BackColor = Saved;

        //    FM.label.Text = "Настройте расписание копирования";
        //    buttonShedule.BackColor = Color.Red;
        //    NextStep = false;
        //    while (FM.Visible && !NextStep) Application.DoEvents();
        //    buttonShedule.BackColor = Saved;

        //    FM.label.Text = "Добавьте файлы и каталоги";
        //    buttonAddDataFile.BackColor = Color.Red;
        //    buttonAddDataPath.BackColor = Color.Red;
        //    NextStep = false;
        //    while (FM.Visible && !NextStep) Application.DoEvents();
        //    buttonAddDataFile.BackColor = Saved;
        //    buttonAddDataPath.BackColor = Saved;

        //    FM.label.Text = "Выполните сценарий";
        //    buttonCopy.BackColor = Color.Red;
        //    NextStep = false;
        //    while (FM.Visible && !NextStep) Application.DoEvents();
        //    buttonCopy.BackColor = Saved;

        //    FM.Visible = false;
        //}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveOptions();
        }

        private void textBoxScenarioTitle_Leave(object sender, EventArgs e)
        {
            Service.SaveList(list, !checkBoxSQLite.Checked);
        }

        //-----------------------------------------------------------------------------------------//

        // Сохранить настройки (которые на главной странице)
        public void SaveOptions()
        {
            // Сохранить чекбоксы и прочие настройки (автостарт, Tray)
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\BackupSoft\\");
            reg.SetValue("AutoStart", checkBoxAutoStart.Checked.ToString());
            reg.SetValue("HideToTray", checkBoxYideToTray.Checked.ToString());
            reg.SetValue("SQLite", checkBoxSQLite.Checked.ToString());
            reg.SetValue("ReportToMail", checkBoxMail.Checked.ToString());

            reg.SetValue("ScenarioName", textBoxScenarioName.Text);
            reg.SetValue("LogFile", textBoxLogFile.Text);
            reg.SetValue("MailTo", textBoxEMail.Text);
            reg.SetValue("SMTP", textBoxSMTP.Text);
            reg.SetValue("Sender", textBoxSender.Text);
            reg.SetValue("Password", textBoxPassword.Text);
            reg.Close();
        }

        // Загрузить настройки (которые на главной странице)
        public void LoadOptions()
        {
            Quiet = true;
            // Восстановить чекбоксы и прочие настройки (автостарт, Tray)
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\BackupSoft\\");
            checkBoxAutoStart.Checked = bool.Parse(reg.GetValue("AutoStart","False").ToString());
            checkBoxYideToTray.Checked = bool.Parse(reg.GetValue("HideToTray","True").ToString());

            checkBoxSQLite.Checked = bool.Parse(reg.GetValue("SQLite", "False").ToString());
            checkBoxMail.Checked = bool.Parse(reg.GetValue("ReportToMail", "False").ToString());

            textBoxScenarioName.Text = reg.GetValue("ScenarioName", "default.scenario").ToString();

            textBoxEMail.Text = reg.GetValue("MailTo", textBoxEMail.Text).ToString();
            textBoxLogFile.Text = reg.GetValue("LogFile", textBoxLogFile.Text).ToString();

            textBoxSMTP.Text = reg.GetValue("SMTP", textBoxSMTP.Text).ToString();
            textBoxSender.Text = reg.GetValue("Sender", textBoxSender.Text).ToString();
            textBoxPassword.Text = reg.GetValue("Password", textBoxPassword.Text).ToString();


            if (checkBoxAutoStart.Checked && checkBoxYideToTray.Checked)
                timerHide.Enabled = true;
            reg.Close();
            Quiet = false;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
            notifyIcon.Visible = checkBoxYideToTray.Checked;
        }

        private void timerHide_Tick(object sender, EventArgs e)
        {
            timerHide.Enabled = false;
            buttonHide_Click(sender, e);
        }

        private void timerShedule_Tick(object sender, EventArgs e)
        {
            // Каждые N секунд
            timerShedule.Enabled = false;
            // проверять, не готов ли какой-нибудь из сценариев к запуску
            for (int k = 0; k < list.Count; k++)
            {
                Scenario s = list.list[k];

                // Если готов 
                if (!s.CanStart())
                    continue;

                // Если открыто главное окно, то запросить подтверждение
                // Если подтверждено или главное окно не открыто - выбрать этот сценарий и выполнить его
                listBoxScenario.SelectedIndex = k;
                buttonCopy_Click(sender, e);
                // Отметить в сценарии дату и время последнего срабатывания
                s.LastTime = DateTime.Now;
                // Сохранить сценарии
                list.list[k] = s;
                Service.SaveList(list, !checkBoxSQLite.Checked);
            }
                timerShedule.Enabled = true;

        }


        //********** SQLite **********


        private void button2_Click(object sender, EventArgs e)
        {
            /*
            //Test SQLite
            string dbFileName = "default.sqlite";
            SQLiteConnection m_dbConn;// = new SQLiteConnection();
            SQLiteCommand m_sqlCmd;// = new SQLiteCommand(m_dbConn);

            //Создать базу
            if (!File.Exists(dbFileName))
            { 
                SQLiteConnection.CreateFile(dbFileName);
                SQLiteConnection.Shutdown(true, true);
                return;
            }

            //Подключиться
            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();

            //Выполнить "создание таблицы"
            m_sqlCmd = new SQLiteCommand(m_dbConn);
            m_sqlCmd.Connection = m_dbConn;
            m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Scenario (id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT)";
            m_sqlCmd.ExecuteNonQuery();

            //Вставить пару строчек
            m_sqlCmd.CommandText = "Insert into Scenario (Title) values ('Scenario one')";
            m_sqlCmd.ExecuteNonQuery();

            m_sqlCmd.CommandText = "Insert into Scenario (Title) values ('Сценарий два')";
            m_sqlCmd.ExecuteNonQuery();

            //Отключиться
            m_dbConn.Close();

            m_dbConn.Open();
            string sqlQuery = "SELECT Title FROM Scenario";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);

            DataTable dTable = new DataTable();
            //Получили данные
            adapter.Fill(dTable);
            listBox1.Items.Clear();
            for (int i = 0; i < dTable.Rows.Count; i++)
            { 
                listBox1.Items.Add(dTable.Rows[i].ItemArray[0].ToString());
            }
            */
        }

        private void buttonSelectScenario_Click(object sender, EventArgs e)
        {
            if (openFileDialogScenario.ShowDialog() != DialogResult.OK)
                return;
            textBoxScenarioName.Text = openFileDialogScenario.FileName;
            SaveOptions();
            Service.LoadList(list, !checkBoxSQLite.Checked);
            ShowScenarioList();
            try
            {
                listBoxScenario.SelectedIndex = 0;
            }
            catch { };
        }

        bool Quiet = false;

        private void checkBoxSQLite_CheckedChanged(object sender, EventArgs e)
        {
            if (!Quiet)
                buttonSelectScenario_Click(sender, e);
        }

        private void textBoxScenarioName_TextChanged(object sender, EventArgs e)
        {
            Service.CurrentFileName = textBoxScenarioName.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Проверить существование, если нет, то создать
            ZipArchive zip;
            ZipArchiveEntry entry;
            Stream stream;
            byte[] buffer = new byte[10];

            if (!File.Exists("test.zip"))
            {
                zip = ZipFile.Open("test.zip", ZipArchiveMode.Create);
                entry = zip.CreateEntry("file.txt");
                stream = entry.Open();
                for (byte i = 0; i < buffer.Length; i++)
                    buffer[i] = i;
                stream.Write(buffer, 0, 10);
                stream.Close();
                zip.Dispose();
            }
            // По любому - открыть
            zip = ZipFile.Open("test.zip",ZipArchiveMode.Update);
            entry = zip.GetEntry("file.txt");
            stream = entry.Open();
            stream.Read(buffer, 0, buffer.Length);
            zip.Dispose();
        }

        private void checkBoxMail_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMail.Checked)
            {
                textBoxEMail.Enabled = false;
                textBoxSMTP.Enabled = false;
                textBoxSender.Enabled = false;
                textBoxPassword.Enabled = false;
                textBoxLogFile.Enabled = false;
            }
            else
            {
                textBoxEMail.Enabled = true;
                textBoxSMTP.Enabled = true;
                textBoxSender.Enabled = true;
                textBoxPassword.Enabled = true;
                textBoxLogFile.Enabled = true;
            }
        }
    }
}