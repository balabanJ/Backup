using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

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
            //MessageBox.Show("Если выбрать, то программа будет запускаться при старте операционной системы", "Справка");
            if (checkBoxAutoStart.Checked)
                Service.SetAutoStart();
            else
                Service.RemoveAutoStart();
        }

        private void checkBoxYideToTray_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Если выбрать, то программа будет запускаться свернутая в значок в системном Tray", "Справка");
            if (checkBoxYideToTray.Checked)
                Service.ActivateTray();
            else
                Service.DeactivateTray();
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то программа свернется в значок в системном Tray", "Справка");
            Service.ActivateTray();
            Service.Minimize();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            //Help такой
            Process.Start("Описание.html");
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            ///!!!
            MessageBox.Show("Произведено резервное копирование по выбранному сценарию", "Справка");
            NextStep = true;
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            ///!!!
            MessageBox.Show("Восстановление данных по выбранному сценарию", "Справка");
            NextStep = true;
        }

        private void buttonAddDataFile_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то добавится выбранный файл", "Справка");
            if (listBoxScenario.SelectedIndex < 0) return;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            foreach (string filename in openFileDialog.FileNames)
            { 
                s.Source.Add(filename);
                listBoxData.Items.Add(filename);
            }

            Service.SaveList(list);
            NextStep = true;
        }

        private void buttonAddDataPath_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то добавится выбранный подкаталог (папка, путь)", "Справка");
            if (listBoxScenario.SelectedIndex < 0) return;
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            string filename = "*" + folderBrowserDialog.SelectedPath;
            s.Source.Add(filename);
            listBoxData.Items.Add(filename);
            Service.SaveList(list);
            NextStep = true;
        }

        private void buttonRemoveData_Click(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0) return;
            //MessageBox.Show("Если нажать, то удалится выбранный элемент копирования", "Справка");
            Scenario s = list[listBoxScenario.SelectedIndex];
            s.Source.RemoveAt(listBoxData.SelectedIndex);
            listBoxData.Items.RemoveAt(listBoxData.SelectedIndex);
            Service.SaveList(list);
        }

        private void buttonDestination_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то будет выбрано место для хранения резервной копии", "Справка");
            if (listBoxScenario.SelectedIndex < 0) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            FormSelectDestination FSD = new FormSelectDestination();
            FSD.textBox1.Text = s.Destination;
            FSD.ShowDialog();
            s.Destination = FSD.textBox1.Text;
            Service.SaveList(list);
            NextStep = true;
        }

        private void buttonShedule_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то будет запущен редактор расписания", "Справка");
            if (listBoxScenario.SelectedIndex < 0) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            FormShedule FS = new FormShedule();
            FS.listBox.Items.Clear();
            foreach (var x in s.Shedule)
                FS.listBox.Items.Add(x);
            FS.ShowDialog();
            s.Shedule.Clear();
            foreach (var x in FS.listBox.Items)
                FS.listBox.Items.Add(x.ToString());
            Service.SaveList(list);
            NextStep = true;
        }

        private void checkBoxPack_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Если выбрать, то будет использовано архивирование", "Справка");
            if (listBoxScenario.SelectedIndex < 0) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            s.Zip = comboBoxZip.SelectedIndex > 0;
            ShowScenarioList();
            Service.SaveList(list);
            NextStep = true;
        }

        private void buttonAddScenario_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то будет создан новый сценарий", "Справка");
            list.Add(new Scenario());
            ShowScenarioList();
            listBoxScenario.SelectedIndex = list.Count - 1;
        }

        private void buttonDeleteScanario_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Если нажать, то будет удален выбранный сценарий", "Справка");
            if (listBoxScenario.SelectedIndex < 0) return;
            list.RemoveAt(listBoxScenario.SelectedIndex);
            ShowScenarioList();
            if (list.Count == 0)
                listBoxScenario_SelectedIndexChanged(null, null);
            Service.SaveList(list);
        }

        ScenarioList list = new ScenarioList();

        void ShowScenarioList()
        {
            int index = listBoxScenario.SelectedIndex;
            listBoxScenario.Items.Clear();
            for (int k=0; k<list.Count; k++)
                listBoxScenario.Items.Add(list[k].ToString());
            try { 
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

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadOptions();
            Service.LoadList(list);
            ShowScenarioList();
            listBoxScenario.SelectedIndex = 0;
        }

        private void listBoxScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Заполнить 
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
            listBoxData.Items.Clear();
            foreach (string t in s.Sources)
                listBoxData.Items.Add(t);
        }

        private void textBoxScenarioTitle_TextChanged(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            s.Title = textBoxScenarioTitle.Text;
            ShowScenarioList();
        }

        private void comboBoxScenarioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxScenario.SelectedIndex < 0) return;
            Scenario s = list[listBoxScenario.SelectedIndex];
            s.scenarioType = (ScenarioType)comboBoxScenarioType.SelectedIndex;
            ShowScenarioList();
            Service.SaveList(list);
            NextStep = true;
        }

        bool NextStep = false;

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Подсказчик по созданию нового сценария");
            FormMaster FM = new FormMaster(); FM.Show();
            buttonAddScenario_Click(sender, e);
            Color Saved = textBoxScenarioTitle.BackColor;
            FM.label.Text = "Дайте название новому сценарию. Выберите тип сохранения";
            textBoxScenarioTitle.BackColor = Color.Red;
            comboBoxScenarioType.BackColor = Color.Red;
            NextStep = false;
            while (FM.Visible && !NextStep) Application.DoEvents();
            textBoxScenarioTitle.BackColor = Saved;
            comboBoxScenarioType.BackColor = Saved;

            FM.label.Text = "Выберите сжатие";
            comboBoxZip.BackColor = Color.Red;
            NextStep = false;
            while (FM.Visible && !NextStep) Application.DoEvents();
            comboBoxZip.BackColor = Saved;

            Saved = buttonDestination.BackColor;

            FM.label.Text = "Выберите место хранения";
            buttonDestination.BackColor = Color.Red;
            NextStep = false;
            while (FM.Visible && !NextStep) Application.DoEvents();
            buttonDestination.BackColor = Saved;

            FM.label.Text = "Настройте расписание копирования";
            buttonShedule.BackColor = Color.Red;
            NextStep = false;
            while (FM.Visible && !NextStep) Application.DoEvents();
            buttonShedule.BackColor = Saved;

            FM.label.Text = "Добавьте файлы и каталоги";
            buttonAddDataFile.BackColor = Color.Red;
            buttonAddDataPath.BackColor = Color.Red;
            NextStep = false;
            while (FM.Visible && !NextStep) Application.DoEvents();
            buttonAddDataFile.BackColor = Saved;
            buttonAddDataPath.BackColor = Saved;

            FM.label.Text = "Выполните сценарий";
            buttonCopy.BackColor = Color.Red;
            NextStep = false;
            while (FM.Visible && !NextStep) Application.DoEvents();
            buttonCopy.BackColor = Saved;

            FM.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveOptions();
        }

        private void textBoxScenarioTitle_Leave(object sender, EventArgs e)
        {
            Service.SaveList(list);
        }

        public void SaveOptions()
        {
            ///!!! Сохранить чекбоксы и прочие настройки
        }

        public void LoadOptions()
        {
            ///!!! Восстановить чекбоксы и прочие настройки
        }


    }
}
