namespace Backup
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /* 
         * Предоставляет функциональные возможности для контейнеров. 
         * Контейнеры являются объектами, логически включающими ноль и более компонентов.
         */
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /* 
         * Реализация метода Dispose необходима для освобождения неуправляемых ресурсов, которые использует ваше приложение. 
         * Сборщик мусора .NET не выделяет и не освобождает неуправляемую память.
         * disposing используется параметр типа Boolean, который указывает, откуда осуществляется вызов метода: 
         * из метода Dispose (значение true) или из метода завершения (значение false).
         */
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
        /* Загружает откомпилированную страницу компонента */
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonHide = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.checkBoxYideToTray = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelState = new System.Windows.Forms.Label();
            this.labelLastTime = new System.Windows.Forms.Label();
            this.comboBoxZip = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxScenarioTitle = new System.Windows.Forms.TextBox();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonShedule = new System.Windows.Forms.Button();
            this.buttonRemoveData = new System.Windows.Forms.Button();
            this.buttonAddDataPath = new System.Windows.Forms.Button();
            this.buttonAddDataFile = new System.Windows.Forms.Button();
            this.buttonDeleteScanario = new System.Windows.Forms.Button();
            this.buttonAddScenario = new System.Windows.Forms.Button();
            this.buttonDestination = new System.Windows.Forms.Button();
            this.labelDestination = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxScenarioType = new System.Windows.Forms.ComboBox();
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.listBoxScenario = new System.Windows.Forms.ListBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.timerShedule = new System.Windows.Forms.Timer(this.components);
            this.checkBoxSQLite = new System.Windows.Forms.CheckBox();
            this.textBoxScenarioName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelectScenario = new System.Windows.Forms.Button();
            this.openFileDialogScenario = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top 
                | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(908, 419);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonSelectScenario);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxScenarioName);
            this.tabPage1.Controls.Add(this.checkBoxSQLite);
            this.tabPage1.Controls.Add(this.buttonExit);
            this.tabPage1.Controls.Add(this.buttonHide);
            this.tabPage1.Controls.Add(this.buttonHelp);
            this.tabPage1.Controls.Add(this.checkBoxYideToTray);
            this.tabPage1.Controls.Add(this.checkBoxAutoStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(900, 393);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Общие";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(13, 127);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(123, 23);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Закончить";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(13, 94);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(123, 23);
            this.buttonHide.TabIndex = 3;
            this.buttonHide.Text = "Свернуть";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.Location = new System.Drawing.Point(819, 17);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonHelp.TabIndex = 2;
            this.buttonHelp.Text = "Помощь";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // checkBoxYideToTray
            // 
            this.checkBoxYideToTray.AutoSize = true;
            this.checkBoxYideToTray.Location = new System.Drawing.Point(12, 43);
            this.checkBoxYideToTray.Name = "checkBoxYideToTray";
            this.checkBoxYideToTray.Size = new System.Drawing.Size(124, 17);
            this.checkBoxYideToTray.TabIndex = 1;
            this.checkBoxYideToTray.Text = "Сворачивать в Tray";
            this.checkBoxYideToTray.UseVisualStyleBackColor = true;
            this.checkBoxYideToTray.CheckedChanged += new System.EventHandler(this.checkBoxYideToTray_CheckedChanged);
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.AutoSize = true;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(13, 17);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(175, 17);
            this.checkBoxAutoStart.TabIndex = 0;
            this.checkBoxAutoStart.Text = "Запускать вместе с Windows";
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelState);
            this.tabPage2.Controls.Add(this.labelLastTime);
            this.tabPage2.Controls.Add(this.comboBoxZip);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.textBoxScenarioTitle);
            this.tabPage2.Controls.Add(this.buttonRestore);
            this.tabPage2.Controls.Add(this.buttonCopy);
            this.tabPage2.Controls.Add(this.buttonShedule);
            this.tabPage2.Controls.Add(this.buttonRemoveData);
            this.tabPage2.Controls.Add(this.buttonAddDataPath);
            this.tabPage2.Controls.Add(this.buttonAddDataFile);
            this.tabPage2.Controls.Add(this.buttonDeleteScanario);
            this.tabPage2.Controls.Add(this.buttonAddScenario);
            this.tabPage2.Controls.Add(this.buttonDestination);
            this.tabPage2.Controls.Add(this.labelDestination);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.comboBoxScenarioType);
            this.tabPage2.Controls.Add(this.listBoxData);
            this.tabPage2.Controls.Add(this.listBoxScenario);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(900, 393);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сценарии";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelState
            // 
            this.labelState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelState.AutoSize = true;
            this.labelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelState.Location = new System.Drawing.Point(659, 94);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(139, 13);
            this.labelState.TabIndex = 11;
            this.labelState.Text = "Ничего не происходит";
            // 
            // labelLastTime
            // 
            this.labelLastTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLastTime.AutoSize = true;
            this.labelLastTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLastTime.Location = new System.Drawing.Point(5, 349);
            this.labelLastTime.Name = "labelLastTime";
            this.labelLastTime.Size = new System.Drawing.Size(170, 13);
            this.labelLastTime.TabIndex = 11;
            this.labelLastTime.Text = "Время последнего запуска";
            // 
            // comboBoxZip
            // 
            this.comboBoxZip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZip.FormattingEnabled = true;
            this.comboBoxZip.Items.AddRange(new object[] {
            "Без сжатия",
            "Сжимать"});
            this.comboBoxZip.Location = new System.Drawing.Point(245, 94);
            this.comboBoxZip.Name = "comboBoxZip";
            this.comboBoxZip.Size = new System.Drawing.Size(141, 21);
            this.comboBoxZip.TabIndex = 10;
            this.comboBoxZip.SelectedIndexChanged += new System.EventHandler(this.checkBoxPack_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(244, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Мастер сценариев";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxScenarioTitle
            // 
            this.textBoxScenarioTitle.Location = new System.Drawing.Point(244, 43);
            this.textBoxScenarioTitle.Name = "textBoxScenarioTitle";
            this.textBoxScenarioTitle.Size = new System.Drawing.Size(139, 20);
            this.textBoxScenarioTitle.TabIndex = 8;
            this.textBoxScenarioTitle.TextChanged += new System.EventHandler(this.textBoxScenarioTitle_TextChanged);
            this.textBoxScenarioTitle.Leave += new System.EventHandler(this.textBoxScenarioTitle_Leave);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestore.Location = new System.Drawing.Point(757, 55);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(134, 23);
            this.buttonRestore.TabIndex = 7;
            this.buttonRestore.Text = "Восстановить из копии";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(757, 26);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(134, 23);
            this.buttonCopy.TabIndex = 7;
            this.buttonCopy.Text = "Сделать копию";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonShedule
            // 
            this.buttonShedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShedule.Location = new System.Drawing.Point(246, 190);
            this.buttonShedule.Name = "buttonShedule";
            this.buttonShedule.Size = new System.Drawing.Size(137, 23);
            this.buttonShedule.TabIndex = 7;
            this.buttonShedule.Text = "Настроить расписание";
            this.buttonShedule.UseVisualStyleBackColor = true;
            this.buttonShedule.Click += new System.EventHandler(this.buttonShedule_Click);
            // 
            // buttonRemoveData
            // 
            this.buttonRemoveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveData.Location = new System.Drawing.Point(644, 329);
            this.buttonRemoveData.Name = "buttonRemoveData";
            this.buttonRemoveData.Size = new System.Drawing.Size(111, 23);
            this.buttonRemoveData.TabIndex = 7;
            this.buttonRemoveData.Text = "Удалить";
            this.buttonRemoveData.UseVisualStyleBackColor = true;
            this.buttonRemoveData.Click += new System.EventHandler(this.buttonRemoveData_Click);
            // 
            // buttonAddDataPath
            // 
            this.buttonAddDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddDataPath.Location = new System.Drawing.Point(644, 300);
            this.buttonAddDataPath.Name = "buttonAddDataPath";
            this.buttonAddDataPath.Size = new System.Drawing.Size(111, 23);
            this.buttonAddDataPath.TabIndex = 7;
            this.buttonAddDataPath.Text = "Добавить путь";
            this.buttonAddDataPath.UseVisualStyleBackColor = true;
            this.buttonAddDataPath.Click += new System.EventHandler(this.buttonAddDataPath_Click);
            // 
            // buttonAddDataFile
            // 
            this.buttonAddDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddDataFile.Location = new System.Drawing.Point(644, 271);
            this.buttonAddDataFile.Name = "buttonAddDataFile";
            this.buttonAddDataFile.Size = new System.Drawing.Size(111, 23);
            this.buttonAddDataFile.TabIndex = 7;
            this.buttonAddDataFile.Text = "Добавить файл";
            this.buttonAddDataFile.UseVisualStyleBackColor = true;
            this.buttonAddDataFile.Click += new System.EventHandler(this.buttonAddDataFile_Click);
            // 
            // buttonDeleteScanario
            // 
            this.buttonDeleteScanario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteScanario.Location = new System.Drawing.Point(245, 319);
            this.buttonDeleteScanario.Name = "buttonDeleteScanario";
            this.buttonDeleteScanario.Size = new System.Drawing.Size(141, 23);
            this.buttonDeleteScanario.TabIndex = 6;
            this.buttonDeleteScanario.Text = "Удалить сценарий";
            this.buttonDeleteScanario.UseVisualStyleBackColor = true;
            this.buttonDeleteScanario.Click += new System.EventHandler(this.buttonDeleteScanario_Click);
            // 
            // buttonAddScenario
            // 
            this.buttonAddScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddScenario.Location = new System.Drawing.Point(244, 290);
            this.buttonAddScenario.Name = "buttonAddScenario";
            this.buttonAddScenario.Size = new System.Drawing.Size(141, 23);
            this.buttonAddScenario.TabIndex = 5;
            this.buttonAddScenario.Text = "Добавить сценарий";
            this.buttonAddScenario.UseVisualStyleBackColor = true;
            this.buttonAddScenario.Click += new System.EventHandler(this.buttonAddScenario_Click);
            // 
            // buttonDestination
            // 
            this.buttonDestination.Location = new System.Drawing.Point(246, 119);
            this.buttonDestination.Name = "buttonDestination";
            this.buttonDestination.Size = new System.Drawing.Size(137, 49);
            this.buttonDestination.TabIndex = 4;
            this.buttonDestination.Text = "Выбрать место хранения";
            this.buttonDestination.UseVisualStyleBackColor = true;
            this.buttonDestination.Click += new System.EventHandler(this.buttonDestination_Click);
            // 
            // labelDestination
            // 
            this.labelDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDestination.AutoSize = true;
            this.labelDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDestination.Location = new System.Drawing.Point(6, 368);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(452, 13);
            this.labelDestination.TabIndex = 3;
            this.labelDestination.Text = "Место хранения - здесь будет показано место хранения резервной копии";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Резервируемые данные";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Сценарии";
            // 
            // comboBoxScenarioType
            // 
            this.comboBoxScenarioType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScenarioType.FormattingEnabled = true;
            this.comboBoxScenarioType.Items.AddRange(new object[] {
            "Зеркальный",
            "Инкрементальный",
            "Дифференциальный",
            "Полный"});
            this.comboBoxScenarioType.Location = new System.Drawing.Point(244, 69);
            this.comboBoxScenarioType.Name = "comboBoxScenarioType";
            this.comboBoxScenarioType.Size = new System.Drawing.Size(141, 21);
            this.comboBoxScenarioType.TabIndex = 2;
            this.comboBoxScenarioType.SelectedIndexChanged += new System.EventHandler(this.comboBoxScenarioType_SelectedIndexChanged);
            // 
            // listBoxData
            // 
            this.listBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.Location = new System.Drawing.Point(412, 22);
            this.listBoxData.Name = "listBoxData";
            this.listBoxData.Size = new System.Drawing.Size(226, 329);
            this.listBoxData.TabIndex = 0;
            // 
            // listBoxScenario
            // 
            this.listBoxScenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxScenario.FormattingEnabled = true;
            this.listBoxScenario.Location = new System.Drawing.Point(3, 26);
            this.listBoxScenario.Name = "listBoxScenario";
            this.listBoxScenario.Size = new System.Drawing.Size(226, 316);
            this.listBoxScenario.TabIndex = 0;
            this.listBoxScenario.SelectedIndexChanged += new System.EventHandler(this.listBoxScenario_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "*.*";
            this.openFileDialog.Filter = "Все файлы|*.*";
            this.openFileDialog.Multiselect = true;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Backup";
            this.notifyIcon.BalloonTipTitle = "Backup";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Backup";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // timerHide
            // 
            this.timerHide.Interval = 300;
            this.timerHide.Tick += new System.EventHandler(this.timerHide_Tick);
            // 
            // timerShedule
            // 
            this.timerShedule.Enabled = true;
            this.timerShedule.Interval = 10000;
            this.timerShedule.Tick += new System.EventHandler(this.timerShedule_Tick);
            // 
            // checkBoxSQLite
            // 
            this.checkBoxSQLite.AutoSize = true;
            this.checkBoxSQLite.Location = new System.Drawing.Point(13, 66);
            this.checkBoxSQLite.Name = "checkBoxSQLite";
            this.checkBoxSQLite.Size = new System.Drawing.Size(111, 17);
            this.checkBoxSQLite.TabIndex = 5;
            this.checkBoxSQLite.Text = "Данные в SQLite";
            this.checkBoxSQLite.UseVisualStyleBackColor = true;
            this.checkBoxSQLite.CheckedChanged += new System.EventHandler(this.checkBoxSQLite_CheckedChanged);
            // 
            // textBoxScenarioName
            // 
            this.textBoxScenarioName.Location = new System.Drawing.Point(279, 19);
            this.textBoxScenarioName.Name = "textBoxScenarioName";
            this.textBoxScenarioName.Size = new System.Drawing.Size(481, 20);
            this.textBoxScenarioName.TabIndex = 6;
            this.textBoxScenarioName.Text = "default.scenario";
            this.textBoxScenarioName.TextChanged += new System.EventHandler(this.textBoxScenarioName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Файл со сценариями";
            // 
            // buttonSelectScenario
            // 
            this.buttonSelectScenario.Location = new System.Drawing.Point(765, 16);
            this.buttonSelectScenario.Name = "buttonSelectScenario";
            this.buttonSelectScenario.Size = new System.Drawing.Size(30, 23);
            this.buttonSelectScenario.TabIndex = 8;
            this.buttonSelectScenario.Text = "...";
            this.buttonSelectScenario.UseVisualStyleBackColor = true;
            this.buttonSelectScenario.Click += new System.EventHandler(this.buttonSelectScenario_Click);
            // 
            // openFileDialogScenario
            // 
            this.openFileDialogScenario.CheckFileExists = false;
            this.openFileDialogScenario.FileName = "*.*";
            this.openFileDialogScenario.Title = "Файл с описанием сценариев";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 443);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Резервное копирование. Настройка.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBoxYideToTray;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
        private System.Windows.Forms.ComboBox comboBoxScenarioType;
        private System.Windows.Forms.ListBox listBoxScenario;
        private System.Windows.Forms.TextBox textBoxScenarioTitle;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonRemoveData;
        private System.Windows.Forms.Button buttonAddDataFile;
        private System.Windows.Forms.Button buttonDeleteScanario;
        private System.Windows.Forms.Button buttonAddScenario;
        private System.Windows.Forms.Button buttonDestination;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxData;
        private System.Windows.Forms.Button buttonAddDataPath;
        private System.Windows.Forms.Button buttonShedule;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxZip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.Timer timerShedule;
        private System.Windows.Forms.Label labelLastTime;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.CheckBox checkBoxSQLite;
        private System.Windows.Forms.TextBox textBoxScenarioName;
        private System.Windows.Forms.Button buttonSelectScenario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialogScenario;
    }
}

