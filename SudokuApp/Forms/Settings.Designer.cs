
namespace SudokuApp
{
    partial class Settings
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
            this.AutoCheck = new System.Windows.Forms.CheckBox();
            this.ShowPossibleNum = new System.Windows.Forms.CheckBox();
            this.SLove_All = new System.Windows.Forms.Button();
            this.GenerateSudoku = new System.Windows.Forms.Button();
            this.LoadButt = new System.Windows.Forms.Button();
            this.saveButt = new System.Windows.Forms.Button();
            this.Drop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AutoCheck
            // 
            this.AutoCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoCheck.AutoSize = true;
            this.AutoCheck.Location = new System.Drawing.Point(11, 120);
            this.AutoCheck.Name = "AutoCheck";
            this.AutoCheck.Size = new System.Drawing.Size(80, 17);
            this.AutoCheck.TabIndex = 4;
            this.AutoCheck.Text = "auto check";
            this.AutoCheck.UseVisualStyleBackColor = true;
            this.AutoCheck.CheckedChanged += new System.EventHandler(this.AutoCheck_CheckedChanged);
            // 
            // ShowPossibleNum
            // 
            this.ShowPossibleNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowPossibleNum.AutoSize = true;
            this.ShowPossibleNum.Location = new System.Drawing.Point(12, 143);
            this.ShowPossibleNum.Name = "ShowPossibleNum";
            this.ShowPossibleNum.Size = new System.Drawing.Size(95, 17);
            this.ShowPossibleNum.TabIndex = 11;
            this.ShowPossibleNum.Text = "auto poss num";
            this.ShowPossibleNum.UseVisualStyleBackColor = true;
            this.ShowPossibleNum.CheckedChanged += new System.EventHandler(this.ShowPossibleNum_CheckedChanged);
            // 
            // SLove_All
            // 
            this.SLove_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SLove_All.Location = new System.Drawing.Point(11, 62);
            this.SLove_All.Name = "SLove_All";
            this.SLove_All.Size = new System.Drawing.Size(55, 23);
            this.SLove_All.TabIndex = 12;
            this.SLove_All.Text = "solve all";
            this.SLove_All.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SLove_All.UseVisualStyleBackColor = true;
            this.SLove_All.Click += new System.EventHandler(this.SLove_All_Click);
            // 
            // GenerateSudoku
            // 
            this.GenerateSudoku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateSudoku.Location = new System.Drawing.Point(11, 91);
            this.GenerateSudoku.Name = "GenerateSudoku";
            this.GenerateSudoku.Size = new System.Drawing.Size(123, 23);
            this.GenerateSudoku.TabIndex = 13;
            this.GenerateSudoku.Text = "Genarate new sudoku";
            this.GenerateSudoku.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.GenerateSudoku.UseVisualStyleBackColor = true;
            this.GenerateSudoku.Click += new System.EventHandler(this.GenerateSudoku_Click);
            // 
            // LoadButt
            // 
            this.LoadButt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButt.Location = new System.Drawing.Point(11, 33);
            this.LoadButt.Name = "LoadButt";
            this.LoadButt.Size = new System.Drawing.Size(55, 23);
            this.LoadButt.TabIndex = 14;
            this.LoadButt.Text = "Load";
            this.LoadButt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.LoadButt.UseVisualStyleBackColor = true;
            this.LoadButt.Click += new System.EventHandler(this.Load_Click);
            // 
            // saveButt
            // 
            this.saveButt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButt.Location = new System.Drawing.Point(12, 4);
            this.saveButt.Name = "saveButt";
            this.saveButt.Size = new System.Drawing.Size(55, 23);
            this.saveButt.TabIndex = 15;
            this.saveButt.Text = "Save";
            this.saveButt.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.saveButt.UseVisualStyleBackColor = true;
            this.saveButt.Click += new System.EventHandler(this.saveButt_Click);
            // 
            // Drop
            // 
            this.Drop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Drop.Location = new System.Drawing.Point(79, 4);
            this.Drop.Name = "Drop";
            this.Drop.Size = new System.Drawing.Size(64, 23);
            this.Drop.TabIndex = 16;
            this.Drop.Text = "DropData";
            this.Drop.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Drop.UseVisualStyleBackColor = true;
            this.Drop.Click += new System.EventHandler(this.Drop_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 181);
            this.Controls.Add(this.Drop);
            this.Controls.Add(this.saveButt);
            this.Controls.Add(this.LoadButt);
            this.Controls.Add(this.GenerateSudoku);
            this.Controls.Add(this.SLove_All);
            this.Controls.Add(this.ShowPossibleNum);
            this.Controls.Add(this.AutoCheck);
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox AutoCheck;
        private System.Windows.Forms.CheckBox ShowPossibleNum;
        private System.Windows.Forms.Button SLove_All;
        private System.Windows.Forms.Button GenerateSudoku;
        private System.Windows.Forms.Button LoadButt;
        private System.Windows.Forms.Button saveButt;
        private System.Windows.Forms.Button Drop;
    }
}