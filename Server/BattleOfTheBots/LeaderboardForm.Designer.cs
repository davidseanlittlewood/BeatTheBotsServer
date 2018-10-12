namespace BattleOfTheBots
{
    partial class LeaderboardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dsLeaderboard = new System.Data.DataSet();
            this.dtLeaderBoard = new System.Data.DataTable();
            this.dcName = new System.Data.DataColumn();
            this.dcWins = new System.Data.DataColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCurrentMatch = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridLeaderboard = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.winsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dsLeaderboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLeaderBoard)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).BeginInit();
            this.SuspendLayout();
            // 
            // dsLeaderboard
            // 
            this.dsLeaderboard.DataSetName = "NewDataSet";
            this.dsLeaderboard.Tables.AddRange(new System.Data.DataTable[] {
            this.dtLeaderBoard});
            // 
            // dtLeaderBoard
            // 
            this.dtLeaderBoard.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcName,
            this.dcWins});
            this.dtLeaderBoard.TableName = "LeaderBoard";
            // 
            // dcName
            // 
            this.dcName.Caption = "Name";
            this.dcName.ColumnName = "Name";
            // 
            // dcWins
            // 
            this.dcWins.Caption = "Wins";
            this.dcWins.ColumnName = "Wins";
            this.dcWins.DataType = typeof(int);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.lblCurrentMatch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1112, 70);
            this.panel2.TabIndex = 1;
            // 
            // lblCurrentMatch
            // 
            this.lblCurrentMatch.AutoSize = true;
            this.lblCurrentMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMatch.Location = new System.Drawing.Point(12, 21);
            this.lblCurrentMatch.Name = "lblCurrentMatch";
            this.lblCurrentMatch.Size = new System.Drawing.Size(162, 29);
            this.lblCurrentMatch.TabIndex = 0;
            this.lblCurrentMatch.Text = "Current Match";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridLeaderboard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1112, 758);
            this.panel1.TabIndex = 2;
            // 
            // gridLeaderboard
            // 
            this.gridLeaderboard.AutoGenerateColumns = false;
            this.gridLeaderboard.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridLeaderboard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridLeaderboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLeaderboard.ColumnHeadersVisible = false;
            this.gridLeaderboard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.winsDataGridViewTextBoxColumn});
            this.gridLeaderboard.DataMember = "LeaderBoard";
            this.gridLeaderboard.DataSource = this.dsLeaderboard;
            this.gridLeaderboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLeaderboard.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridLeaderboard.Enabled = false;
            this.gridLeaderboard.EnableHeadersVisualStyles = false;
            this.gridLeaderboard.Location = new System.Drawing.Point(0, 0);
            this.gridLeaderboard.Name = "gridLeaderboard";
            this.gridLeaderboard.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLeaderboard.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLeaderboard.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLeaderboard.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridLeaderboard.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLeaderboard.RowTemplate.Height = 30;
            this.gridLeaderboard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridLeaderboard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLeaderboard.ShowEditingIcon = false;
            this.gridLeaderboard.ShowRowErrors = false;
            this.gridLeaderboard.Size = new System.Drawing.Size(1112, 758);
            this.gridLeaderboard.TabIndex = 1;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.FillWeight = 500F;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // winsDataGridViewTextBoxColumn
            // 
            this.winsDataGridViewTextBoxColumn.DataPropertyName = "Wins";
            this.winsDataGridViewTextBoxColumn.HeaderText = "Wins";
            this.winsDataGridViewTextBoxColumn.Name = "winsDataGridViewTextBoxColumn";
            this.winsDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // LeaderboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 828);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "LeaderboardForm";
            this.Text = "Battle of the bots leaderboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LeaderboardForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dsLeaderboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLeaderBoard)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.DataSet dsLeaderboard;
        private System.Data.DataTable dtLeaderBoard;
        private System.Data.DataColumn dcName;
        private System.Data.DataColumn dcWins;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCurrentMatch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridLeaderboard;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn winsDataGridViewTextBoxColumn;
    }
}