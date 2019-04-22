using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Backup
{
    public partial class FormShedule : Form
    {
        public FormShedule()
        {
            InitializeComponent();
        }

        private void checkBoxLimitDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerDateFrom.Visible = !checkBoxLimitDate.Checked;
            dateTimePickerDateTo.Visible = !checkBoxLimitDate.Checked;
            labelDateFrom.Visible = !checkBoxLimitDate.Checked;
            labelDateTo.Visible = !checkBoxLimitDate.Checked;
        }

        private void checkBoxLimitTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerTimeFrom.Visible = !checkBoxLimitTime.Checked;
            dateTimePickerTimeTo.Visible = !checkBoxLimitTime.Checked;
            labelTimeFrom.Visible = !checkBoxLimitTime.Checked;
            labelTimeTo.Visible = !checkBoxLimitTime.Checked;
        }

        private void FormShedule_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
        }

        string FromFields()
        {
            string result = comboBoxPeriod.Text+"\t";
            if (!checkBoxLimitDate.Checked)
            {
                result += "Даты с " + dateTimePickerDateFrom.Text + " по " + dateTimePickerDateTo.Text+"\t";
            }
            else
            {
                result += "\t";
            }

            if (!checkBoxLimitTime.Checked)
            {
                result += "Время с " + dateTimePickerTimeFrom.Text + " по " + dateTimePickerTimeTo.Text + "\t";
            }
            else
            {
                result += "\t";
            }
            return result;
        }

        void ToFields(string value)
        {
            string[] ss = value.Split('\t');
            comboBoxPeriod.Text = ss[0];
            if (ss[1] != "")
            {
                string[] sss = ss[1].Split(' ');
                dateTimePickerDateFrom.Text = sss[2];
                dateTimePickerDateTo.Text = sss[4];
                checkBoxLimitDate.Checked = false;
            }
            else checkBoxLimitDate.Checked = true;

            if (ss[2]!="")
            {
                string[] sss = ss[2].Split(' ');
                dateTimePickerTimeFrom.Text = sss[2];
                dateTimePickerTimeTo.Text = sss[4];
                checkBoxLimitTime.Checked = false;
            }
            else checkBoxLimitTime.Checked = true;

        }

        private void buttonAppend_Click(object sender, EventArgs e)
        {
            //Сформировать правило и записать в listBox
            if (comboBoxPeriod.SelectedIndex<0)
            {
                MessageBox.Show("Не выбран период");
                return;
            }
            listBox.Items.Add(FromFields());
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0) return;
            ToFields(listBox.Items[listBox.SelectedIndex].ToString());
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0) return;
            listBox.Items[listBox.SelectedIndex] = FromFields();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0) return;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }
    }
}
