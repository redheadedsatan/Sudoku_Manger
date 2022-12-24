using System;
using System.Collections.Generic;

namespace Sudoku_Manger
{

    public class Sudoku_manger
    {

        private int Row_size;
        private int Column_Size;
        private int Sudoku_Size;
        private int[,] FullBoard;
        private int[,] sudokuPlayer;
        private int[,] sudokuClues;
        private List<int>[,] constraints;
        private bool[,] isConstrainted;

        public Sudoku_manger(int sudokuRegionSize = 3)
        {
            SetSudokuSizes(sudokuRegionSize);
            Create_Sudoku();
        }
        public void SetSudokuSizes(int sudokuRegionSize = 3)
        {
            Row_size = (int)(Math.Pow(sudokuRegionSize, 2.0));
            Column_Size = (int)(Math.Pow(sudokuRegionSize, 2.0));
            Sudoku_Size = (int)(Math.Pow(sudokuRegionSize, 2.0));
        }
        private void Create_Sudoku()
        {
            sudokuPlayer = new int[Row_size, Column_Size];
            sudokuClues = new int[Row_size, Column_Size];
            isConstrainted = new bool[Row_size, Column_Size];
            constraints = new List<int>[Row_size, Column_Size];
            FullBoard = new int[Row_size, Column_Size];
        }

        #region testing
        public void TestSingleAndidate()
        {
            SetBoardToZero();
            List<int> tmp = new List<int>();
            tmp.Add(1);
            constraints[0, 0] = tmp;
            CheckForSingleCandidate();
        }

