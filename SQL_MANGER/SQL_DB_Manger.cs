using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQL_MANGER
{
    public enum Table
    {
        ID,
        NAME,
        DATE,
        BITMAP,
        SUDOKUPLAYERCONSTRAINS,
        SUDOKUFULL,
        SUDOKUCLUES,
        SUDOKUPLAYER

    }
    public class SQL_DB_Manger
    {
        private static string TABLE_NAME = "SUDOKU_DATA";
        private static string DB_NAME = "SUDOKU";
        private static string CONNECTIONSTRING = "Server=localhost;Database=master;Trusted_Connection=True;";
        private static SqlConnection sqlconnection = null;


        public static void Open()
        {
            if (sqlconnection == null)
            {
                sqlconnection = new SqlConnection(CONNECTIONSTRING);
            }
            try
            {
                CreateDataBase();
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message);
            }

            sqlconnection.Open();
        }
        public static void CreateDataBase()
        {
            string cmdText = "CREATE database " + DB_NAME;
            if (sqlconnection == null)
            {
                sqlconnection = new SqlConnection(CONNECTIONSTRING);
            }
            if (sqlconnection.State != ConnectionState.Open)
            {
                try
                {
                    sqlconnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(cmdText, sqlconnection);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                    //MessageBox.Show(e.Message);
                }
                finally
                {
                    sqlconnection.Close();
                }
            }

        }
        public static void Close()
        {
            sqlconnection.Close();
        }
        public static void CreateTable()
        {
            string SQLcmd = $"CREATE TABLE {TABLE_NAME} ({ Table.ID.ToString()} INTEGER PRIMARY KEY IDENTITY, {Table.NAME.ToString()} VARCHAR(50) NOT NULL" +
                $", {Table.DATE.ToString()} DATETIME, {Table.BITMAP.ToString()} TEXT" +
                $", {Table.SUDOKUFULL.ToString()} VARCHAR(162), {Table.SUDOKUCLUES.ToString()} VARCHAR(162), {Table.SUDOKUPLAYER.ToString()} VARCHAR(162)" +
                $", {Table.SUDOKUPLAYERCONSTRAINS.ToString()} VARCHAR(810))";
            try
            {
                Open();
                SqlCommand sqlCommand = new SqlCommand(SQLcmd, sqlconnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }
        }
        public static int InsertToTable(string name, DateTime date, string bitMap, string playerConstrains, string full, string clues, string player)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            string cmd = $"INSERT INTO {TABLE_NAME}({Table.NAME.ToString()},{Table.DATE.ToString()},{Table.BITMAP.ToString()}" +
                $",{Table.SUDOKUPLAYERCONSTRAINS.ToString()},{Table.SUDOKUFULL.ToString()},{Table.SUDOKUCLUES.ToString()},{Table.SUDOKUPLAYER.ToString()})" +
                $" VALUES('{name}','{date.ToString(format)}','{bitMap}','{playerConstrains}','{full}','{clues}','{player}')";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show("insert to sql"  + e.Message);

            }
            finally
            {
                Close();
            }

            return GetId(name);
        }
        public static int GetId(string name)
        {
            string cmd = $"SELECT {Table.ID} FROM {TABLE_NAME} WHERE {Table.NAME.ToString()} = '{name}'";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SqlDataReader reader = SQLcmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {

               // MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }
            return 0;
        }
        public static void UpdateTable(int id, object data, Table dataType)
        {
            string cmd = $"UPDATE {TABLE_NAME} SET {dataType.ToString()} = '{data}'";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.BeginExecuteNonQuery();

            }
            catch (Exception e)
            {

                //MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }

        }
        public static List<Tuple<int, string, byte[], DateTime>> ReadTableDataInfo()
        {
            List<Tuple<int, string, byte[], DateTime>> ids = new List<Tuple<int, string, byte[], DateTime>>();
            string cmd = $"SELECT {Table.ID.ToString()},{Table.NAME.ToString()},{Table.BITMAP.ToString()},{Table.DATE.ToString()} FROM {TABLE_NAME}";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SqlDataReader reader = SQLcmd.ExecuteReader();
                while (reader.Read())
                {
                    string bitmapData = reader.GetString(2);
                    string[] splitData = bitmapData.Split(' ');
                    byte[] bitmap = new byte[splitData.Length];
                    for (int i = 0; i < bitmap.Length; i++)
                    {
                        bitmap[i] = byte.Parse(splitData[i]);
                    }
                    Tuple<int, string, byte[], DateTime> data = new Tuple<int, string, byte[], DateTime>(reader.GetInt32(0)
                        , reader.GetString(1), bitmap,reader.GetDateTime(3));
                    ids.Add(data);

                }
            }
            catch (Exception e)
            {

                //MessageBox.Show("read sql  " + e.Message);
            }
            finally
            {
                Close();
            }
            return ids;
        }
        public static string[] ReadSudokuData(int id)
        {
            string[] data = new string[4];
            string cmd = $"SELECT {Table.SUDOKUFULL.ToString()},{Table.SUDOKUCLUES.ToString()},{Table.SUDOKUPLAYER.ToString()}," + 
                $"{Table.SUDOKUPLAYERCONSTRAINS.ToString()} FROM {TABLE_NAME} WHERE {Table.ID.ToString()} = {id}";
            try
            {

                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SqlDataReader reader = SQLcmd.ExecuteReader();
                while (reader.Read())
                {
                    data[0] = reader.GetString(0);
                    data[1] = reader.GetString(1);
                    data[2] = reader.GetString(2);
                    data[3] = reader.GetString(3);
                }
            }
            catch (Exception e)
            {

                //MessageBox.Show("read sql string data " + e.Message);
            }
            finally
            {
                Close();
            }
            return data;
        }
        public static void RemoveFromTable(int id)
        {
            string cmd = $"DELETE FROM {TABLE_NAME} WHERE {Table.ID.ToString()} = {id}";
            try
            {
                Open();
                Console.WriteLine("open");
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.ExecuteNonQuery();
                Console.WriteLine("data  removed");
            }
            catch (Exception e)
            {

                //MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }
        }
        public static int UpadteTable(int id,string name, DateTime date, string bitMap, string playerConstrains, string full, string clues, string player)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            string cmd = $"UPDATE {TABLE_NAME} SET {Table.NAME.ToString()} = '{name}', {Table.DATE.ToString()} = '{date.ToString(format)}', " +
                $"{Table.BITMAP.ToString()} = '{bitMap}', {Table.SUDOKUPLAYERCONSTRAINS.ToString()} = '{playerConstrains}'," +
                $" {Table.SUDOKUFULL.ToString()} = '{full}', {Table.SUDOKUCLUES.ToString()} = '{clues}', {Table.SUDOKUPLAYER.ToString()} = '{player}'" +
                $"WHERE {Table.ID.ToString()} = {id}";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("update to sql"  + e.Message);

            }
            finally
            {
                Close();
            }

            return GetId(name);
        }

        public static void DropTable()
        {
            string cmd = $"DROP table {TABLE_NAME}";
            try
            {
                Open();
                Console.WriteLine("open");
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.ExecuteNonQuery();
                Console.WriteLine("table  Droped");
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }
        }
        public static void DropDB()
        {
            string cmd = $"DROP DATABASE {DB_NAME}";
            try
            {
                Open();
                Console.WriteLine("open");
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.ExecuteNonQuery();
                Console.WriteLine("DB  Droped");
            }
            catch (Exception e)
            {

               // MessageBox.Show(e.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}

