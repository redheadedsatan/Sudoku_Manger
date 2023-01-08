
namespace SudokuApp
{
    partial class SudokuName
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
            this.write_Poss_Values = new System.Windows.Forms.CheckBox();
            this.sudoku_Table = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.AutoCheck = new System.Windows.Forms.CheckBox();
            this.checkBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // write_Poss_Values
            // 
            this.write_Poss_Values.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.write_Poss_Values.AutoSize = true;
            this.write_Poss_Values.Location = new System.Drawing.Point(8, 622);
            this.write_Poss_Values.Name = "write_Poss_Values";
            this.write_Poss_Values.Size = new System.Drawing.Size(231, 17);
            this.write_Poss_Values.TabIndex = 1;
            this.write_Poss_Values.Text = "write possible values(pressing  q toggle this)";
            this.write_Poss_Values.UseVisualStyleBackColor = true;
            this.write_Poss_Values.CheckedChanged += new System.EventHandler(this.write_Poss_Values_CheckedChanged);
            // 
            // sudoku_Table
            // 
            this.sudoku_Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sudoku_Table.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sudoku_Table.BackColor = System.Drawing.Color.White;
            this.sudoku_Table.Location = new System.Drawing.Point(12, 12);
            this.sudoku_Table.Margin = new System.Windows.Forms.Padding(0);
            this.sudoku_Table.MinimumSize = new System.Drawing.Size(180, 180);
            this.sudoku_Table.Name = "sudoku_Table";
            this.sudoku_Table.Padding = new System.Windows.Forms.Padding(0);
            this.sudoku_Table.Size = new System.Drawing.Size(600, 575);
            this.sudoku_Table.TabIndex = 2;
            this.sudoku_Table.TabStop = false;
            this.sudoku_Table.SizeChanged += new System.EventHandler(this.sudoku_Table_SizeChanged);
            this.sudoku_Table.Enter += new System.EventHandler(this.sudoku_Table_Enter);
            // 
            // AutoCheck
            // 
            this.AutoCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoCheck.AutoSize = true;
            this.AutoCheck.Location = new System.Drawing.Point(8, 599);
            this.AutoCheck.Name = "AutoCheck";
            this.AutoCheck.Size = new System.Drawing.Size(80, 17);
            this.AutoCheck.TabIndex = 3;
            this.AutoCheck.Text = "auto check";
            this.AutoCheck.UseVisualStyleBackColor = true;
            // 
            // checkBut
            // 
            this.checkBut.Location = new System.Drawing.Point(94, 595);
            this.checkBut.Name = "checkBut";
            this.checkBut.Size = new System.Drawing.Size(98, 23);
            this.checkBut.TabIndex = 4;
            this.checkBut.Text = "Manual Check";
            this.checkBut.UseVisualStyleBackColor = true;
            this.checkBut.Click += new System.EventHandler(this.checkBut_Click);
            // 
            // SudokuName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 651);
            this.Controls.Add(this.checkBut);
            this.Controls.Add(this.AutoCheck);
            this.Controls.Add(this.sudoku_Table);
            this.Controls.Add(this.write_Poss_Values);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(220, 290);
            this.Name = "SudokuName";
            this.Text = "sudoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SudokuName_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox write_Poss_Values;
        private System.Windows.Forms.GroupBox sudoku_Table;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox AutoCheck;
        private System.Windows.Forms.Button checkBut;
    }
}

