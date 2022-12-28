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
            Sudoku_manger sd;
            SQL_DB_Manger.CreateTable();
            //Sudoku_manger pain = new Sudoku_manger();
            //pain.CreateFullBoard();
            //Console.WriteLine(pain.ToString());
        }
        
    }
}
