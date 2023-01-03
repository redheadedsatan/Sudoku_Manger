using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SQL_MANGER
{
    public enum Table
    {
        ID,
        NAME,
        SUDOKUFULL,
        SUDOKUCLUES,
        SUDOKUPLAYER

    }
    public class SQL_DB_Manger
    {
        private static string TABLE_NAME = "sudoku";
        private static string DB_NAME = "TEST";
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
                Console.WriteLine(e.Message);
            }

            sqlconnection.Open();
        }
        private static bool CheckDatabaseExists()
        {
            bool result = false;
            try
            {
                SqlConnection sqlconnectionCheck = new SqlConnection("server = (local)\\SQLEXPRESS; Trusted_Connection = yes");
                string sqlCreateDBQuery = "SELECT database_id FROM sys.databases WHERE Name = " + DB_NAME;
                using (sqlconnectionCheck)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, sqlconnection))
                    {
                        sqlconnection.Open();

                        object resultObj = sqlCmd.ExecuteScalar();

                        int databaseID = 0;

                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }

                        sqlconnectionCheck.Close();

                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }
        private static void CreateDataBase()
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
                    // Console.WriteLine(e.Message);
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
            string SQLcmd = $"CREATE TABLE {TABLE_NAME}({ Table.ID.ToString()} INTEGER PRIMARY KEY IDENTITY, {Table.NAME.ToString()}" +
                $"VARCHAR(50) NOT NULL, {Table.SUDOKUFULL.ToString()} VARCHAR(200), {Table.SUDOKUCLUES.ToString()} VARCHAR(200), {Table.SUDOKUPLAYER.ToString()} VARCHAR(200))";
            try
            {
                Open();
                SqlCommand sqlCommand = new SqlCommand(SQLcmd, sqlconnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        public static int InsertToTable(string name, string full, string clues, string player)
        {
            
            string cmd = $"INSERT INTO {TABLE_NAME}({Table.NAME.ToString()},{Table.SUDOKUFULL.ToString()},{Table.SUDOKUCLUES.ToString()},{Table.SUDOKUPLAYER.ToString()})" +
                $" VALUES('{name}','{full}','{clues}','{player}')";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
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

                Console.WriteLine(e.Message);
            }
            finally
            {
                Close();
            }
            return 0;
        }
        public static void UpdateTable(int id, string name = null, string full = null, string clues = null, string player = null)
        {
            string cmd = BuildUpdateTablecmd(id, name, full, clues, player);
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SQLcmd.BeginExecuteNonQuery();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                Close();
            }

        }
        private static string BuildUpdateTablecmd(int id, string name = null, string full = null, string clues = null, string player = null)
        {
            string startcmd = $"UPDATE {TABLE_NAME} SET ";
            string cmd = startcmd;
            if (name != null)
            {
                cmd += $"{Table.NAME.ToString()} = '{name}'";
            }
            if (full != null)
            {
                if (cmd != startcmd)
                {
                    cmd += ",";
                }
                cmd += $" {Table.SUDOKUFULL.ToString()}= '{full}'";
            }
            if (clues != null)
            {
                if (cmd != startcmd)
                {
                    cmd += ",";
                }
                cmd += $" {Table.SUDOKUCLUES.ToString()}= '{clues}'";
            }
            if (player != null)
            {
                if (cmd != startcmd)
                {
                    cmd += ",";
                }
                cmd += $" {Table.SUDOKUPLAYER.ToString()}= '{player}'";
            }
            if (cmd != startcmd)
            {
                cmd += $" WHERE {Table.ID.ToString()} = {id}";
            }
            else
            {
                return "";
            }
            return cmd;
        }
        public static void PrintTable()
        {
            string cmd = $"SELECT * FROM {TABLE_NAME}";
            try
            {
                Open();
                SqlCommand SQLcmd = new SqlCommand(cmd, sqlconnection);
                SqlDataReader reader = SQLcmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetString(3)}, {reader.GetString(4)}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                Close();
            }
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

                Console.WriteLine(e.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}

