﻿namespace BattleOfTheBots.UIControl
{
    partial class BotsUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelDrawArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelDrawArea
            // 
            this.panelDrawArea.BackColor = System.Drawing.Color.Transparent;
            this.panelDrawArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelDrawArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDrawArea.Location = new System.Drawing.Point(0, 0);
            this.panelDrawArea.Name = "panelDrawArea";
            this.panelDrawArea.Size = new System.Drawing.Size(1189, 253);
            this.panelDrawArea.TabIndex = 0;
            // 
            // BotsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDrawArea);
            this.Name = "BotsUI";
            this.Size = new System.Drawing.Size(1189, 253);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrawArea;
    }
}
