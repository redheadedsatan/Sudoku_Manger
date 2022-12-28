using System;
using System.Data;
using System.Data.SqlClient;
using Sudoku_Manger;
using SQL_Manger;

namespace suduku_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku_manger sd;
            
            Console.ReadKey();
            //Sudoku_manger pain = new Sudoku_manger();
            //pain.CreateFullBoard();
            //Console.WriteLine(pain.ToString());
        }
        static void CreateDB()
        {
            string connectioString = "Server=localhost;Database=master;Trusted_Connection=True;";
            string cmdText = "CREATE database TEST";
            SqlConnection sqlconnection = new SqlConnection(connectioString);
            Console.WriteLine("try");
            if (sqlconnection.State != ConnectionState.Open)
            {
                try
                {
                    sqlconnection.Open();
                    Console.WriteLine("OPEN");
                    SqlCommand sqlCommand = new SqlCommand(cmdText,sqlconnection);
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("create");
                    
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
    }
}
