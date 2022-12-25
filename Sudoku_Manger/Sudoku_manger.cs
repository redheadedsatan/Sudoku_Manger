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
        int sqrt;

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
            sqrt = sudokuRegionSize;
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
            while (FillBoard() == false) ;
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
        private bool FillBoard()
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
                            return false;
                        }
                        int value = rng.Next(0, constraints[i, j].Count);
                        SetNum(i, j, constraints[i, j][value]);
                    }
                }
            }
            return true;
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
        //UNfinished #TODO
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
                    //TODO  return  add candidate and doublr line;
                }

            }
            return false;
        }
        private bool SetRegion(int regionNumber, int Value)
        {

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

        private bool DoubleLineRegion()
        {
            List<int>[] regionRowConstrains;
            List<int> constrainsInBoth = new List<int>();
            for (int i = 0; i < Sudoku_Size - sqrt; i++)
            {
                regionRowConstrains = GetLineConstarinsFromRegionRow(i);
                int[] countNum = GetNumCounter(regionRowConstrains);
                if (CheckForOneRowInRegion(regionRowConstrains, countNum, i))
                {
                    return true;
                }
                for (int j = i + sqrt; j < Sudoku_Size; j += sqrt)
                {
                    List<int>[] checkRegionBelow = GetLineConstarinsFromRegionRow(j);
                    if (CheckForOneRowInRegion(checkRegionBelow,GetNumCounter(checkRegionBelow), j))
                    {
                        return true;
                    }
                    constrainsInBoth = GetConstrainInBothRegions(regionRowConstrains, checkRegionBelow);
                    for (int z = 0; z < constrainsInBoth.Count; z++)
                    {

                    }
                }
            }
            return false;
        }
        private List<int>[] GetLineConstarinsFromRegionRow(int regionNum)
        {
            int row = regionNum / sqrt;
            int column = regionNum % sqrt;
            row *= sqrt;
            column *= sqrt;
            List<int>[] regionRowConstrains = new List<int>[sqrt];
            for (int i = 0; i < sqrt; i++)
            {
                regionRowConstrains[i] = new List<int>();
                for (int j = 0; j < sqrt; j++)
                {
                    int len = constraints[i + row, column + j].Count;
                    for (int z = 0; z < len; z++)
                    {
                        if (!regionRowConstrains[i].Contains(constraints[j + row, column + i][z]))
                        {
                            regionRowConstrains[i].Add(constraints[j + row, column + i][z]);
                        }
                    }

                }

            }

            return regionRowConstrains;
        }
        private int[] GetNumCounter(List<int>[] regionRowConstrains)
        {
            int[] countNum = new int[Sudoku_Size];
            for (int i = 0; i < Sudoku_Size; i++)
            {
                countNum[i] = 0;
            }
            for (int i = 0; i < regionRowConstrains.Length; i++)
            {
                for (int j = 0; j < regionRowConstrains[i].Count; j++)
                {
                    countNum[regionRowConstrains[i][j] - 1]++;
                }
            }
            return countNum;
        }
        //removes all cobstrains that happen 1 or sqrt time and remove candidate lines
        private bool CheckForOneRowInRegion(List<int>[] regionRowConstrains, int[] countNum, int regionNum)
        {
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (countNum[i] == 1 || countNum[i] == sqrt)
                {
                    for (int j = 0; j < sqrt; j++)
                    {
                        regionRowConstrains[j].Remove(i + 1);
                    }
                    if (countNum[i] == 1)
                    {
                        SingleCandidate(regionNum, i + 1);
                        return true;
                    }
                }

            }
            return false;
        }

        private List<int> GetConstrainInBothRegions(List<int>[] regionRowConstrains1, List<int>[] regionRowConstrains2)
        {
            List<int> constrainsInBoth = new List<int>();
            for (int i = 1; i <= Sudoku_Size; i++)
            {
                constrainsInBoth.Add(i);
            }
            for (int i = 0; i < sqrt; i++)
            {
                for (int j = 1; j <= Sudoku_Size; j++)
                {
                    if (regionRowConstrains1[i].Contains(j) != regionRowConstrains2[i].Contains(j))
                    {
                        constrainsInBoth.Remove(j);
                    }
                }
            }
            return constrainsInBoth;
        }

        private void SingleCandidate(int regionNum, int value)
        {
            int row = regionNum / sqrt;
            int column = regionNum % sqrt;
            row *= 3;
            column *= 3;
            for (int i = row; i < Sudoku_Size; i++)
            {
                for (int j = column; j < Sudoku_Size; j++)
                {
                    if (constraints[i, j].Contains(value))
                    {
                        RemoveLine(i, regionNum, value);
                        return;
                    }
                }
            }
        }
        private void RemoveLine(int row, int regionNum, int value)
        {
            int column = regionNum % sqrt;
            column *= 3;
            for (int i = 0; i < Column_Size; i++)
            {
                if (i < column || i >= column + sqrt)
                {
                    constraints[row, i].Remove(value);
                }
            }
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
