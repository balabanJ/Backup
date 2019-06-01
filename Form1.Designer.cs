namespace Backup
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.timerShedule = new System.Windows.Forms.Timer(this.components);
            this.openFileDialogScenario = new System.Windows.Forms.OpenFileDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.labelLastTime = new System.Windows.Forms.Label();
            this.comboBoxZip = new System.Windows.Forms.ComboBox();
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
            this.comboBoxScenarioType = new System.Windows.Forms.ComboBox();
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.listBoxScenario = new System.Windows.Forms.ListBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxSQLite = new System.Windows.Forms.CheckBox();
            this.buttonSelectScenario = new System.Windows.Forms.Button();
            this.checkBoxMail = new System.Windows.Forms.CheckBox();
            this.checkBoxYideToTray = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxScenarioName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSMTP = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxSender = new System.Windows.Forms.TextBox();
            this.textBoxLogFile = new System.Windows.Forms.TextBox();
            this.textBoxEMail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Сценарии";
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
            // openFileDialogScenario
            // 
            this.openFileDialogScenario.CheckFileExists = false;
            this.openFileDialogScenario.FileName = "*.*";
            this.openFileDialogScenario.Title = "Файл с описанием сценариев";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.labelState);
            this.tabPage2.Controls.Add(this.labelLastTime);
            this.tabPage2.Controls.Add(this.comboBoxZip);
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
            this.tabPage2.Size = new System.Drawing.Size(858, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сценарии";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(274, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Использование архиватора";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Тип резервирования";
            // 
            // labelState
            // 
            this.labelState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelState.AutoSize = true;
            this.labelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelState.Location = new System.Drawing.Point(455, 345);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(139, 13);
            this.labelState.TabIndex = 11;
            this.labelState.Text = "Ничего не происходит";
            // 
            // labelLastTime
            // 
            this.labelLastTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLastTime.AutoSize = true;
            this.labelLastTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLastTime.Location = new System.Drawing.Point(19, 345);
            this.labelLastTime.Name = "labelLastTime";
            this.labelLastTime.Size = new System.Drawing.Size(170, 13);
            this.labelLastTime.TabIndex = 11;
            this.labelLastTime.Text = "Время последнего запуска";
            // 
            // comboBoxZip
            // 
            this.comboBoxZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxZip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZip.FormattingEnabled = true;
            this.comboBoxZip.Items.AddRange(new object[] {
            "Без сжатия",
            "Сжимать"});
            this.comboBoxZip.Location = new System.Drawing.Point(277, 120);
            this.comboBoxZip.Name = "comboBoxZip";
            this.comboBoxZip.Size = new System.Drawing.Size(145, 21);
            this.comboBoxZip.TabIndex = 10;
            this.toolTip.SetToolTip(this.comboBoxZip, "Использование архиватора");
            this.comboBoxZip.SelectedIndexChanged += new System.EventHandler(this.checkBoxPack_CheckedChanged);
            // 
            // textBoxScenarioTitle
            // 
            this.textBoxScenarioTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScenarioTitle.Location = new System.Drawing.Point(277, 41);
            this.textBoxScenarioTitle.Name = "textBoxScenarioTitle";
            this.textBoxScenarioTitle.Size = new System.Drawing.Size(145, 20);
            this.textBoxScenarioTitle.TabIndex = 8;
            this.textBoxScenarioTitle.TextChanged += new System.EventHandler(this.textBoxScenarioTitle_TextChanged);
            this.textBoxScenarioTitle.Leave += new System.EventHandler(this.textBoxScenarioTitle_Leave);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestore.Location = new System.Drawing.Point(699, 318);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(134, 23);
            this.buttonRestore.TabIndex = 7;
            this.buttonRestore.Text = "Восстановить из копии";
            this.toolTip.SetToolTip(this.buttonRestore, "Восстановить данные вручную");
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(699, 286);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(134, 23);
            this.buttonCopy.TabIndex = 7;
            this.buttonCopy.Text = "Сделать копию";
            this.toolTip.SetToolTip(this.buttonCopy, "Сделать копию вручную");
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonShedule
            // 
            this.buttonShedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShedule.Location = new System.Drawing.Point(277, 187);
            this.buttonShedule.Name = "buttonShedule";
            this.buttonShedule.Size = new System.Drawing.Size(145, 33);
            this.buttonShedule.TabIndex = 7;
            this.buttonShedule.Text = "Настроить расписание";
            this.toolTip.SetToolTip(this.buttonShedule, "Расписание выполнения сценария");
            this.buttonShedule.UseVisualStyleBackColor = true;
            this.buttonShedule.Click += new System.EventHandler(this.buttonShedule_Click);
            // 
            // buttonRemoveData
            // 
            this.buttonRemoveData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveData.Location = new System.Drawing.Point(699, 83);
            this.buttonRemoveData.Name = "buttonRemoveData";
            this.buttonRemoveData.Size = new System.Drawing.Size(134, 23);
            this.buttonRemoveData.TabIndex = 7;
            this.buttonRemoveData.Text = "Удалить";
            this.toolTip.SetToolTip(this.buttonRemoveData, "Удалить выбранный элемент резервирования");
            this.buttonRemoveData.UseVisualStyleBackColor = true;
            this.buttonRemoveData.Click += new System.EventHandler(this.buttonRemoveData_Click);
            // 
            // buttonAddDataPath
            // 
            this.buttonAddDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddDataPath.Location = new System.Drawing.Point(699, 54);
            this.buttonAddDataPath.Name = "buttonAddDataPath";
            this.buttonAddDataPath.Size = new System.Drawing.Size(134, 23);
            this.buttonAddDataPath.TabIndex = 7;
            this.buttonAddDataPath.Text = "Добавить путь";
            this.toolTip.SetToolTip(this.buttonAddDataPath, "Добавить каталог в список резервируемых данных");
            this.buttonAddDataPath.UseVisualStyleBackColor = true;
            this.buttonAddDataPath.Click += new System.EventHandler(this.buttonAddDataPath_Click);
            // 
            // buttonAddDataFile
            // 
            this.buttonAddDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddDataFile.Location = new System.Drawing.Point(699, 25);
            this.buttonAddDataFile.Name = "buttonAddDataFile";
            this.buttonAddDataFile.Size = new System.Drawing.Size(134, 23);
            this.buttonAddDataFile.TabIndex = 7;
            this.buttonAddDataFile.Text = "Добавить файл";
            this.toolTip.SetToolTip(this.buttonAddDataFile, "Добавить файл в список резервируемых данных");
            this.buttonAddDataFile.UseVisualStyleBackColor = true;
            this.buttonAddDataFile.Click += new System.EventHandler(this.buttonAddDataFile_Click);
            // 
            // buttonDeleteScanario
            // 
            this.buttonDeleteScanario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteScanario.Location = new System.Drawing.Point(277, 318);
            this.buttonDeleteScanario.Name = "buttonDeleteScanario";
            this.buttonDeleteScanario.Size = new System.Drawing.Size(145, 23);
            this.buttonDeleteScanario.TabIndex = 6;
            this.buttonDeleteScanario.Text = "Удалить сценарий";
            this.toolTip.SetToolTip(this.buttonDeleteScanario, "Удалить выбранный сценарий");
            this.buttonDeleteScanario.UseVisualStyleBackColor = true;
            this.buttonDeleteScanario.Click += new System.EventHandler(this.buttonDeleteScanario_Click);
            // 
            // buttonAddScenario
            // 
            this.buttonAddScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddScenario.Location = new System.Drawing.Point(277, 286);
            this.buttonAddScenario.Name = "buttonAddScenario";
            this.buttonAddScenario.Size = new System.Drawing.Size(145, 23);
            this.buttonAddScenario.TabIndex = 5;
            this.buttonAddScenario.Text = "Добавить сценарий";
            this.toolTip.SetToolTip(this.buttonAddScenario, "Создать новый сценарий");
            this.buttonAddScenario.UseVisualStyleBackColor = true;
            this.buttonAddScenario.Click += new System.EventHandler(this.buttonAddScenario_Click);
            // 
            // buttonDestination
            // 
            this.buttonDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDestination.Location = new System.Drawing.Point(277, 147);
            this.buttonDestination.Name = "buttonDestination";
            this.buttonDestination.Size = new System.Drawing.Size(145, 34);
            this.buttonDestination.TabIndex = 4;
            this.buttonDestination.Text = "Выбрать место хранения";
            this.toolTip.SetToolTip(this.buttonDestination, "Каталог или файл, в который будет делаться резервная копия");
            this.buttonDestination.UseVisualStyleBackColor = true;
            this.buttonDestination.Click += new System.EventHandler(this.buttonDestination_Click);
            // 
            // labelDestination
            // 
            this.labelDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDestination.AutoSize = true;
            this.labelDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDestination.Location = new System.Drawing.Point(19, 367);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(210, 13);
            this.labelDestination.TabIndex = 3;
            this.labelDestination.Text = "Место хранения резеврных копий";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Название сценария";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Резервируемые данные";
            // 
            // comboBoxScenarioType
            // 
            this.comboBoxScenarioType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxScenarioType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScenarioType.FormattingEnabled = true;
            this.comboBoxScenarioType.Items.AddRange(new object[] {
            "Полный",
            "Зеркальный",
            "Инкрементальный",
            "Дифференциальный"});
            this.comboBoxScenarioType.Location = new System.Drawing.Point(277, 80);
            this.comboBoxScenarioType.Name = "comboBoxScenarioType";
            this.comboBoxScenarioType.Size = new System.Drawing.Size(145, 21);
            this.comboBoxScenarioType.TabIndex = 2;
            this.toolTip.SetToolTip(this.comboBoxScenarioType, "Тип сценария");
            this.comboBoxScenarioType.SelectedIndexChanged += new System.EventHandler(this.comboBoxScenarioType_SelectedIndexChanged);
            // 
            // listBoxData
            // 
            this.listBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.Location = new System.Drawing.Point(458, 25);
            this.listBoxData.Name = "listBoxData";
            this.listBoxData.Size = new System.Drawing.Size(226, 316);
            this.listBoxData.TabIndex = 0;
            this.toolTip.SetToolTip(this.listBoxData, "Список файлов и каталогов для резервирования");
            // 
            // listBoxScenario
            // 
            this.listBoxScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxScenario.FormattingEnabled = true;
            this.listBoxScenario.Location = new System.Drawing.Point(22, 25);
            this.listBoxScenario.Name = "listBoxScenario";
            this.listBoxScenario.Size = new System.Drawing.Size(226, 316);
            this.listBoxScenario.TabIndex = 0;
            this.listBoxScenario.SelectedIndexChanged += new System.EventHandler(this.listBoxScenario_SelectedIndexChanged);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            this.toolTip.ShowAlways = true;
            // 
            // checkBoxSQLite
            // 
            this.checkBoxSQLite.AutoSize = true;
            this.checkBoxSQLite.Location = new System.Drawing.Point(9, 19);
            this.checkBoxSQLite.Name = "checkBoxSQLite";
            this.checkBoxSQLite.Size = new System.Drawing.Size(111, 17);
            this.checkBoxSQLite.TabIndex = 5;
            this.checkBoxSQLite.Text = "Данные в SQLite";
            this.toolTip.SetToolTip(this.checkBoxSQLite, "Описание сценариев хранится в таблице SQLite");
            this.checkBoxSQLite.UseVisualStyleBackColor = true;
            this.checkBoxSQLite.CheckedChanged += new System.EventHandler(this.checkBoxSQLite_CheckedChanged);
            // 
            // buttonSelectScenario
            // 
            this.buttonSelectScenario.Location = new System.Drawing.Point(496, 42);
            this.buttonSelectScenario.Name = "buttonSelectScenario";
            this.buttonSelectScenario.Size = new System.Drawing.Size(29, 20);
            this.buttonSelectScenario.TabIndex = 8;
            this.buttonSelectScenario.Text = "...";
            this.toolTip.SetToolTip(this.buttonSelectScenario, "Выбрать файл сценариев");
            this.buttonSelectScenario.UseVisualStyleBackColor = true;
            this.buttonSelectScenario.Click += new System.EventHandler(this.buttonSelectScenario_Click);
            // 
            // checkBoxMail
            // 
            this.checkBoxMail.AutoSize = true;
            this.checkBoxMail.Checked = true;
            this.checkBoxMail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMail.Location = new System.Drawing.Point(7, 19);
            this.checkBoxMail.Name = "checkBoxMail";
            this.checkBoxMail.Size = new System.Drawing.Size(124, 17);
            this.checkBoxMail.TabIndex = 16;
            this.checkBoxMail.Text = "Отправлять отчеты";
            this.toolTip.SetToolTip(this.checkBoxMail, "Выбрать, если нужно посылать отчеты на почту");
            this.checkBoxMail.UseVisualStyleBackColor = true;
            this.checkBoxMail.CheckedChanged += new System.EventHandler(this.checkBoxMail_CheckedChanged);
            // 
            // checkBoxYideToTray
            // 
            this.checkBoxYideToTray.AutoSize = true;
            this.checkBoxYideToTray.Location = new System.Drawing.Point(11, 42);
            this.checkBoxYideToTray.Name = "checkBoxYideToTray";
            this.checkBoxYideToTray.Size = new System.Drawing.Size(124, 17);
            this.checkBoxYideToTray.TabIndex = 1;
            this.checkBoxYideToTray.Text = "Сворачивать в Tray";
            this.toolTip.SetToolTip(this.checkBoxYideToTray, "Выбрать, если нужно сворачивать в Tray");
            this.checkBoxYideToTray.UseVisualStyleBackColor = true;
            this.checkBoxYideToTray.CheckedChanged += new System.EventHandler(this.checkBoxYideToTray_CheckedChanged);
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.AutoSize = true;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(11, 19);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(175, 17);
            this.checkBoxAutoStart.TabIndex = 0;
            this.checkBoxAutoStart.Text = "Запускать вместе с Windows";
            this.toolTip.SetToolTip(this.checkBoxAutoStart, "Выбрать, если требуется автоматический старт программы");
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonHelp.Location = new System.Drawing.Point(69, 286);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(123, 23);
            this.buttonHelp.TabIndex = 2;
            this.buttonHelp.Text = "Помощь";
            this.toolTip.SetToolTip(this.buttonHelp, "Справка о программе");
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.buttonHelp);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(858, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Общие";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.Controls.Add(this.checkBoxSQLite);
            this.groupBox3.Controls.Add(this.textBoxScenarioName);
            this.groupBox3.Controls.Add(this.buttonSelectScenario);
            this.groupBox3.Location = new System.Drawing.Point(292, 308);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(537, 71);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Хранилище сценариев";
            // 
            // textBoxScenarioName
            // 
            this.textBoxScenarioName.Location = new System.Drawing.Point(9, 42);
            this.textBoxScenarioName.Name = "textBoxScenarioName";
            this.textBoxScenarioName.Size = new System.Drawing.Size(481, 20);
            this.textBoxScenarioName.TabIndex = 6;
            this.textBoxScenarioName.Text = "default.sqlite";
            this.textBoxScenarioName.TextChanged += new System.EventHandler(this.textBoxScenarioName_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.checkBoxMail);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxSMTP);
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.textBoxSender);
            this.groupBox2.Controls.Add(this.textBoxLogFile);
            this.groupBox2.Controls.Add(this.textBoxEMail);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(292, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 193);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Журнализация";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "SMTP Server";
            // 
            // textBoxSMTP
            // 
            this.textBoxSMTP.Location = new System.Drawing.Point(7, 103);
            this.textBoxSMTP.Name = "textBoxSMTP";
            this.textBoxSMTP.Size = new System.Drawing.Size(162, 20);
            this.textBoxSMTP.TabIndex = 13;
            this.textBoxSMTP.Text = "smtp.yandex.ru";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(344, 102);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(144, 20);
            this.textBoxPassword.TabIndex = 11;
            // 
            // textBoxSender
            // 
            this.textBoxSender.Location = new System.Drawing.Point(176, 102);
            this.textBoxSender.Name = "textBoxSender";
            this.textBoxSender.Size = new System.Drawing.Size(162, 20);
            this.textBoxSender.TabIndex = 9;
            this.textBoxSender.Text = "demo@yandex.ru";
            // 
            // textBoxLogFile
            // 
            this.textBoxLogFile.Location = new System.Drawing.Point(9, 162);
            this.textBoxLogFile.Name = "textBoxLogFile";
            this.textBoxLogFile.Size = new System.Drawing.Size(479, 20);
            this.textBoxLogFile.TabIndex = 6;
            this.textBoxLogFile.Text = ".\\log.txt";
            this.textBoxLogFile.TextChanged += new System.EventHandler(this.textBoxScenarioName_TextChanged);
            // 
            // textBoxEMail
            // 
            this.textBoxEMail.Location = new System.Drawing.Point(7, 64);
            this.textBoxEMail.Name = "textBoxEMail";
            this.textBoxEMail.Size = new System.Drawing.Size(481, 20);
            this.textBoxEMail.TabIndex = 6;
            this.textBoxEMail.Text = "demo@gmail.com";
            this.textBoxEMail.TextChanged += new System.EventHandler(this.textBoxScenarioName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(341, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Пароль";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Почтовый адрес отправителя";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Файл журнала";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Почтовый адрес для отчета";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.checkBoxYideToTray);
            this.groupBox1.Controls.Add(this.checkBoxAutoStart);
            this.groupBox1.Location = new System.Drawing.Point(292, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 64);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Общая настройка";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 205);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Backup";
            this.notifyIcon.BalloonTipTitle = "Backup";
            this.notifyIcon.Text = "Backup";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 446);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BackUp: Программное обеспечение для управления резервным копированием";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.Timer timerShedule;
        private System.Windows.Forms.OpenFileDialog openFileDialogScenario;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelLastTime;
        private System.Windows.Forms.ComboBox comboBoxZip;
        private System.Windows.Forms.TextBox textBoxScenarioTitle;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonShedule;
        private System.Windows.Forms.Button buttonRemoveData;
        private System.Windows.Forms.Button buttonAddDataPath;
        private System.Windows.Forms.Button buttonAddDataFile;
        private System.Windows.Forms.Button buttonDeleteScanario;
        private System.Windows.Forms.Button buttonAddScenario;
        private System.Windows.Forms.Button buttonDestination;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxScenarioType;
        private System.Windows.Forms.ListBox listBoxData;
        private System.Windows.Forms.ListBox listBoxScenario;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxMail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSMTP;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxSender;
        private System.Windows.Forms.TextBox textBoxLogFile;
        private System.Windows.Forms.TextBox textBoxEMail;
        private System.Windows.Forms.TextBox textBoxScenarioName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSelectScenario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxSQLite;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.CheckBox checkBoxYideToTray;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

