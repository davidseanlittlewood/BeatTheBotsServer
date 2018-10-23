namespace BattleOfTheBots
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.dsBotConfig = new System.Data.DataSet();
            this.dtBotConfig = new System.Data.DataTable();
            this.dsbColName = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dcolStatus = new System.Data.DataColumn();
            this.dsGameConfig = new System.Data.DataSet();
            this.dtGameConfig = new System.Data.DataTable();
            this.dcHealth = new System.Data.DataColumn();
            this.dcFlips = new System.Data.DataColumn();
            this.dcFlipOdds = new System.Data.DataColumn();
            this.dcFuel = new System.Data.DataColumn();
            this.dcArenaSize = new System.Data.DataColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblBot2Message = new System.Windows.Forms.Label();
            this.lblBot2Status = new System.Windows.Forms.Label();
            this.lblBot2Name = new System.Windows.Forms.Label();
            this.lblBot1Message = new System.Windows.Forms.Label();
            this.lblBot1Status = new System.Windows.Forms.Label();
            this.lblBot1Name = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaderboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridGameConfig = new System.Windows.Forms.DataGridView();
            this.healthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlipOdds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fuel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArenaSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridBotConfig = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.urlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.botRegistryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.houseBotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsBotConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBotConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGameConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGameConfig)).BeginInit();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGameConfig)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBotConfig)).BeginInit();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbOutput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 421);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1192, 321);
            this.panel1.TabIndex = 0;
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.SystemColors.WindowText;
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.tbOutput.Location = new System.Drawing.Point(0, 0);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.Size = new System.Drawing.Size(1192, 321);
            this.tbOutput.TabIndex = 0;
            this.tbOutput.Text = ">\r\n";
            // 
            // dsBotConfig
            // 
            this.dsBotConfig.DataSetName = "NewDataSet";
            this.dsBotConfig.Tables.AddRange(new System.Data.DataTable[] {
            this.dtBotConfig});
            // 
            // dtBotConfig
            // 
            this.dtBotConfig.Columns.AddRange(new System.Data.DataColumn[] {
            this.dsbColName,
            this.dataColumn1,
            this.dataColumn2,
            this.dcolStatus});
            this.dtBotConfig.TableName = "dsBotConfigTable";
            // 
            // dsbColName
            // 
            this.dsbColName.Caption = "Name";
            this.dsbColName.ColumnName = "Name";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "Url";
            this.dataColumn1.ColumnName = "Url";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "Enabled";
            this.dataColumn2.ColumnName = "Enabled";
            this.dataColumn2.DataType = typeof(bool);
            // 
            // dcolStatus
            // 
            this.dcolStatus.Caption = "Status";
            this.dcolStatus.ColumnName = "Status";
            // 
            // dsGameConfig
            // 
            this.dsGameConfig.DataSetName = "NewDataSet";
            this.dsGameConfig.Tables.AddRange(new System.Data.DataTable[] {
            this.dtGameConfig});
            // 
            // dtGameConfig
            // 
            this.dtGameConfig.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcHealth,
            this.dcFlips,
            this.dcFlipOdds,
            this.dcFuel,
            this.dcArenaSize});
            this.dtGameConfig.TableName = "GameConfigTable";
            // 
            // dcHealth
            // 
            this.dcHealth.Caption = "Health";
            this.dcHealth.ColumnName = "Health";
            this.dcHealth.DataType = typeof(short);
            // 
            // dcFlips
            // 
            this.dcFlips.Caption = "Flips";
            this.dcFlips.ColumnName = "Flips";
            this.dcFlips.DataType = typeof(short);
            // 
            // dcFlipOdds
            // 
            this.dcFlipOdds.Caption = "Flip Odds";
            this.dcFlipOdds.ColumnName = "FlipOdds";
            this.dcFlipOdds.DataType = typeof(short);
            // 
            // dcFuel
            // 
            this.dcFuel.Caption = "Fuel";
            this.dcFuel.ColumnName = "Fuel";
            this.dcFuel.DataType = typeof(short);
            // 
            // dcArenaSize
            // 
            this.dcArenaSize.Caption = "Arena Size";
            this.dcArenaSize.ColumnName = "ArenaSize";
            this.dcArenaSize.DataType = typeof(short);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Controls.Add(this.menuStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 307);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1192, 114);
            this.panel4.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 955F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblBot2Message, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblBot2Status, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblBot2Name, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblBot1Message, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblBot1Status, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblBot1Name, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0855F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.68774F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.22676F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1192, 90);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblBot2Message
            // 
            this.lblBot2Message.AutoSize = true;
            this.lblBot2Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBot2Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBot2Message.Location = new System.Drawing.Point(240, 61);
            this.lblBot2Message.Name = "lblBot2Message";
            this.lblBot2Message.Size = new System.Drawing.Size(949, 28);
            this.lblBot2Message.TabIndex = 10;
            this.lblBot2Message.Text = ".........";
            this.lblBot2Message.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBot2Status
            // 
            this.lblBot2Status.AutoSize = true;
            this.lblBot2Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBot2Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBot2Status.Location = new System.Drawing.Point(106, 61);
            this.lblBot2Status.Name = "lblBot2Status";
            this.lblBot2Status.Size = new System.Drawing.Size(127, 28);
            this.lblBot2Status.TabIndex = 9;
            this.lblBot2Status.Text = ".........";
            this.lblBot2Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBot2Name
            // 
            this.lblBot2Name.AutoSize = true;
            this.lblBot2Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBot2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBot2Name.Location = new System.Drawing.Point(4, 61);
            this.lblBot2Name.Name = "lblBot2Name";
            this.lblBot2Name.Size = new System.Drawing.Size(95, 28);
            this.lblBot2Name.TabIndex = 8;
            this.lblBot2Name.Text = ".........";
            this.lblBot2Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBot1Message
            // 
            this.lblBot1Message.AutoSize = true;
            this.lblBot1Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBot1Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBot1Message.Location = new System.Drawing.Point(240, 30);
            this.lblBot1Message.Name = "lblBot1Message";
            this.lblBot1Message.Size = new System.Drawing.Size(949, 30);
            this.lblBot1Message.TabIndex = 7;
            this.lblBot1Message.Text = ".........";
            this.lblBot1Message.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBot1Status
            // 
            this.lblBot1Status.AutoSize = true;
            this.lblBot1Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBot1Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBot1Status.Location = new System.Drawing.Point(106, 30);
            this.lblBot1Status.Name = "lblBot1Status";
            this.lblBot1Status.Size = new System.Drawing.Size(127, 30);
            this.lblBot1Status.TabIndex = 6;
            this.lblBot1Status.Text = ".........";
            this.lblBot1Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBot1Name
            // 
            this.lblBot1Name.AutoSize = true;
            this.lblBot1Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBot1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBot1Name.Location = new System.Drawing.Point(4, 30);
            this.lblBot1Name.Name = "lblBot1Name";
            this.lblBot1Name.Size = new System.Drawing.Size(95, 30);
            this.lblBot1Name.TabIndex = 5;
            this.lblBot1Name.Text = ".........";
            this.lblBot1Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(240, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(949, 28);
            this.label5.TabIndex = 2;
            this.label5.Text = "Status Message";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(106, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 28);
            this.label4.TabIndex = 1;
            this.label4.Text = "Status";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bot";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.leaderboardToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1192, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Enabled = false;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // leaderboardToolStripMenuItem
            // 
            this.leaderboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.leaderboardToolStripMenuItem.Name = "leaderboardToolStripMenuItem";
            this.leaderboardToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.leaderboardToolStripMenuItem.Text = "Leaderboard";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gridGameConfig);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(681, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(511, 307);
            this.panel3.TabIndex = 5;
            // 
            // gridGameConfig
            // 
            this.gridGameConfig.AutoGenerateColumns = false;
            this.gridGameConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGameConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.healthDataGridViewTextBoxColumn,
            this.Flips,
            this.FlipOdds,
            this.Fuel,
            this.ArenaSize});
            this.gridGameConfig.DataMember = "GameConfigTable";
            this.gridGameConfig.DataSource = this.dsGameConfig;
            this.gridGameConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGameConfig.Location = new System.Drawing.Point(0, 24);
            this.gridGameConfig.Name = "gridGameConfig";
            this.gridGameConfig.Size = new System.Drawing.Size(511, 283);
            this.gridGameConfig.TabIndex = 4;
            this.gridGameConfig.Leave += new System.EventHandler(this.gridGameConfig_Leave);
            // 
            // healthDataGridViewTextBoxColumn
            // 
            this.healthDataGridViewTextBoxColumn.DataPropertyName = "Health";
            this.healthDataGridViewTextBoxColumn.HeaderText = "Health";
            this.healthDataGridViewTextBoxColumn.Name = "healthDataGridViewTextBoxColumn";
            // 
            // Flips
            // 
            this.Flips.DataPropertyName = "Flips";
            this.Flips.HeaderText = "Flips";
            this.Flips.Name = "Flips";
            // 
            // FlipOdds
            // 
            this.FlipOdds.DataPropertyName = "FlipOdds";
            this.FlipOdds.HeaderText = "FlipOdds";
            this.FlipOdds.Name = "FlipOdds";
            // 
            // Fuel
            // 
            this.Fuel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fuel.DataPropertyName = "Fuel";
            this.Fuel.HeaderText = "Fuel";
            this.Fuel.Name = "Fuel";
            // 
            // ArenaSize
            // 
            this.ArenaSize.DataPropertyName = "ArenaSize";
            this.ArenaSize.HeaderText = "ArenaSize";
            this.ArenaSize.Name = "ArenaSize";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(511, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Game Configuration";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridBotConfig);
            this.panel2.Controls.Add(this.menuStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(681, 307);
            this.panel2.TabIndex = 6;
            // 
            // gridBotConfig
            // 
            this.gridBotConfig.AutoGenerateColumns = false;
            this.gridBotConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBotConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.urlDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.statusDataGridViewTextBoxColumn});
            this.gridBotConfig.DataMember = "dsBotConfigTable";
            this.gridBotConfig.DataSource = this.dsBotConfig;
            this.gridBotConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBotConfig.Location = new System.Drawing.Point(0, 24);
            this.gridBotConfig.Name = "gridBotConfig";
            this.gridBotConfig.Size = new System.Drawing.Size(681, 283);
            this.gridBotConfig.TabIndex = 3;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // urlDataGridViewTextBoxColumn
            // 
            this.urlDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.urlDataGridViewTextBoxColumn.DataPropertyName = "Url";
            this.urlDataGridViewTextBoxColumn.HeaderText = "Url";
            this.urlDataGridViewTextBoxColumn.Name = "urlDataGridViewTextBoxColumn";
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.botRegistryToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(681, 24);
            this.menuStrip2.TabIndex = 4;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // botRegistryToolStripMenuItem
            // 
            this.botRegistryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.houseBotsToolStripMenuItem});
            this.botRegistryToolStripMenuItem.Name = "botRegistryToolStripMenuItem";
            this.botRegistryToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.botRegistryToolStripMenuItem.Text = "Bot Registry";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // houseBotsToolStripMenuItem
            // 
            this.houseBotsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomBotToolStripMenuItem});
            this.houseBotsToolStripMenuItem.Name = "houseBotsToolStripMenuItem";
            this.houseBotsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.houseBotsToolStripMenuItem.Text = "House Bots";
            // 
            // randomBotToolStripMenuItem
            // 
            this.randomBotToolStripMenuItem.Name = "randomBotToolStripMenuItem";
            this.randomBotToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.randomBotToolStripMenuItem.Text = "Random Bot";
            this.randomBotToolStripMenuItem.Click += new System.EventHandler(this.randomBotToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 742);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Bot Wars";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsBotConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBotConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGameConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGameConfig)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGameConfig)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBotConfig)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Data.DataSet dsBotConfig;
        private System.Data.DataTable dtBotConfig;
        private System.Data.DataColumn dsbColName;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataTable dtGameConfig;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gridGameConfig;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblBot2Message;
        private System.Windows.Forms.Label lblBot2Status;
        private System.Windows.Forms.Label lblBot2Name;
        private System.Windows.Forms.Label lblBot1Message;
        private System.Windows.Forms.Label lblBot1Status;
        private System.Windows.Forms.Label lblBot1Name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Data.DataColumn dcolStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridBotConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem botRegistryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaderboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Data.DataSet dsGameConfig;
        private System.Data.DataColumn dcHealth;
        private System.Data.DataColumn dcFlips;
        private System.Data.DataColumn dcFlipOdds;
        private System.Data.DataColumn dcFuel;
        private System.Windows.Forms.DataGridViewTextBoxColumn healthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flips;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlipOdds;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fuel;
        private System.Data.DataColumn dcArenaSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArenaSize;
        private System.Windows.Forms.ToolStripMenuItem houseBotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomBotToolStripMenuItem;
    }
}

