
namespace SudokuApp
{
    partial class Sudoku
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
            this.sudoku_Board = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sudoku_Board)).BeginInit();
            this.SuspendLayout();
            // 
            // sudoku_Board
            // 
            this.sudoku_Board.AllowUserToAddRows = false;
            this.sudoku_Board.AllowUserToDeleteRows = false;
            this.sudoku_Board.AllowUserToResizeColumns = false;
            this.sudoku_Board.AllowUserToResizeRows = false;
            this.sudoku_Board.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sudoku_Board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sudoku_Board.ColumnHeadersVisible = false;
            this.sudoku_Board.Location = new System.Drawing.Point(12, 12);
            this.sudoku_Board.Name = "sudoku_Board";
            this.sudoku_Board.RowHeadersVisible = false;
            this.sudoku_Board.Size = new System.Drawing.Size(601, 600);
            this.sudoku_Board.TabIndex = 0;
            this.sudoku_Board.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.sudoku_Board_CellEndEdit);
            this.sudoku_Board.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.sudoku_Board_CellPainting);
            this.sudoku_Board.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.sudoku_Board_EditingControlShowing);
            this.sudoku_Board.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sudoku_Board_MouseDown);
            this.sudoku_Board.Resize += new System.EventHandler(this.sudoku_Board_Resize);
            // 
            // Check
            // 
            this.Check.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Check.Location = new System.Drawing.Point(251, 618);
            this.Check.Name = "Check";
            this.Check.Size = new System.Drawing.Size(80, 23);
            this.Check.TabIndex = 1;
            this.Check.Text = "Check";
            this.Check.UseVisualStyleBackColor = true;
            this.Check.Click += new System.EventHandler(this.Check_Click);
            // 
            // Sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 650);
            this.Controls.Add(this.Check);
            this.Controls.Add(this.sudoku_Board);
            this.Name = "Sudoku";
            this.Text = "sudoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sudoku_Board)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sudoku_Board;
        private System.Windows.Forms.Button Check;
    }
}

