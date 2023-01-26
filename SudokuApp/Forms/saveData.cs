using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku_Manger;
using SQL_MANGER;
using SudokuApp.classes;

namespace SudokuApp
{
    public partial class saveData : Form
    {
        public bool Saving;
        List<PanelWithSaveData> savePanels;
        public Bitmap img;
        public SudokuName sudokuData;
        public saveData(Bitmap bm, SudokuName boarddata)
        {
            InitializeComponent();
            Saving = true;
            savePanels = new List<PanelWithSaveData>();
            img = new Bitmap(bm, 120, 120);
            sudokuData = boarddata;
            sudokuData.Enabled = false;
            saveTextBox.Text = DateTime.Now.ToString();
            PanelWithSaveData tempPanel = new PanelWithSaveData(this, 0);
            savePanels.Add(tempPanel);
            SavePanel.Controls.Add(tempPanel.dataHolder);
            LoadSql();
            CenterToScreen();            
        }
        public saveData(SudokuName boarddata)
        {
            InitializeComponent();
            Saving = false;
            savePanels = new List<PanelWithSaveData>();
            sudokuData = boarddata;
            SavePanel.Height = 360;
            SavePanel.Location = new Point(0,0);
            label1.Visible = false;
            saveTextBox.Visible = false;
            
            LoadSql();
            CenterToScreen();
        }
        public string SaveText() 
        {
            return saveTextBox.Text;
        }
        private void LoadSql() 
        {
            
            SQL_DB_Manger.CreateDataBase();
            SQL_DB_Manger.CreateTable();
            List<Tuple<int, string, byte[], DateTime>> Sqldata = SQL_DB_Manger.ReadTableDataInfo();
            int max = Math.Max(3, Sqldata.Count);
            for (int i = 0; i < max; i++)
            {
                PanelWithSaveData tempPanel;
                if (i < Sqldata.Count)
                {
                    tempPanel = new PanelWithSaveData(this,Sqldata[i].Item1, Sqldata[i].Item2, Sqldata[i].Item3, Sqldata[i].Item4, SavePanel.Controls.Count);
                }
                else
                {
                    tempPanel = new PanelWithSaveData(this,i); 
                }
                savePanels.Add(tempPanel);
                SavePanel.Controls.Add(tempPanel.dataHolder);

            }
            
        }
        public int saveDataInSql(int id = -1) 
        {
            if (id == -1)
            {
                id = GetIdIfNameInDB(saveTextBox.Text);
            }
            
            if (id == -1)
            {
                return AddDataToDB();
            }
            else 
            {
                UpdateDataToDatabase(id);
                for (int i = 0; i < savePanels.Count; i++)
                {
                    if (savePanels[i].id == id)
                    {
                        savePanels[i].dataHolder.Controls.Clear();
                        savePanels[i].LoadToPanel(id);
                    }
                }

                return -1;
            }
            
        }
        public int GetIdIfNameInDB(string name) 
        {
            for (int i = 0; i < savePanels.Count; i++)
            {
                if (savePanels[i].saveName != null)
                {
                    if (savePanels[i].saveName.Text == name)
                    {
                        DialogResult value = MessageBox.Show($"ovverides the save named: {name}");
                        return SQL_DB_Manger.GetId(name);
                    }
                }
            }
            return -1;
        }
        private int AddDataToDB() 
        {
            return SQL_DB_Manger.InsertToTable(saveTextBox.Text, DateTime.Now, BitMapTOString(img), sudokuData.sudokuManage.ToStringConstrains()
                , sudokuData.sudokuManage.ToStringFull(), sudokuData.sudokuManage.ToStringClues(), sudokuData.sudokuManage.ToStringPlayer());
        }
        private void UpdateDataToDatabase(int id)
        {
            SQL_DB_Manger.UpadteTable(id,saveTextBox.Text, DateTime.Now, BitMapTOString(img), sudokuData.sudokuManage.ToStringConstrains()
                , sudokuData.sudokuManage.ToStringFull(), sudokuData.sudokuManage.ToStringClues(), sudokuData.sudokuManage.ToStringPlayer());
        }
        public string BitMapTOString(Bitmap bm)
        {
            string sqlData = "";
            byte[] data;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                bm.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                data = stream.ToArray();
            }
            sqlData = string.Join(" ", data);
            return sqlData;
        }
        public void DropData() 
        {
            SQL_DB_Manger.DropTable();
            SQL_DB_Manger.DropDB();
        }
        private void saveData_FormClosed(object sender, FormClosedEventArgs e)
        {
            sudokuData.Enabled = true;
        }
        public void LoadData(int id) 
        {
            string[] sudokuStrings = SQL_DB_Manger.ReadSudokuData(id);
            sudokuData.sudokuManage = new Sudoku_manger(id, sudokuStrings[0], sudokuStrings[1], sudokuStrings[2], sudokuStrings[3]);
            sudokuData.DrawSudoku();
        }
        public void RemoveData(int id) 
        {
            SQL_DB_Manger.RemoveFromTable(id);
            
        }
        private void saveData_Load(object sender, EventArgs e)
        {

        }
    }
}