        public void TestSinglePostion()
        {
            SetBoardToZero();
            List<int> tmp = new List<int>();
            for (int i = 1; i < 8; i++)
            {
                tmp.Add(i);
            }
            for (int i = 1; i <= 8; i++)
            {
                constraints[0, i] = new List<int>(tmp);
            }
            CheckForSinglePostionCandidateLines();
        }
        public void TestCandidateLines()
        {
            SetBoardToZero();
            List<int> tmp = new List<int>();
            for (int i = 1; i <= 8; i++)
            {
                tmp.Add(i);
            }
            //constraints[0, 0] = new List<int>(tmp);
            //constraints[0, 1] = new List<int>(tmp);
            //constraints[0, 2] = new List<int>(tmp);
            constraints[1, 0] = new List<int>(tmp);
            constraints[1, 1] = new List<int>(tmp);
            constraints[1, 2] = new List<int>(tmp);
            constraints[2, 0] = new List<int>(tmp);
            constraints[2, 1] = new List<int>(tmp);
            constraints[2, 2] = new List<int>(tmp);
            CheckForSinglePostionCandidateLines();
        }
        #endregion
        public void CreateFullBoard()
        {
            SetBoardToZero();
            FillBoard();
            RemoveValues();
        }
        private void SetBoardToZero()
        {
            List<int> arrayInt = new List<int>();
            for (int i = 0; i < Sudoku_Size; i++)
            {
                arrayInt.Add(i + 1);
            }
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    sudokuPlayer[i, j] = 0;
                    constraints[i, j] = new List<int>(arrayInt);

                }
            }
        }
        private void FillBoard()
        {
            Random rng = new Random();
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    if (sudokuPlayer[i, j] == 0)
                    {
                        if (constraints[i, j].Count == 0)
                        {
                            Console.WriteLine("failed");
                            Console.WriteLine();
                            CreateFullBoard();
                            return;
                        }
                        int value = rng.Next(0, constraints[i, j].Count);
                        SetNum(i, j, constraints[i, j][value]);
                    }
                }
            }
        }
        private void RemoveValues()
        {
            SetBoard();
            RemoveRandomNumbers();
        }
        private void SetBoard()
        {
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    sudokuClues[i, j] = sudokuPlayer[i, j];
                    FullBoard[i, j] = sudokuPlayer[i, j];
                }
            }
        }

        private void RemoveRandomNumbers() 
        {
            
            int counter = 0;
            do
            {
                if (RemoveRandNumAndCheck())
                {
                    counter = 0;
                }
                else 
                {
                    counter++;
                }
                RestoreBoard();
            }
            while (counter < 10);
        }
        //removes random number and check if board is solvable restore org value if not solvable
        private bool RemoveRandNumAndCheck() 
        {
            Random rng = new Random();
            int row = rng.Next(0, Sudoku_Size);
            int column = rng.Next(0, Sudoku_Size);
            int data = sudokuPlayer[row, column];
            sudokuPlayer[row, column] = 0;
            sudokuClues[row, column] = 0;
            SetConstraints();
            if (!CheckBoard())
            {
                sudokuPlayer[row, column] = data;
                sudokuClues[row, column] = data;
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckBoard()
        {
            SetNum(0, 0, 0);
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    if (sudokuPlayer[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //restore playet suduke
        private void RestoreBoard() 
        {
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    sudokuPlayer[i, j] = sudokuClues[i, j];
                }
            }
        }
        private void SetConstraints()
        {
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    if (sudokuPlayer[i, j] == 0)
                    {
                        SetConstarinsCell(i, j);
                    }
                }
            }
        }
        private void SetConstarinsCell(int row, int column)
        {
            List<int> con = new List<int>();
            for (int i = 1; i <= Sudoku_Size; i++)
            {
                con.Add(i);
            }
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            int numOfBoxRow = row / sqrt;
            int numOfBoxCuolmn = column / sqrt;
            int rowi = 0;
            int columni = 0;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                con.Remove(sudokuPlayer[row, i]);
                con.Remove(sudokuPlayer[i, column]);
                con.Remove(sudokuPlayer[numOfBoxRow * 3 + rowi, numOfBoxCuolmn * 3 + columni]);
                rowi++;
                if (rowi == sqrt)
                {
                    rowi = 0;
                    columni++;
                }
                if (columni == sqrt)
                {
                    rowi = 0;
                }
            }
            for (int i = 0; i < con.Count; i++)
            {
                constraints[row, column].Add(con[i]);
            }
        }
        private void SetNum(int row, int column, int value)
        {
            if (value > 0 && value <= Sudoku_Size)
            {
                sudokuPlayer[row, column] = value;
                RemoveConstraints(row, column, sudokuPlayer[row, column]);
            }
            if (CheckForSinglePostionCandidateLines())
            {
                return;
            }
            else if (CheckForSingleCandidate())
            {
                return;
            }
        }
        private void RemoveConstraints(int row, int column, int value)
        {
            constraints[row, column].Clear();
            RemoveConstraintFromRow(row, value);
            RemoveConstraintFromColumn(column, value);
            RemoveConstraintFromRegion(row, column, value);

        }
        private void RemoveConstraintFromRow(int row, int value)
        {
            for (int i = 0; i < Row_size; i++)
            {
                constraints[row, i].Remove(value);
            }
        }
        private void RemoveConstraintFromColumn(int column, int value)
        {
            for (int i = 0; i < Row_size; i++)
            {
                constraints[i, column].Remove(value);
            }
        }
        private void RemoveConstraintFromRegion(int row, int column, int value)
        {
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            int numOfBoxRow = row / sqrt;
            int numOfBoxCuolmn = column / sqrt;
            int rowi = 0;
            int columni = 0;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                constraints[numOfBoxRow * 3 + rowi, numOfBoxCuolmn * 3 + columni].Remove(value);
                rowi++;
                if (rowi == sqrt)
                {
                    rowi = 0;
                    columni++;
                }
                if (columni == sqrt)
                {
                    rowi = 0;
                }
            }
        }
        private bool CheckForSingleCandidate()
        {
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    if (sudokuPlayer[i, j] == 0)
                    {
                        if (constraints[i, j].Count == 1)
                        {
                            SetNum(i, j, constraints[i, j][0]);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool CheckForSinglePostionCandidateLines()
        {
            int[] rowCounters = SetRowCounter();
            int[,] columnCounters = SetRegionColumnCounter();
            int[,] regionCounters = SetRegionColumnCounter();
            if (FindSinglePostionAndSetRow(rowCounters, columnCounters, regionCounters))
            {
                return true;
            }
            if (SetColumRegionCandidateLines(columnCounters, regionCounters))
            {
                return true;
            }
            return false;
        }
        private int[] SetRowCounter()
        {
            int[] arrayInt = new int[Row_size];
            for (int i = 0; i < arrayInt.Length; i++)
            {
                arrayInt[i] = 0;
            }
            return arrayInt;
        }
        private int[,] SetRegionColumnCounter()
        {
            int[,] arrayInt = new int[Row_size, Sudoku_Size];
            for (int i = 0; i < arrayInt.GetLength(0); i++)
            {
                for (int j = 0; j < arrayInt.GetLength(1); j++)
                {
                    arrayInt[i, j] = 0;
                }
            }
            return arrayInt;

        }
        private bool FindSinglePostionAndSetRow(int[] rowCounters, int[,] columnCounters, int[,] regionCounters)
        {
            for (int i = 0; i < Row_size; i++)
            {
                rowCounters = SetRowCounter();
                SortCountersInRow(rowCounters, columnCounters, regionCounters, i);
                if (SetRow(rowCounters, i))
                {
                    return true;
                }
            }
            return false;
        }
        private void SortCountersInRow(int[] rowCounters, int[,] columnCounters, int[,] regionCounters, int row)
        {
            const int FACTOR = 1;
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            for (int j = 0; j < Column_Size; j++)
            {
                if (sudokuPlayer[row, j] == 0)
                {
                    for (int i = 1; i <= Sudoku_Size; i++)
                    {
                        if (constraints[row, j].Contains(i))
                        {
                            rowCounters[i - FACTOR]++;
                            columnCounters[j, i - FACTOR]++;
                            regionCounters[(int)(row / sqrt + Math.Floor((double)j / sqrt) * sqrt), i - FACTOR]++;
                        }
                    }
                }

            }
        }
        private bool SetRow(int[] rowCounters, int row)
        {
            for (int j = 0; j < rowCounters.Length; j++)
            {
                if (rowCounters[j] == 1)
                {
                    for (int z = 0; z < Column_Size; z++)
                    {
                        if (constraints[row, z].Contains(j + 1))
                        {
                            SetNum(row, z, j + 1);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool SetColumRegionCandidateLines(int[,] columnCounters, int[,] regionCounters)
        {

            for (int i = 0; i < Sudoku_Size; i++)
            {
                for (int j = 0; j < Sudoku_Size; j++)
                {
                    if (SetCoulmn(columnCounters, i, j))
                    {
                        return true;
                    }
                    else if (SetRegionAndCheckCandidateLines(regionCounters, i, j))
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        private bool SetCoulmn(int[,] columnCounters, int coulmn, int value)
        {
            if (columnCounters[coulmn, value] == 1)
            {
                for (int i = 0; i < Row_size; i++)
                {
                    if (constraints[i, coulmn].Contains(value))
                    {
                        SetNum(i, coulmn, value + 1);
                        return true;
                    }
                }
            }
            return false;
        }
        private bool SetRegionAndCheckCandidateLines(int[,] regionCounters, int Region, int Value)
        {
            if (regionCounters[Region, Value] >= 1 && regionCounters[Region, Value] <= Math.Sqrt(Sudoku_Size))
            {

                if (regionCounters[Region, Value] == 1)
                {
                    if (SetRegion(Region, Value))
                    {
                        return true;
                    }
                }
                else
                {
                    return CandidateLines(regionCounters, Region, Value);
                }

            }
            return false;
        }
        private bool SetRegion(int regionNumber, int Value)
        {
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            int row = regionNumber / 3;
            int column = regionNumber % 3;
            row *= 3;
            column *= 3;
            for (int inRegionRow = 0; inRegionRow < sqrt; inRegionRow++)
            {
                for (int inRegionColumn = 0; inRegionColumn < sqrt; inRegionColumn++)
                {
                    if (constraints[inRegionRow + row, inRegionColumn + column].Contains(Value + 1))
                    {
                        SetNum(inRegionRow + row, inRegionColumn + column, Value + 1);
                        return true;
                    }
                }
            }
            return false;
        }
        //finds the posible first candidate line and than looks at the row aqnd coulms to check candidate line
        private bool CandidateLines(int[,] regionCounters, int regionNumber, int Value)
        {
            int countOfNumInRegion = regionCounters[regionNumber, Value];
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            int row = regionNumber / 3;
            int column = regionNumber % 3;
            row *= 3;
            column *= 3;
            for (int inRegionRow = 0; inRegionRow < sqrt; inRegionRow++)
            {
                for (int inRegionColumn = 0; inRegionColumn < sqrt; inRegionColumn++)
                {
                    //find first candidate line
                    if (constraints[inRegionRow + row, inRegionColumn + column].Contains(Value + 1) && sudokuPlayer[inRegionRow + row, inRegionColumn + column] != 0)
                    {
                        if (SetRegoinRow(inRegionRow, countOfNumInRegion, row, inRegionColumn, column, Value + 1))
                        {
                            return true;
                        }
                        if (SetRegionColumn(inRegionRow, countOfNumInRegion, row, inRegionColumn, column, Value + 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //cheks the row for additinal candidate line 
        private bool SetRegoinRow(int inRegionRow, int countOfNumInRegion, int row, int inRegionColumn, int column, int Value)
        {
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            int counter = 1;
            int index = 1;
            while (inRegionRow + index < sqrt && counter < countOfNumInRegion)
            {
                if (constraints[inRegionRow + row + index, inRegionColumn + column].Contains(Value))
                {
                    if (++counter == countOfNumInRegion)
                    {
                        RemoveConstraintFromRow(inRegionColumn + column, Value);
                        constraints[inRegionRow + row, inRegionColumn + column].Add(Value);
                        if (countOfNumInRegion == sqrt)
                        {
                            constraints[inRegionRow + row + index, inRegionColumn + column].Add(Value);
                            constraints[inRegionRow + row + index - 1, inRegionColumn + column].Add(Value);
                        }
                        else if (countOfNumInRegion != 1)
                        {
                            constraints[inRegionRow + row + index, inRegionColumn + column].Add(Value);
                        }
                        SetNum(0, 0, 0);
                        return true;
                    }
                }
                index++;
            }
            return false;
        }
        //cheks the column for additinal candidate line 
        private bool SetRegionColumn(int inRegionRow, int countOfNumInRegion, int row, int inRegionColumn, int column, int Value)
        {
            int sqrt = (int)Math.Sqrt(Sudoku_Size);
            int counter = 1;
            int index = 1;
            while (inRegionColumn + index < sqrt && counter < countOfNumInRegion)
            {
                if (constraints[inRegionRow + row, inRegionColumn + column + index].Contains(Value))
                {
                    if (++counter == countOfNumInRegion)
                    {
                        RemoveConstraintFromColumn(inRegionRow + row, Value);
                        constraints[inRegionRow + row, inRegionColumn + column].Add(Value);
                        if (countOfNumInRegion == sqrt)
                        {
                            constraints[inRegionRow + row, inRegionColumn + column + index].Add(Value);
                            constraints[inRegionRow + row, inRegionColumn + column + index - 1].Add(Value);
                        }
                        else if (countOfNumInRegion != 1)
                        {
                            constraints[inRegionRow + row, inRegionColumn + column + index].Add(Value);
                        }
                        SetNum(0, 0, 0);
                        return true;
                    }
                }
                index++;
            }
            return false;
        }
        public override string ToString()
        {
            string data = "";
            for (int i = 0; i < Sudoku_Size; i++)
            {
                for (int j = 0; j < Sudoku_Size; j++)
                {
                    if (sudokuPlayer[i, j] == 0)
                    {
                        data += "|" + " ";
                    }
                    else
                    {
                        data += "|" + sudokuPlayer[i, j];
                    }
                }
                data += "|\n";
            }
            return data;
        }

    }
}
