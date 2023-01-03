
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Sudoku_Manger;



namespace SudokuApp
{
    public partial class Sudoku : Form
    {
        private int Sudoku_size = 9;
        private Sudoku_manger sudokuTable;
        public Sudoku()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sudoku_Board.SelectionMode = DataGridViewSelectionMode.CellSelect;
            string[] rowData = new string[Sudoku_size];
            for (int i = 0; i < Sudoku_size; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = "Column" + (i + 1);
                column.Width = 60;
                column.MaxInputLength = 1;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Font = new Font("Arial", (sudoku_Board.Height / (Sudoku_size * 1.5f)), GraphicsUnit.Pixel);

                

                sudoku_Board.Columns.Add(column);
                sudoku_Board.Columns[i].Width = sudoku_Board.Width / Sudoku_size;

                sudoku_Board.Rows.Add(rowData);

                sudoku_Board.Rows[i].Height = sudoku_Board.Height / Sudoku_size;
            }
            LoadSudoku();



        }
        private void LoadSudoku()
        {
            sudokuTable = new Sudoku_manger();
            sudokuTable.CreateFullBoard();

            for (int i = 0; i < Sudoku_size; i++)
            {
                for (int j = 0; j < Sudoku_size; j++)
                {
                    if (sudokuTable.sudokuClues[i, j] != 0)
                    {
                        sudoku_Board.Rows[i].Cells[j].Value = sudokuTable.sudokuClues[i, j];
                        sudoku_Board.Rows[i].Cells[j].ReadOnly = true;
                        sudoku_Board.Rows[i].Cells[j].Style.BackColor = Color.Gray;


                    }
                }
            }
        }
        private void sudoku_Board_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < Sudoku_size; i++)
            {
                sudoku_Board.Rows[i].Height = sudoku_Board.Height / Sudoku_size;
                if (sudoku_Board.Height > 0)
                {
                    sudoku_Board.Columns[i].DefaultCellStyle.Font = new Font("Arial", (sudoku_Board.Height / (Sudoku_size * 1.5f)), GraphicsUnit.Pixel);
                    sudoku_Board.Columns[i].Width = sudoku_Board.Width / Sudoku_size;
                }

            }
        }

        private void sudoku_Board_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(polNoDataGridViewTextBoxColumn_KeyPress);

            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                tb.KeyPress += new KeyPressEventHandler(polNoDataGridViewTextBoxColumn_KeyPress);
            }

        }
        private void polNoDataGridViewTextBoxColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void sudoku_Board_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (sudoku_Board.SelectedCells[0].Value != null)
            {
                sudokuTable.SetPlayerNumber(sudoku_Board.SelectedCells[0].RowIndex, sudoku_Board.SelectedCells[0].ColumnIndex,
                    int.Parse(sudoku_Board.SelectedCells[0].Value.ToString()));
            }
        }

        private void sudoku_Board_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = sudoku_Board.HitTest(e.X, e.Y);
                if (hti.RowIndex >= 0)
                {
                    sudoku_Board.ClearSelection();
                    sudoku_Board.CurrentCell = sudoku_Board[hti.ColumnIndex, hti.RowIndex];
                }
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {

            List<Tuple<int, int>> mistakes = sudokuTable.checkSudoku();
            if (mistakes.Count == 0)
            {
                for (int i = 0; i < Sudoku_size; i++)
                {
                    for (int j = 0; j < Sudoku_size; j++)
                    {
                        sudoku_Board.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Sudoku_size; i++)
                {
                    for (int j = 0; j < Sudoku_size; j++)
                    {
                        if (mistakes.Contains(new Tuple<int, int>(i, j)))
                        {
                            sudoku_Board.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        }
                        else if(sudokuTable.sudokuClues[i, j] == 0)
                        {
                            sudoku_Board.Rows[i].Cells[j].Style.BackColor = Color.White;
                        }
                    }
                }
                
            }
        }
    }
}
