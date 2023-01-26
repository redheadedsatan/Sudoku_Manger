using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuApp
{
    public partial class Settings : Form
    {
        SudokuName sudoku;
        public Settings(SudokuName sn)
        {
            InitializeComponent();
            CenterToScreen();
            sudoku = sn;
            ShowPossibleNum.Checked = sudoku.checkPosNum;
            AutoCheck.Checked = sudoku.autoCheck;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            
        }

        private void SLove_All_Click(object sender, EventArgs e)
        {
            sudoku.SolveAll();
        }

        private void GenerateSudoku_Click(object sender, EventArgs e)
        {
            sudoku.GenerateSudokuTable();
        }

        private void ShowPossibleNum_CheckedChanged(object sender, EventArgs e)
        {
            sudoku.checkPosNum = ShowPossibleNum.Checked;
            sudoku.CheckForPossNum();
        }

        private void AutoCheck_CheckedChanged(object sender, EventArgs e)
        {
            sudoku.autoCheck = AutoCheck.Checked;
            sudoku.Auto_Check();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            sudoku.settingsOn = false;
        }

        private void Load_Click(object sender, EventArgs e)
        {
            saveData sd = new saveData(sudoku);
            sd.Show();
            this.Close();
        }

        private void saveButt_Click(object sender, EventArgs e)
        {

            saveData sd = new saveData(sudoku.getPic(),sudoku);
            sd.Show();
        }

        private void Drop_Click(object sender, EventArgs e)
        {
            saveData sd = new saveData(sudoku);
            sd.DropData();
            sd.Close();
        }
    }
}
