using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace SudokuApp.classes
{

    class PanelWithSaveData
    {
        Button update;
        Button delete;
        bool dataLoaded;
        saveData sd;
        public Panel dataHolder;
        public Label saveName;
        Label Date;
        PictureBox pict;
        public int id;
        public PanelWithSaveData(saveData save, int sudokuId, string name, byte[] bm, DateTime date, int y)
        {
            dataLoaded = true;
            sd = save;
            id = sudokuId;
            SetPanel(y);
            SetButts();
            SetImage(bm);
            SetName(name);
            SetDate(date);
        }
        public PanelWithSaveData(saveData save, int y)
        {
            dataLoaded = false;
            sd = save;
            SetPanel(y);
        }
        private void SetPanel(int y)
        {
            dataHolder = new Panel();
            dataHolder.Height = 120;
            dataHolder.Width = 300;
            dataHolder.BackColor = Color.Gray;
            dataHolder.BorderStyle = BorderStyle.Fixed3D;
            dataHolder.Location = new Point(5, 120 * y);
            dataHolder.Click += new System.EventHandler(ClickPanel);
        }
        private void ClickPanel(object sender, EventArgs e)
        {
            if (sd.Saving)
            {
                SaveInPanel();
            }
            else if(dataLoaded)
            {
                sd.LoadData(id);
                sd.Close();
            }
            
        }
        private void SetButts() 
        {
            delete = new Button();
            delete.Location = new Point(140, 80);
            delete.Text = "delete";
            delete.AutoSize = true;
            delete.BackColor = Color.LightGray;
            delete.Click += new System.EventHandler(RemovePanel);
            dataHolder.Controls.Add(delete);
            if (sd.Saving)
            {
                update = new Button();
                update.Location = new Point(220, 80);
                update.Text = "Update";
                update.AutoSize = true;
                update.BackColor = Color.LightGray;
                update.Click += new System.EventHandler(UpdatePanel);
                dataHolder.Controls.Add(update);
            }
            
        }
        private void RemovePanel(object sender, EventArgs e) 
        {
            dataHolder.Controls.Clear();
            sd.RemoveData(id);
        }
        private void UpdatePanel(object sender, EventArgs e)
        {
            dataHolder.Controls.Clear();
            SaveInPanel(id);
        }
        public void SaveInPanel(int id = -1) 
        {
            
            int temp = sd.saveDataInSql(id);
            if (temp == -1)
            {
                return;
            }
            LoadToPanel(temp);


        }
        public void LoadToPanel(int id) 
        {
            dataLoaded = true;
            this.id = id;
            SetName(sd.SaveText());
            SetDate();
            SetImage(sd.img);
            SetButts();
        }
        private void SetImage(byte[] bm)
        {
            pict = new PictureBox();
            pict.Size = new Size(120, 120);
            pict.Location = new Point(0, 0);
            pict.Click += new System.EventHandler(ClickPanel);
            using (var ms = new MemoryStream(bm))
            {
                Bitmap imgData = new Bitmap(ms);
                pict.Image = imgData;
            }
            dataHolder.Controls.Add(pict);

        }
        private void SetImage(Bitmap bm)
        {
            pict = new PictureBox();
            pict.Size = new Size(120, 120);
            pict.Location = new Point(0, 0);
            pict.Image = bm;
            pict.Click += new System.EventHandler(ClickPanel);
            dataHolder.Controls.Add(pict);
        }
        private void SetName(string name)
        {
            saveName = new Label();
            saveName.Click += new System.EventHandler(ClickPanel);
            saveName.Text = name;
            saveName.AutoSize = true;
            saveName.Location = new Point(140, 0);
            dataHolder.Controls.Add(saveName);
        }
        private void SetDate(DateTime date)
        {
            Date = new Label();
            Date.Text =  date.ToString();
            Date.AutoSize = true;
            Date.Click += new System.EventHandler(ClickPanel);
            Date.Location = new Point(140, 60);
            dataHolder.Controls.Add(Date);
        }
        private void SetDate()
        {
            Date = new Label();
            Date.Text = DateTime.Now.ToString();
            Date.AutoSize = true;
            Date.Click += new System.EventHandler(ClickPanel);
            Date.Location = new Point(140, 60);
            dataHolder.Controls.Add(Date);
        }
        public void LoadData(int sudokuId, byte[] bm, string name)
        {
            id = sudokuId;
            SetImage(bm);
            SetName(name);
            SetDate();
        }
    }
}
