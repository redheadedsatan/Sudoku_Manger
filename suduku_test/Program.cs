using System;
using System.Data;
using System.Data.SqlClient;
using Sudoku_Manger;
using SQL_MANGER;

namespace suduku_test
{
    class Program
    {
        static void Main(string[] args)
        {
            SQL_DB_Manger.PrintTable();
            int id = SQL_DB_Manger.GetId("11");
            SQL_DB_Manger.PrintTable();
            Console.WriteLine(id);
            SQL_DB_Manger.RemoveFromTable(5);
            SQL_DB_Manger.PrintTable();

            //Sudoku_manger pain = new Sudoku_manger();
            //pain.CreateFullBoard();
            // Console.WriteLine(pain.ToString().Replace('0',' '));
        }

    }
}
