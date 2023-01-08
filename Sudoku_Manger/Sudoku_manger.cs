using System;
using System.Collections.Generic;

namespace Sudoku_Manger
{

    public class Sudoku_manger
    {
        int counterSinglePos = 0;
        int counterSingleCandidate = 0;
        int counterCandidateLine = 0;
        int counterDoubleLine = 0;
        int counterNakedlines = 0;
        public int sqlId;
        private int Row_size;
        private int Column_Size;
        private int Sudoku_Size;
        private int[,] FullBoard;
        public int[,] sudokuPlayer;
        public int[,] sudokuClues;
        private List<int>[,] constraints;
        private bool[,] isConstrainted;
        int sqrt;

        
        public Sudoku_manger(int sudokuRegionSize = 3)
        {
            SetSudokuSizes(sudokuRegionSize);
            Create_Sudoku();
        }
        public Sudoku_manger(int id, string full, string clues, string player) 
        {
            sqlId = id;
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

        public void CreateFullBoard()
        {
            do
            {
                SetBoardToZero();
            }
            while (FillBoard() == false);

            //CheckSet_andstop();
            RemoveValues();
            //CheckSet_andstop();
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
        private void SetNum(int row, int column, int value)
        {
            if (value > 0 && value <= Sudoku_Size)
            {
                sudokuPlayer[row, column] = value;
                RemoveConstraints(row, column, value);
            }

            //CheckSet_andstop();

            if (CheckForSingleCandidate())
            {
                counterSingleCandidate++;
                return;
            }
            else if (CheckForSinglePostion())
            {
                counterSinglePos++;
                return;
            }
            else if (CandidateLine())
            {
                counterCandidateLine++;
                return;
            }
            else if (DoubleLineRegion())
            {
                counterDoubleLine++;
                return;
            }
            else if (NakedLines())
            {
                counterNakedlines++;
                return;
            }
        }
        #region Constarins
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
                constraints[numOfBoxRow * sqrt + rowi, numOfBoxCuolmn * sqrt + columni].Remove(value);
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
        #endregion


        #region singleCandidate
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
        #endregion

        #region SinglePostion
        private bool CheckForSinglePostion()
        {
            int[] rowCounters = SetRowCounter();
            int[,] columnCounters = SetRegionColumnCounter();
            int[,] regionCounters = SetRegionColumnCounter();
            if (FindSinglePostionAndSetRow(rowCounters, columnCounters, regionCounters))
            {
                return true;
            }
            if (SetColumRegion(columnCounters, regionCounters))
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
                            regionCounters[(int)(sqrt * (Math.Floor((double)row / sqrt)) + Math.Floor((double)j / sqrt)), i - FACTOR]++;
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
        private bool SetColumRegion(int[,] columnCounters, int[,] regionCounters)
        {

            for (int i = 0; i < Sudoku_Size; i++)
            {
                for (int j = 0; j < Sudoku_Size; j++)
                {
                    if (SetCoulmn(columnCounters, i, j))
                    {
                        return true;
                    }
                    else if (SetRegion(regionCounters, i, j))
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        private bool SetCoulmn(int[,] columnCounters, int column, int value)
        {
            if (columnCounters[column, value] == 1)
            {
                for (int i = 0; i < Row_size; i++)
                {
                    if (constraints[i, column].Contains(value + 1))
                    {

                        SetNum(i, column, value + 1);
                        return true;
                    }
                }
            }
            return false;
        }
        private bool SetRegion(int[,] regionCounters, int Region, int Value)
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
            }
            return false;
        }
        private bool SetRegion(int regionNumber, int Value)
        {

            int row = regionNumber / sqrt;
            int column = regionNumber % sqrt;
            row *= sqrt;
            column *= sqrt;
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
        #endregion

        #region DoubleLines
        private bool DoubleLineRegion()
        {
            bool remove = false;
            List<int>[] regionRowConstrains = new List<int>[sqrt];
            List<int>[] regionCoulmnConstrains = new List<int>[sqrt];
            for (int i = 0; i < Sudoku_Size - sqrt; i++)
            {
                GetLineConstarinsFromRegion(i, regionRowConstrains, regionCoulmnConstrains);
                int[] countNumRow = new int[Sudoku_Size];
                int[] countNumCoulm = new int[Sudoku_Size];
                GetNumCounter(regionRowConstrains, regionCoulmnConstrains, countNumRow, countNumCoulm);
                if (CheckDoubleLineRow(i, regionRowConstrains))
                {
                    remove = true;
                }
                if (CheckDoubleLineCoulmn(i, regionCoulmnConstrains))
                {
                    remove = true;
                }
            }

            return remove;
        }
        private void GetLineConstarinsFromRegion(int regionNum, List<int>[] regionConstrainsRow, List<int>[] regionConstrainsColumn)
        {
            int row = regionNum / sqrt;
            int column = regionNum % sqrt;
            row *= sqrt;
            column *= sqrt;
            for (int i = 0; i < sqrt; i++)
            {
                regionConstrainsRow[i] = new List<int>();
                regionConstrainsColumn[i] = new List<int>();
            }
            for (int i = 0; i < sqrt; i++)
            {

                for (int j = 0; j < sqrt; j++)
                {
                    int len = constraints[j + row, column + i].Count;
                    for (int z = 0; z < len; z++)
                    {
                        if (!regionConstrainsRow[i].Contains(constraints[j + row, column + i][z]))
                        {
                            regionConstrainsRow[i].Add(constraints[j + row, column + i][z]);
                        }
                        if (!regionConstrainsColumn[j].Contains(constraints[j + row, column + i][z]))
                        {
                            regionConstrainsColumn[j].Add(constraints[j + row, column + i][z]);
                        }
                    }

                }

            }
        }
        private void GetNumCounter(List<int>[] regionRowConstrains, List<int>[] regionCoulmnConstrains, int[] countNumRow, int[] countNumCoulmnn)
        {

            for (int i = 0; i < Sudoku_Size; i++)
            {
                countNumRow[i] = 0;
                countNumCoulmnn[i] = 0;
            }
            for (int i = 0; i < regionRowConstrains.Length; i++)
            {
                for (int j = 0; j < regionRowConstrains[i].Count; j++)
                {
                    countNumRow[regionRowConstrains[i][j] - 1]++;
                }
                for (int j = 0; j < regionCoulmnConstrains[i].Count; j++)
                {
                    countNumCoulmnn[regionCoulmnConstrains[i][j] - 1]++;
                }
            }
        }
        //removes all cobstrains that happen 1 or sqrt time
        private bool CheckForOneLineInRegion(List<int>[] regionRowConstrains, List<int>[] regionCoulmnConstrains, int[] countNumRow, int[] countNumCoulmn, int regionNum)
        {
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (countNumRow[i] <= 1 || countNumRow[i] >= sqrt)
                {
                    for (int j = 0; j < sqrt; j++)
                    {
                        regionRowConstrains[j].Remove(i + 1);
                    }

                }
                if (countNumCoulmn[i] <= 1 || countNumCoulmn[i] >= sqrt)
                {
                    for (int j = 0; j < sqrt; j++)
                    {
                        regionCoulmnConstrains[j].Remove(i + 1);
                    }
                }

            }
            return false;
        }
        private bool CheckDoubleLineRow(int region, List<int>[] regionRowConstrains)
        {
            bool remove = false;
            for (int j = region + sqrt; j < Sudoku_Size; j += sqrt)
            {
                List<int> constrainsInBothRow = new List<int>();
                List<int>[] checkRegionBelowRow = new List<int>[sqrt];
                List<int>[] checkRegionBelowCoulmn = new List<int>[sqrt];
                GetLineConstarinsFromRegion(j, checkRegionBelowRow, checkRegionBelowCoulmn);
                int[] numCounterTempRow = new int[Sudoku_Size];
                int[] numCounterTempCoulmn = new int[Sudoku_Size];
                GetNumCounter(checkRegionBelowRow, checkRegionBelowCoulmn, numCounterTempRow, numCounterTempCoulmn);
                if (CheckForOneLineInRegion(checkRegionBelowRow, checkRegionBelowCoulmn, numCounterTempRow, numCounterTempCoulmn, j))
                {
                    remove = true;
                }
                constrainsInBothRow = GetConstrainInBothRegions(regionRowConstrains, checkRegionBelowRow);
                if (constrainsInBothRow.Count > 0)
                {
                    for (int z = 0; z < constrainsInBothRow.Count; z++)
                    {
                        if (RemoveDoubleLine(region, j, constrainsInBothRow[z], regionRowConstrains, true))
                        {

                            SetNum(0, 0, 0);
                            remove = true;
                        }
                    }
                }
            }
            return remove;
        }
        private List<int> GetConstrainInBothRegions(List<int>[] regionRowConstrains1, List<int>[] regionRowConstrains2)
        {
            List<int> constrainsInBoth = new List<int>();
            for (int i = 0; i < regionRowConstrains1.Length; i++)
            {
                for (int j = 0; j < regionRowConstrains1[i].Count; j++)
                {
                    if (!constrainsInBoth.Contains(regionRowConstrains1[i][j]))
                    {
                        constrainsInBoth.Add(regionRowConstrains1[i][j]);
                    }
                }

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
        private bool RemoveDoubleLine(int region1, int region2, int value, List<int>[] lines, bool isRow)
        {
            bool removed = false;
            int row1 = region1 / sqrt;
            int column1 = region1 % sqrt;
            row1 *= sqrt;
            column1 *= sqrt;
            int column2 = region2 % sqrt;
            int row2 = region2 / sqrt;
            column2 *= sqrt;
            row2 *= sqrt;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(value))
                {
                    for (int j = 0; j < Column_Size; j++)
                    {
                        if (isRow)
                        {
                            if (!((j >= row1 && j < row1 + sqrt) || (j >= row2 && j < row2 + sqrt)))
                            {
                                if (constraints[j, column1 + i].Remove(value))
                                {
                                    removed = true;
                                }
                            }

                        }
                        else
                        {
                            if (!((j >= column1 && j < column1 + sqrt) || (j >= column2 && j < column2 + sqrt)))
                            {
                                if (constraints[row1 + i, j].Remove(value))
                                {
                                    removed = true;
                                }
                            }
                        }
                    }
                }
            }

            return removed;
        }
        private bool CheckDoubleLineCoulmn(int region, List<int>[] regionCoulmnConstrains)
        {
            List<int> constrainsInBothCoulmn = new List<int>();
            bool remove = false;
            for (int j = region + 1; j < Sudoku_Size; j += sqrt)
            {
                List<int>[] checkRegionBelowRow = new List<int>[sqrt];
                List<int>[] checkRegionBelowCoulmn = new List<int>[sqrt];
                GetLineConstarinsFromRegion(j, checkRegionBelowRow, checkRegionBelowCoulmn);
                int[] numCounterTempRow = new int[Sudoku_Size];
                int[] numCounterTempCoulmn = new int[Sudoku_Size];
                GetNumCounter(checkRegionBelowRow, checkRegionBelowCoulmn, numCounterTempRow, numCounterTempCoulmn);
                if (CheckForOneLineInRegion(checkRegionBelowRow, checkRegionBelowCoulmn, numCounterTempRow, numCounterTempCoulmn, j))
                {
                    remove = true;
                }
                constrainsInBothCoulmn = GetConstrainInBothRegions(regionCoulmnConstrains, checkRegionBelowCoulmn);
                if (constrainsInBothCoulmn.Count > 0)
                {
                    for (int z = 0; z < constrainsInBothCoulmn.Count; z++)
                    {
                        if (RemoveDoubleLine(region, j, constrainsInBothCoulmn[z], regionCoulmnConstrains, false))
                        {

                            SetNum(0, 0, 0);

                            remove = true;
                        }
                    }
                }
            }
            return remove;
        }
        #endregion

        #region CandidateLine
        private bool CandidateLine()
        {
            bool remove = false;
            List<int>[] regionRowConstrains = new List<int>[sqrt];
            List<int>[] regionCoulmnConstrains = new List<int>[sqrt];
            for (int i = 0; i < Sudoku_Size - sqrt; i++)
            {
                GetLineConstarinsFromRegion(i, regionRowConstrains, regionCoulmnConstrains);
                int[] countNumRow = new int[Sudoku_Size];
                int[] countNumCoulm = new int[Sudoku_Size];
                GetNumCounter(regionRowConstrains, regionCoulmnConstrains, countNumRow, countNumCoulm);
                if (CheckForCandidateLine(countNumRow, countNumCoulm, i))
                {
                    remove = true;
                }
            }
            return remove;
        }
        private bool CheckForCandidateLine(int[] countNumRow, int[] countNumCoulmn, int regionNum)
        {
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (countNumRow[i] == 1)
                {
                    if (CandidateLine(regionNum, i + 1, true))
                    {
                        return true;
                    }
                }
                if (countNumCoulmn[i] <= 1 || countNumCoulmn[i] >= sqrt)
                {

                    if (countNumCoulmn[i] == 1)
                    {
                        if (CandidateLine(regionNum, i + 1, false))
                        {
                            return true;
                        }
                    }


                }
            }
            return false;
        }
        private bool CandidateLine(int regionNum, int value, bool isRow)
        {
            int row = regionNum / sqrt;
            int column = regionNum % sqrt;
            row *= sqrt;
            column *= sqrt;
            for (int i = row; i < row + sqrt; i++)
            {
                for (int j = column; j < column + sqrt; j++)
                {
                    if (constraints[i, j].Contains(value))
                    {
                        if (isRow)
                        {
                            if (RemoveLineOutsideRegion(j, regionNum, value, isRow))
                            {
                                SetNum(0, 0, 0);

                                return true;
                            }
                        }
                        else
                        {
                            if (RemoveLineOutsideRegion(i, regionNum, value, isRow))
                            {
                                SetNum(0, 0, 0);

                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
            return false;
        }
        private bool RemoveLineOutsideRegion(int Line, int regionNum, int value, bool isRow)
        {
            bool removed = false;
            int row = regionNum / sqrt;
            int column = regionNum % sqrt;
            row *= sqrt;
            column *= sqrt;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (isRow)
                {
                    if (i < row || i >= row + sqrt)
                    {
                        if (constraints[i, Line].Remove(value))
                        {
                            removed = true;
                        }
                    }
                }
                else
                {
                    if (i < column || i >= column + sqrt)
                    {
                        if (constraints[Line, i].Remove(value))
                        {
                            removed = true;
                        }
                    }
                }
            }
            return removed;
        }
        #endregion

        #region Naked Lines
        private bool NakedLines()
        {
            bool removed = false;
            for (int i = 0; i < Row_size; i++)
            {
                if (CheckLines(i))
                {
                    removed = true;
                }
            }
            return removed;
        }
        private bool CheckLines(int index)
        {
            bool removed = false;
            List<int> rowCells;
            List<int> columnCells;
            List<int> regionCells;
            int regionrow = index / sqrt;
            int regioncolumn = index % sqrt;
            regionrow *= sqrt;
            regioncolumn *= sqrt;
            int Regionrowi = regionrow;
            int Regioncolumni = regioncolumn;
            List<int>[] row = new List<int>[Sudoku_Size];
            List<int>[] column = new List<int>[Sudoku_Size];
            List<int>[] region = new List<int>[Sudoku_Size];
            SetLines(row, column, region, index);
            for (int i = 0; i < Sudoku_Size; i++)
            {
                rowCells = CheckNakedLine(constraints[index, i], row);
                columnCells = CheckNakedLine(constraints[i, index], column);
                regionCells = CheckNakedLine(constraints[Regionrowi, Regioncolumni], region);
                if (constraints[index, i].Count == rowCells.Count)
                {
                    if (RemoveNakedLine(constraints[index, i], rowCells, index, true))
                    {

                        removed = true;
                    }
                }
                if (constraints[i, index].Count == columnCells.Count)
                {
                    if (RemoveNakedLine(constraints[i, index], columnCells, index, false))
                    {
                        removed = true;
                    }
                }
                if (constraints[Regionrowi, Regioncolumni].Count == regionCells.Count)
                {
                    if (RemoveNakedRegion(constraints[Regionrowi, Regioncolumni], regionCells, index))
                    {
                        removed = true;
                    }
                }
                Regioncolumni++;
                if (Regioncolumni == regioncolumn + sqrt)
                {
                    Regioncolumni = regioncolumn;
                    Regionrowi++;
                }
            }
            return removed;
        }
        private void SetLines(List<int>[] row, List<int>[] column, List<int>[] region, int index)
        {
            int Regionrow = index / sqrt;
            int Regioncolumn = index % sqrt;
            Regionrow *= sqrt;
            Regioncolumn *= sqrt;
            int Regionrowi = Regionrow;
            int Regioncolumni = Regioncolumn;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                row[i] = new List<int>(constraints[index, i]);
                column[i] = new List<int>(constraints[i, index]);
                region[i] = new List<int>(constraints[Regionrowi, Regioncolumni]);
                Regioncolumni++;
                if (Regioncolumni == Regioncolumn + sqrt)
                {
                    Regioncolumni = Regioncolumn;
                    Regionrowi++;
                }
            }
        }
        private List<int> CheckNakedLine(List<int> constarinsInCell, List<int>[] line)
        {
            List<int> cells = new List<int>();
            for (int i = 0; i < Column_Size; i++)
            {
                bool match = true;
                if (line[i].Count < 1)
                {
                    match = false;
                }
                for (int j = 0; j < line[i].Count; j++)
                {
                    if (!constarinsInCell.Contains(line[i][j]))
                    {
                        match = false;
                    }
                }
                if (match)
                {
                    cells.Add(i);
                }
            }
            return cells;
        }
        private bool RemoveNakedLine(List<int> remove, List<int> protectedCells, int line, bool isRow)
        {
            bool removed = false;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (!protectedCells.Contains(i))
                {
                    for (int j = 0; j < remove.Count; j++)
                    {
                        if (isRow)
                        {
                            if (constraints[line, i].Remove(remove[j]))
                            {

                                removed = true;
                            }
                        }
                        else
                        {
                            if (constraints[i, line].Remove(remove[j]))
                            {
                                removed = true;
                            }
                        }
                    }
                }
            }
            return removed;
        }
        private bool RemoveNakedRegion(List<int> remove, List<int> protectedCells, int region)
        {
            bool removed = false;
            int regionrow = region / sqrt;
            int regioncolumn = region % sqrt;
            regionrow *= sqrt;
            regioncolumn *= sqrt;
            for (int i = regionrow; i < regionrow + sqrt; i++)
            {
                for (int j = regioncolumn; j < regioncolumn + sqrt; j++)
                {
                    if (!protectedCells.Contains((i - regionrow) * sqrt + (j - regioncolumn)))
                    {
                        for (int z = 0; z < remove.Count; z++)
                        {

                            if (constraints[i, j].Remove(remove[z]))
                            {

                                removed = true;
                            }

                        }
                    }
                }
            }
            return removed;

        }
        #endregion

        #region RemoveValueOfCelsForSuduk
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
            bool prv = true;
            int counter = 0;
            do
            {
                if (RemoveRandNumAndCheck(prv))
                {
                    counter = 0;
                    prv = true;
                }
                else
                {
                    prv = false;
                    counter++;
                }
                RestoreBoard();

            }
            while (counter < 10);
        }
        //removes random number and check if board is solvable restore org value if not solvable
        private bool RemoveRandNumAndCheck(bool prv)
        {
            int row;
            int column;
            Random rng = new Random();

            row = rng.Next(0, Sudoku_Size);
            column = rng.Next(0, Sudoku_Size);

            int data = sudokuPlayer[row, column];
            if (data == 0)
            {
                return prv;
            }

            sudokuPlayer[row, column] = 0;
            sudokuClues[row, column] = 0;
            SetConstraints();
            //Console.WriteLine($"removed \nrow: {row + 1} \ncolumn: {column + 1}");
            //CheckSet_andstop();
            ResetCounters();
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
                con.Remove(sudokuPlayer[numOfBoxRow * sqrt + rowi, numOfBoxCuolmn * sqrt + columni]);
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
        private void ResetCounters() 
        {
            counterSinglePos = 0;
            counterSingleCandidate = 0;
            counterCandidateLine = 0;
            counterDoubleLine = 0;
            counterNakedlines = 0;
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
        #endregion

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
            CheckForSinglePostion();
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
            constraints[0, 1] = new List<int>(tmp);
            constraints[0, 2] = new List<int>(tmp);
            //constraints[1, 0] = new List<int>(tmp);
            constraints[1, 1] = new List<int>(tmp);
            constraints[1, 2] = new List<int>(tmp);
            //constraints[2, 0] = new List<int>(tmp);
            constraints[2, 1] = new List<int>(tmp);
            constraints[2, 2] = new List<int>(tmp);
            DoubleLineRegion();
            CheckSet_andstop();
        }
        public void TestDoubleLines()
        {
            SetBoardToZero();
            List<int> tmp = new List<int>();
            for (int i = 1; i <= 8; i++)
            {
                tmp.Add(i);
            }
            constraints[0, 1] = new List<int>(tmp);
            constraints[1, 1] = new List<int>(tmp);
            constraints[2, 1] = new List<int>(tmp);

            constraints[3, 1] = new List<int>(tmp);
            constraints[4, 1] = new List<int>(tmp);
            constraints[5, 1] = new List<int>(tmp);

            constraints[1, 0] = new List<int>(tmp);
            constraints[1, 1] = new List<int>(tmp);
            constraints[1, 2] = new List<int>(tmp);

            constraints[1, 3] = new List<int>(tmp);
            constraints[1, 4] = new List<int>(tmp);
            constraints[1, 5] = new List<int>(tmp);
            DoubleLineRegion();
            CheckSet_andstop();
        }
        public void TestNakedLines()
        {
            SetBoardToZero();
            List<int> tmp = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                tmp.Add(i + 1);


            }
            constraints[0, 0] = new List<int>(tmp);
            constraints[1, 1] = new List<int>(tmp);
            constraints[2, 2] = new List<int>(tmp);
            CheckSet_andstop();
            NakedLines();


            CheckSet_andstop();
        }
        public void TestSolve()
        {
            SetBoardToZero();

        }
        private void CheckSet_andstop()
        {
            Console.WriteLine(this.ToStringPlayer().Replace('0', ' '));
            string data = "";
            for (int i = 0; i < Sudoku_Size; i++)
            {
                for (int j = 0; j < Sudoku_Size; j++)
                {

                    data += "|";
                    for (int z = 1; z <= Sudoku_Size; z++)
                    {
                        if (constraints[i, j].Contains(z))
                        {
                            data += z + ",";
                        }
                        else
                        {
                            data += " ,";
                        }

                    }


                }
                data += "|\n";
            }
            Console.WriteLine(data);
            Console.ReadKey();
        }

        #endregion

        #region to strings
        public string ToStringPlayer()
        {
            string data = "";
            for (int i = 0; i < Sudoku_Size; i++)
            {
                data += sudokuPlayer[i, 0];
                for (int j = 1; j < Sudoku_Size; j++)
                {
                    data += "," + sudokuPlayer[i, j];
                }
                data += "\n";
            }
            return data;
        }
        public string ToStringClues()
        {
            string data = "";
            for (int i = 0; i < Sudoku_Size; i++)
            {
                data += sudokuClues[i, 0];
                for (int j = 1; j < Sudoku_Size; j++)
                {
                    data += "," + sudokuClues[i, j];
                }
                data += "\n";
            }
            return data;
        }
        public string ToStringFull()
        {
            string data = "";
            for (int i = 0; i < Sudoku_Size; i++)
            {
                data += FullBoard[i, 0];
                for (int j = 1; j < Sudoku_Size; j++)
                {
                    data += "," + FullBoard[i, j];
                }
                data += "\n";
            }
            return data;
        }
        #endregion

        public void SetPlayerNumber(int row,int column,int value) 
        {
            sudokuPlayer[row, column] = value;
        }
        public List<Tuple<int,int>> checkSudoku() 
        {
            List<Tuple<int, int>> wrongCells = new List<Tuple<int, int>>();
            for (int i = 0; i < Row_size; i++)
            {
                for (int j = 0; j < Column_Size; j++)
                {
                    if (sudokuPlayer[i, j] != 0 && (CheckRow(i,sudokuPlayer[i,j]) || CheckColumn(j, sudokuPlayer[i, j]) || CheckRegion(i,j, sudokuPlayer[i, j])))
                    {
                        wrongCells.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return wrongCells;

        }
        public bool CheckRow(int row,int value) 
        {
            int counter = 0;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (sudokuPlayer[row,i] == value)
                {
                    counter++;
                }
            }
            if (counter == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool CheckColumn(int column, int value)
        {
            int counter = 0;
            for (int i = 0; i < Sudoku_Size; i++)
            {
                if (sudokuPlayer[i, column] == value)
                {
                    counter++;
                }
            }
            if (counter == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool CheckRegion(int row,int column,int value) 
        {
            row /= sqrt;
            column /= sqrt;
            row *= sqrt;
            column *= sqrt;
            int counter = 0;
            for (int i = row; i < row + sqrt; i++)
            {
                for (int j = column; j < column + sqrt; j++)
                {
                    if (sudokuPlayer[i, j] == value)
                    {
                        counter++;
                    }
                }
                
            }
            if (counter == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
