using System;

namespace sudoku
{
    class CheckBoard
    {

        public int?[,] board;
        public int?[,] checkboard;
        public int sq;
        public int num;
        public int row;
        public int col;



        public void PrintArray (int?[,] matrix)
        {
            Console.Write("\t");
            for (int i = 0; i < sq; i++)
            {
                if (i == 2 || i == 5)
                    Console.Write($" {i + 1}\t\t");
                else
                    Console.Write($" {i + 1}\t");
            }
            Console.Write("\n\n");
            for (int j = 0; j < sq; j++)
            {
                Console.Write(j + 1 + "\t");
                for (int k = 0; k < sq; k++)
                {
                    if (checkboard[j, k] == null)
                        Console.Write("|" + matrix[j, k]);
                    else
                        Console.Write("|" + matrix[j, k] + "*");

                    if (k == 2 || k == 5)
                        Console.Write("|\t|\t");
                    else
                        Console.Write("|\t");
                }
                Console.Write("\n\n");
                if (j == 2 || j == 5)
                    Console.Write("\t _\t _\t _\t_\t _\t _\t _\t_\t _\t _\t _\n\n");
            }
        }



        public bool CheckIfMoveIsValid (int row, int col)
        {
            if (checkboard[row, col] == null)
            {
                Console.WriteLine("You can only place numbers on empty blocks or replace numbers you set.");
                return false;
            }
            return true;
        }



        public bool CheckIfFull (int?[,] matrix)
        {
            int count = 0;
            for (int i = 0; i < sq; i++)
            {
                for (int j = 0; j < sq; j++)
                {
                    if (matrix[i, j] != null) { count++; }
                }
            }
            if (count == sq*sq) { return true; }
            else { return false; }
        }



        public bool RuleCheck(int?[,] matrix, int row, int col, int num)
        {
            matrix[row, col] = null;
            for (int i = 0; i < sq; i++)
            {
                if (matrix[row, i] == num) { return false; }
            }

            for (int j = 0; j < sq; j++)
            {
                if (matrix[j, col] == num) { return false; }
            }

            for (int k = 0; k < sq; k+=3)
            {
                if (row >= k && row < 3 + k)
                {
                    if (!ColumnCheck(matrix, col, num, k)) { return false; }
                }
            }
            matrix[row, col] = num;
            return true;
        }



        private bool ColumnCheck(int?[,] matrix, int col, int num, int addRow)
        {
            for (int i = 0; i < sq; i+=3)
            {
                if (col >= i && col < 3 + i)
                {
                    if (!SquareCheck(matrix, num, addRow, i)) { return false; }
                }
            }
            return true;
        }



        private bool SquareCheck(int?[,] matrix, int num, int addRow, int addCol)
        {
           for (int i = addRow; i < 3 + addRow; i++)
           {
                for (int j = addCol; j < 3 + addCol; j++)
                {
                    if (matrix[i, j] == num) { return false; }
                }
           }
           return true;
        }
       
    }
}