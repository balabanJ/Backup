using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Backup
{
    public partial class FormSelectDestination : Form
    {
        public Scenario scenario;

        public FormSelectDestination()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (scenario.Zip && scenario.scenarioType == ScenarioType.зеркальный)
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                textBox1.Text = openFileDialog.FileName;
            }
            else
            {
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
                textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void FormSelectDestination_Load(object sender, EventArgs e)
        {

        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!scenario.Zip)
                if (!Directory.Exists(textBox1.Text))
                    Directory.CreateDirectory(textBox1.Text);

            Process.Start(textBox1.Text);
        }
    }
}
