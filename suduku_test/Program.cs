using System;
using Sudoku_Manger;

namespace suduku_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku_manger pain = new Sudoku_manger();
            pain.TestDoubleLines();
            //Console.WriteLine(pain.ToString());
        }
    }
}
