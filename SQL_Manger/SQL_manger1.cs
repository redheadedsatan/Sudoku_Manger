using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SQL_Manger
{
    class SQL_manger1
    {
        private static string DB_NAME = "TEST";
        private static string CONNECTIONSTRING = "Server=localhost;Database=master;Trusted_Connection=True;";
        private static SqlConnection sqlconnection = null;


        public static void Open()
        {
            if (sqlconnection == null)
            {
                sqlconnection = new SqlConnection(CONNECTIONSTRING);
            }
            if (!CheckDatabaseExists())
            {
                CreateDataBase();
            }
            sqlconnection.Open();
        }
        private static bool CheckDatabaseExists()
        {
            bool result = false;
            try
            {
                string sqlCreateDBQuery = "SELECT database_id FROM sys.databases WHERE Name = " + DB_NAME;
                using (sqlconnection)
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

                        sqlconnection.Close();

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
                    Console.WriteLine(e.Message);
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
            string SQLcmd = "CREATE TABLE sudoku(ID INT PRIMARY KEY INDENTITY, NAME VARCHAR(50) NOT NULL, SUDOKUFULL TINYTEXT, SUDOKUCLUES TINYTEXT, SUDOKUPLAYER TINYTEXT)";
            try
            {
                Open();
                Console.WriteLine("connection Open");
                SqlCommand sqlCommand = new SqlCommand(SQLcmd, sqlconnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("tableCreated");
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
    }
}
