
namespace SudokuApp
{
    partial class saveData
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
            this.SavePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.saveTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SavePanel
            // 
            this.SavePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePanel.AutoScroll = true;
            this.SavePanel.BackColor = System.Drawing.SystemColors.GrayText;
            this.SavePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SavePanel.Location = new System.Drawing.Point(0, 37);
            this.SavePanel.Name = "SavePanel";
            this.SavePanel.Size = new System.Drawing.Size(330, 320);
            this.SavePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save file name:";
            // 
            // saveTextBox
            // 
            this.saveTextBox.Location = new System.Drawing.Point(150, 11);
            this.saveTextBox.Name = "saveTextBox";
            this.saveTextBox.Size = new System.Drawing.Size(135, 20);
            this.saveTextBox.TabIndex = 1;
            // 
            // saveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 356);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveTextBox);
            this.Controls.Add(this.SavePanel);
            this.MaximumSize = new System.Drawing.Size(345, 395);
            this.MinimumSize = new System.Drawing.Size(345, 325);
            this.Name = "saveData";
            this.Text = "save";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.saveData_FormClosed);
            this.Load += new System.EventHandler(this.saveData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel SavePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox saveTextBox;
    }
}