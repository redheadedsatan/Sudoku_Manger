
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
        public bool settingsOn = false;
        public bool checkPosNum = false;
        public bool autoCheck = false;
        private int Sudoku_size = 9;
        public Sudoku_manger sudokuManage;
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
            linesH = new List<Panel>();
            linesW = new List<Panel>();
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
                        sudokuManage.constraints[i, j].Clear();
                        sudokuManage.SetPlayerNumber(i, j, sudokuManage.sudokuClues[i, j]);
                        sudoku_Player[i, j].Text = sudokuManage.sudokuClues[i, j].ToString();
                        sudoku_Player[i, j].ReadOnly = true;
                        sudoku_Player[i, j].BackColor = Color.Gray;
                    }
                    sudoku_Table.Controls.Add(sudoku_Player[i, j]);
                }
            }
            DrawLine();
            Diff.Text = "Difficulty: " + sudokuManage.diff;
        }
        public Bitmap getPic()
        {
            Bitmap bm = new Bitmap(sudoku_Table.Width, sudoku_Table.Height);
            sudoku_Table.DrawToBitmap(bm, new Rectangle(0, 0, this.sudoku_Table.Width, this.sudoku_Table.Height));
            return bm;
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
                if (i % 3 == 0)
                {
                    lineW.Width = 3;
                    lineH.Height = 3;
                }
            }
            Panel endw = new Panel();
            endw.Width = 3;
            endw.Height = sudoku_Table.Height - mod;
            endw.Location = new Point(Sudoku_size * (sudoku_Table.Size.Width / Sudoku_size), 0);
            endw.Enabled = false;
            endw.BackColor = Color.Black;
            sudoku_Table.Controls.Add(endw);
            linesW.Add(endw);
            Panel endH = new Panel();
            endH.Width = sudoku_Table.Width - mod;
            endH.Height = 3;
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
                    if (sudoku_Player[i, j].MaxLength == 9)
                    {
                        sudoku_Player[i, j].Font = new Font("Arial", 0.2f * sudoku_Player[i, j].Height);
                    }
                }
            }
            for (int i = 0; i < linesH.Count / 2; i++)
            {
                linesH[i].Location = new Point(0, i * (sudoku_Table.Size.Height / Sudoku_size));
                linesH[i].Width = sudoku_Table.Width - mod;
                linesW[i].Location = new Point(i * (sudoku_Table.Size.Width / Sudoku_size), 0);
                linesW[i].Height = sudoku_Table.Height - mod;
                linesH[2 * i].Location = new Point(0, i * (sudoku_Table.Size.Height / Sudoku_size));
                linesH[2 * i].Width = sudoku_Table.Width - mod;
                linesW[2 * i].Location = new Point(i * (sudoku_Table.Size.Width / Sudoku_size), 0);
                linesW[2 * i].Height = sudoku_Table.Height - mod;
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
            TextBox data = (TextBox)sender;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || e.KeyChar == '0')
            {
                e.Handled = true;
            }
            else if (!writePossNum)
            {

                data.MaxLength = 1;
                data.TextAlign = HorizontalAlignment.Center;
                data.Font = new Font("Arial", 0.6f * data.Height);
                data.ForeColor = Color.Black;
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
                if (autoCheck)
                {
                    check();
                }
            }
            else
            {
                data.MaxLength = 9;
                data.TextAlign = HorizontalAlignment.Left;
                data.ForeColor = Color.Green;
                data.Font = new Font("Arial", 0.2f * data.Height);
                if (data.Text.Contains(e.KeyChar))
                {
                    data.Text = data.Text.Remove(data.Text.IndexOf(e.KeyChar), 1);
                    sudokuManage.constraints[int.Parse(data.Name[1].ToString()), int.Parse(data.Name[2].ToString())].Add(int.Parse(e.KeyChar.ToString()));
                    e.Handled = true;
                }
                else if (data.Text == "")
                {
                    data.Text += e.KeyChar;
                }
                else
                {
                    data.Text += e.KeyChar;
                    e.Handled = true;
                }
                List<char> sorted = data.Text.ToList();
                sorted.Sort();
                data.Text = "";
                for (int i = 0; i < sorted.Count; i++)
                {
                    data.Text += sorted[i];
                }
            }
            CheckForPossNum();
            data.Select(0, 0);

        }
        private void sudoku_Table_Enter(object sender, EventArgs e)
        {

        }
        private void checkBut_Click(object sender, EventArgs e)
        {
            check();
        }
        public void check()
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
        public void GenerateSudokuTable()
        {
            Cursor.Current = Cursors.WaitCursor;
            sudokuManage.CreateFullBoard();
            DrawSudoku();
            Cursor.Current = Cursors.Default;
            Diff.Text = "Difficulty: " + sudokuManage.diff;
            CheckForPossNum();
        }
        public void DrawSudoku()
        {
            for (int i = 0; i < Sudoku_size; i++)
            {

                for (int j = 0; j < Sudoku_size; j++)
                {
                    if (sudokuManage.sudokuClues[i, j] != 0)
                    {
                        sudokuManage.SetPlayerNumber(i, j, sudokuManage.sudokuClues[i, j]);
                        sudoku_Player[i, j].Text = sudokuManage.sudokuClues[i, j].ToString();
                        sudoku_Player[i, j].ReadOnly = true;
                        sudoku_Player[i, j].BackColor = Color.Gray;
                    }
                    else
                    {
                        sudoku_Player[i, j].Text = "";
                        sudoku_Player[i, j].ReadOnly = false;
                        sudoku_Player[i, j].BackColor = Color.White;
                        if (sudokuManage.sudokuPlayer[i, j].ToString() != "0")
                        {
                            sudoku_Player[i, j].Text = sudokuManage.sudokuPlayer[i, j].ToString();
                        }
                    }
                    sudoku_Player[i, j].ForeColor = Color.Black;
                    sudoku_Player[i, j].MaxLength = 1;
                    sudoku_Player[i, j].TextAlign = HorizontalAlignment.Center;
                    sudoku_Player[i, j].Font = new Font("Arial", 0.6f * sudoku_Player[i, j].Height);
                }

            }

        }
        public void SolveAll()
        {
            sudokuManage.SetConstraints();
            sudokuManage.SetNum(0, 0, 0);
            for (int i = 0; i < Sudoku_size; i++)
            {
                for (int j = 0; j < Sudoku_size; j++)
                {
                    sudoku_Player[i, j].Text = sudokuManage.sudokuPlayer[i, j].ToString();
                    sudoku_Player[i, j].MaxLength = 1;
                    sudoku_Player[i, j].TextAlign = HorizontalAlignment.Center;
                    sudoku_Player[i, j].Font = new Font("Arial", 0.6f * sudoku_Player[i, j].Height);
                    sudoku_Player[i, j].ForeColor = Color.Black;
                }
            }
            check();
        }
        private void Solve_cell_Click(object sender, EventArgs e)
        {
            sudokuManage.SetConstraints();
            Tuple<int, int, int> cell = sudokuManage.Solve_OneCell();
            sudoku_Player[cell.Item1, cell.Item2].Text = cell.Item3.ToString();
            sudoku_Player[cell.Item1, cell.Item2].MaxLength = 1;
            sudoku_Player[cell.Item1, cell.Item2].TextAlign = HorizontalAlignment.Center;
            sudoku_Player[cell.Item1, cell.Item2].Font = new Font("Arial", 0.6f * sudoku_Player[cell.Item1, cell.Item2].Height);
            sudoku_Player[cell.Item1, cell.Item2].ForeColor = Color.Black;
            CheckForPossNum();
        }
        public void CheckPossNum()
        {
            sudokuManage.SetConstraints();
            for (int i = 0; i < Sudoku_size; i++)
            {
                for (int j = 0; j < Sudoku_size; j++)
                {
                    if (sudokuManage.constraints[i, j].Count != 0)
                    {
                        sudoku_Player[i, j].MaxLength = 9;
                        sudoku_Player[i, j].TextAlign = HorizontalAlignment.Left;
                        sudoku_Player[i, j].ForeColor = Color.Green;
                        sudoku_Player[i, j].Font = new Font("Arial", 0.2f * sudoku_Player[i, j].Height);
                        sudoku_Player[i, j].Text = "";
                        for (int z = 0; z < sudokuManage.constraints[i, j].Count; z++)
                        {
                            sudoku_Player[i, j].Text += sudokuManage.constraints[i, j][z];
                        }
                    }
                }
            }
        }
        public void Auto_Check()
        {
            if (autoCheck)
            {
                checkBut.Visible = false;
                check();
            }
            else
            {
                checkBut.Visible = true;
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
            }
        }
        private void Show_pss_num_Click(object sender, EventArgs e)
        {
            CheckPossNum();
        }
        private void Settings_Click(object sender, EventArgs e)
        {
            if (settingsOn == false)
            {
                Settings set = new Settings(this);
                set.Show();
                settingsOn = true;
            }
        }

        public void CheckForPossNum()
        {
            if (checkPosNum)
            {
                CheckPossNum();
            }
            Show_pss_num.Visible = !checkPosNum;
        }
    }
}
