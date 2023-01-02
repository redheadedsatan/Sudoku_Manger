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
            SQL_DB_Manger.UpdateTable(1, "110",null,"245","356");
            SQL_DB_Manger.PrintTable();
            //Sudoku_manger pain = new Sudoku_manger();
            //pain.CreateFullBoard();
            // Console.WriteLine(pain.ToString().Replace('0',' '));
        }

    }
}
