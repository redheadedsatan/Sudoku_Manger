
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
    public partial class SudokuName : Form
    {
        private int Sudoku_size = 9;
        private Sudoku_manger sudokuManage;
        public SudokuName()
        {

            InitializeComponent();

        }
        TextBox[,] sudoku_Player;
        List<Panel> linesW;
        List<Panel> linesH;
        bool writePossNum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            sudokuManage = new Sudoku_manger();
            sudokuManage.CreateFullBoard();
            DrawLine();
            sudoku_Player = new TextBox[Sudoku_size, Sudoku_size];
            for (int i = 0; i < Sudoku_size; i++)
            {
                for (int j = 0; j < Sudoku_size; j++)
                {

                    sudoku_Player[i, j] = SetCell(i, j);
                    if (sudokuManage.sudokuClues[i, j] != 0)
                    {
                        sudokuManage.SetPlayerNumber(i,j, sudokuManage.sudokuClues[i, j]);
                        sudoku_Player[i, j].Text = sudokuManage.sudokuClues[i, j].ToString();
                        sudoku_Player[i, j].ReadOnly = true;
                        sudoku_Player[i, j].BackColor = Color.Gray;

                    }
                    sudoku_Table.Controls.Add(sudoku_Player[i, j]);

                }
            }
        }
        private TextBox SetCell(int row, int column)
        {
            TextBox cell = new TextBox();
            cell.Name = $"t{row}{column}";
            cell.BorderStyle = BorderStyle.None;
            cell.Multiline = true;
            cell.Location = new Point(column * (sudoku_Table.Size.Width / Sudoku_size), row * (sudoku_Table.Size.Height / Sudoku_size));
            cell.Height = (int)((sudoku_Table.Size.Height / Sudoku_size));
            cell.Width = sudoku_Table.Size.Width / Sudoku_size;
            cell.TextAlign = HorizontalAlignment.Center;
            cell.MaxLength = 1;
            cell.Enter += new System.EventHandler(Text_Select);
            cell.Leave += new System.EventHandler(Text_Leave);
            cell.KeyPress += new KeyPressEventHandler(Enter_Value); ;
            cell.Font = new Font("Arial", 0.6f * cell.Height);
            return cell;
        }
        private void DrawLine()
        {
            linesH = new List<Panel>();
            linesW = new List<Panel>();
            int mod = 0;
            for (int i = 0; i < Sudoku_size; i++)
            {
                Panel lineW = new Panel();
                lineW.Width = 1;
                lineW.Height = sudoku_Table.Height - mod;
                lineW.Location = new Point(i * (sudoku_Table.Size.Width / Sudoku_size), 0);
                lineW.Enabled = false;
                lineW.BackColor = Color.Black;
                sudoku_Table.Controls.Add(lineW);
                linesW.Add(lineW);
                Panel lineH = new Panel();
                lineH.Width = sudoku_Table.Width - mod;
                lineH.Height = 1;
                lineH.Location = new Point(0, i * (sudoku_Table.Size.Height / Sudoku_size));
                lineH.Enabled = false;
                lineH.BackColor = Color.Black;
                sudoku_Table.Controls.Add(lineH);
                linesH.Add(lineH);
            }
            Panel endw = new Panel();
            endw.Width = 1;
            endw.Height = sudoku_Table.Height - mod;
            endw.Location = new Point(Sudoku_size * (sudoku_Table.Size.Width / Sudoku_size), 0);
            endw.Enabled = false;
            endw.BackColor = Color.Black;
            sudoku_Table.Controls.Add(endw);
            linesW.Add(endw);
            Panel endH = new Panel();
            endH.Width = sudoku_Table.Width - mod;
            endH.Height = 1;
            endH.Location = new Point(0, Sudoku_size * (sudoku_Table.Size.Height / Sudoku_size));
            endH.Enabled = false;
            endH.BackColor = Color.Black;
            sudoku_Table.Controls.Add(endH);
            linesH.Add(endH);

        }
        private void sudoku_Table_SizeChanged(object sender, EventArgs e)
        {
            int mod = 0;
            for (int i = 0; i < Sudoku_size; i++)
            {
                for (int j = 0; j < Sudoku_size; j++)
                {
                    TextBox cell = SetCell(i, j);
                    sudoku_Player[i, j].Location = cell.Location;
                    sudoku_Player[i, j].Size = cell.Size;
                    sudoku_Player[i, j].Font = cell.Font;
                }
            }
            for (int i = 0; i < linesH.Count; i++)
            {
                linesH[i].Location = new Point(0, i * (sudoku_Table.Size.Height / Sudoku_size));
                linesH[i].Width = sudoku_Table.Width - mod;
                linesW[i].Location = new Point(i * (sudoku_Table.Size.Width / Sudoku_size), 0);
                linesW[i].Height = sudoku_Table.Height - mod;
            }
        }
        private void Text_Select(object sender, EventArgs e)
        {
            TextBox data = (TextBox)sender;
            if (data.BackColor != Color.Gray)
            {
                data.BackColor = Color.Aqua;
            }
           
        }
        private void Text_Leave(object sender, EventArgs e)
        {
            TextBox data = (TextBox)sender;
            if (data.BackColor != Color.Gray)
            {
                data.BackColor = Color.White;
            }

        }
        private void Enter_Value(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if(!writePossNum)
            {
                TextBox data = (TextBox)sender;
                data.MaxLength = 1;
                data.TextAlign = HorizontalAlignment.Center;
                data.Font = new Font("Arial", 0.6f * data.Height);
                if (e.KeyChar == '\b')
                {
                    sudokuManage.SetPlayerNumber(int.Parse(data.Name[1].ToString()), int.Parse(data.Name[2].ToString()), 0);
                    data.Text = "";
                }
                else
                {
                    sudokuManage.SetPlayerNumber(int.Parse(data.Name[1].ToString()), int.Parse(data.Name[2].ToString()), int.Parse(e.KeyChar.ToString()));
                    data.Text = e.KeyChar.ToString();
                }
                if (AutoCheck.Checked)
                {
                    check();
                }
                
            }
            else
            {
                TextBox data = (TextBox)sender;
                data.MaxLength = 9;
                data.TextAlign = HorizontalAlignment.Left;
                data.Font = new Font("Arial", 0.2f * data.Height);
                if (data.Text.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
               
            }
        }
        private void sudoku_Table_Enter(object sender, EventArgs e)
        {

        }

        private void checkBut_Click(object sender, EventArgs e)
        {
            check();
        }
        private void check() 
        {
            List<Tuple<int, int>> errors = sudokuManage.checkSudoku();
            for (int i = 0; i < Sudoku_size; i++)
            {
                for (int j = 0; j < Sudoku_size; j++)
                {
                    if (sudokuManage.sudokuClues[i, j] == 0)
                    {
                        sudoku_Player[i, j].BackColor = Color.White;
                    }
                    else
                    {
                        sudoku_Player[i, j].ForeColor = Color.Black;
                    }
                }
            }

            for (int i = 0; i < errors.Count; i++)
            {
                if (sudokuManage.sudokuClues[errors[i].Item1, errors[i].Item2] != 0)
                {
                    sudoku_Player[errors[i].Item1, errors[i].Item2].ForeColor = Color.Red;
                }
                else
                {
                    sudoku_Player[errors[i].Item1, errors[i].Item2].BackColor = Color.Red;
                }
            }
        }
        private void write_Poss_Values_CheckedChanged(object sender, EventArgs e)
        {
            writePossNum = !writePossNum;
        }

        private void SudokuName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'q' || e.KeyChar == 'Q')
            {
                write_Poss_Values.Checked = !writePossNum;
            }
        }
    }
}
