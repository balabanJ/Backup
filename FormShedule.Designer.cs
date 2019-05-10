namespace Backup
{
    partial class FormShedule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox = new System.Windows.Forms.ListBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.comboBoxPeriod = new System.Windows.Forms.ComboBox();
            this.dateTimePickerDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeTo = new System.Windows.Forms.DateTimePicker();
            this.buttonAppend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDateFrom = new System.Windows.Forms.Label();
            this.labelTimeFrom = new System.Windows.Forms.Label();
            this.labelTimeTo = new System.Windows.Forms.Label();
            this.dateTimePickerDateTo = new System.Windows.Forms.DateTimePicker();
            this.labelDateTo = new System.Windows.Forms.Label();
            this.checkBoxLimitDate = new System.Windows.Forms.CheckBox();
            this.checkBoxLimitTime = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(21, 22);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(519, 212);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(556, 251);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Удалить";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(648, 222);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(75, 23);
            this.buttonReplace.TabIndex = 2;
            this.buttonReplace.Text = "Заменить";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // comboBoxPeriod
            // 
            this.comboBoxPeriod.FormattingEnabled = true;
            this.comboBoxPeriod.Items.AddRange(new object[] {
            "Каждый час",
            "Каждый день",
            "Каждую неделю",
            "Каждый месяц",
            "Каждый год"});
            this.comboBoxPeriod.Location = new System.Drawing.Point(556, 22);
            this.comboBoxPeriod.Name = "comboBoxPeriod";
            this.comboBoxPeriod.Size = new System.Drawing.Size(167, 21);
            this.comboBoxPeriod.TabIndex = 3;
            // 
            // dateTimePickerDateFrom
            // 
            this.dateTimePickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateFrom.Location = new System.Drawing.Point(628, 72);
            this.dateTimePickerDateFrom.Name = "dateTimePickerDateFrom";
            this.dateTimePickerDateFrom.Size = new System.Drawing.Size(84, 20);
            this.dateTimePickerDateFrom.TabIndex = 4;
            this.dateTimePickerDateFrom.Visible = false;
            // 
            // dateTimePickerTimeFrom
            // 
            this.dateTimePickerTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeFrom.Location = new System.Drawing.Point(637, 159);
            this.dateTimePickerTimeFrom.Name = "dateTimePickerTimeFrom";
            this.dateTimePickerTimeFrom.ShowUpDown = true;
            this.dateTimePickerTimeFrom.Size = new System.Drawing.Size(75, 20);
            this.dateTimePickerTimeFrom.TabIndex = 5;
            this.dateTimePickerTimeFrom.Visible = false;
            // 
            // dateTimePickerTimeTo
            // 
            this.dateTimePickerTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeTo.Location = new System.Drawing.Point(637, 185);
            this.dateTimePickerTimeTo.Name = "dateTimePickerTimeTo";
            this.dateTimePickerTimeTo.ShowUpDown = true;
            this.dateTimePickerTimeTo.Size = new System.Drawing.Size(75, 20);
            this.dateTimePickerTimeTo.TabIndex = 6;
            this.dateTimePickerTimeTo.Visible = false;
            // 
            // buttonAppend
            // 
            this.buttonAppend.Location = new System.Drawing.Point(556, 222);
            this.buttonAppend.Name = "buttonAppend";
            this.buttonAppend.Size = new System.Drawing.Size(75, 23);
            this.buttonAppend.TabIndex = 7;
            this.buttonAppend.Text = "Добавить";
            this.buttonAppend.UseVisualStyleBackColor = true;
            this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(553, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Периодичность";
            // 
            // labelDateFrom
            // 
            this.labelDateFrom.AutoSize = true;
            this.labelDateFrom.Location = new System.Drawing.Point(553, 76);
            this.labelDateFrom.Name = "labelDateFrom";
            this.labelDateFrom.Size = new System.Drawing.Size(46, 13);
            this.labelDateFrom.TabIndex = 9;
            this.labelDateFrom.Text = "Срок от";
            this.labelDateFrom.Visible = false;
            // 
            // labelTimeFrom
            // 
            this.labelTimeFrom.AutoSize = true;
            this.labelTimeFrom.Location = new System.Drawing.Point(565, 160);
            this.labelTimeFrom.Name = "labelTimeFrom";
            this.labelTimeFrom.Size = new System.Drawing.Size(59, 13);
            this.labelTimeFrom.TabIndex = 10;
            this.labelTimeFrom.Text = "Начиная с";
            this.labelTimeFrom.Visible = false;
            // 
            // labelTimeTo
            // 
            this.labelTimeTo.AutoSize = true;
            this.labelTimeTo.Location = new System.Drawing.Point(553, 189);
            this.labelTimeTo.Name = "labelTimeTo";
            this.labelTimeTo.Size = new System.Drawing.Size(78, 13);
            this.labelTimeTo.TabIndex = 11;
            this.labelTimeTo.Text = "Не позже чем";
            this.labelTimeTo.Visible = false;
            // 
            // dateTimePickerDateTo
            // 
            this.dateTimePickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateTo.Location = new System.Drawing.Point(628, 98);
            this.dateTimePickerDateTo.Name = "dateTimePickerDateTo";
            this.dateTimePickerDateTo.Size = new System.Drawing.Size(84, 20);
            this.dateTimePickerDateTo.TabIndex = 4;
            this.dateTimePickerDateTo.Visible = false;
            // 
            // labelDateTo
            // 
            this.labelDateTo.AutoSize = true;
            this.labelDateTo.Location = new System.Drawing.Point(553, 102);
            this.labelDateTo.Name = "labelDateTo";
            this.labelDateTo.Size = new System.Drawing.Size(47, 13);
            this.labelDateTo.TabIndex = 9;
            this.labelDateTo.Text = "Срок до";
            this.labelDateTo.Visible = false;
            // 
            // checkBoxLimitDate
            // 
            this.checkBoxLimitDate.AutoSize = true;
            this.checkBoxLimitDate.Checked = true;
            this.checkBoxLimitDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLimitDate.Location = new System.Drawing.Point(556, 49);
            this.checkBoxLimitDate.Name = "checkBoxLimitDate";
            this.checkBoxLimitDate.Size = new System.Drawing.Size(145, 17);
            this.checkBoxLimitDate.TabIndex = 12;
            this.checkBoxLimitDate.Text = "Без ограничений срока";
            this.checkBoxLimitDate.UseVisualStyleBackColor = true;
            this.checkBoxLimitDate.CheckedChanged += new System.EventHandler(this.checkBoxLimitDate_CheckedChanged);
            // 
            // checkBoxLimitTime
            // 
            this.checkBoxLimitTime.AutoSize = true;
            this.checkBoxLimitTime.Checked = true;
            this.checkBoxLimitTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLimitTime.Location = new System.Drawing.Point(556, 136);
            this.checkBoxLimitTime.Name = "checkBoxLimitTime";
            this.checkBoxLimitTime.Size = new System.Drawing.Size(103, 17);
            this.checkBoxLimitTime.TabIndex = 12;
            this.checkBoxLimitTime.Text = "В любое время";
            this.checkBoxLimitTime.UseVisualStyleBackColor = true;
            this.checkBoxLimitTime.CheckedChanged += new System.EventHandler(this.checkBoxLimitTime_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(21, 250);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(519, 23);
            this.buttonOK.TabIndex = 13;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormShedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 285);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxLimitTime);
            this.Controls.Add(this.checkBoxLimitDate);
            this.Controls.Add(this.labelTimeTo);
            this.Controls.Add(this.labelTimeFrom);
            this.Controls.Add(this.labelDateTo);
            this.Controls.Add(this.labelDateFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAppend);
            this.Controls.Add(this.dateTimePickerTimeTo);
            this.Controls.Add(this.dateTimePickerTimeFrom);
            this.Controls.Add(this.dateTimePickerDateTo);
            this.Controls.Add(this.dateTimePickerDateFrom);
            this.Controls.Add(this.comboBoxPeriod);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.listBox);
            this.Name = "FormShedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расписание ";
            this.Load += new System.EventHandler(this.FormShedule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.ComboBox comboBoxPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeTo;
        private System.Windows.Forms.Button buttonAppend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDateFrom;
        private System.Windows.Forms.Label labelTimeFrom;
        private System.Windows.Forms.Label labelTimeTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateTo;
        private System.Windows.Forms.Label labelDateTo;
        private System.Windows.Forms.CheckBox checkBoxLimitDate;
        private System.Windows.Forms.CheckBox checkBoxLimitTime;
        public System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button buttonOK;
    }
}