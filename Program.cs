using System;
using System.ComponentModel;
using System.Security.Cryptography;

namespace sudoku
{
    class CheckBoard
    {
        public static int?[,] board;
        public static int sq;
        public static void PrintArray(int?[,] matrix, int size)
        {
            Console.Write("\t");
            for (int i = 0; i < size; i++)
            {
                Console.Write(i + 1 + "\t");
            }
            Console.Write("\n\n");
            for (int j = 0; j < size; j++)
            {
                Console.Write(j + 1 + " \t");
                for (int k = 0; k < size; k++)
                {
                    Console.Write("|" + matrix[j, k] + "|\t");
                }
                Console.Write("\n\n");
            }
        }
        public static bool RuleCheck(int?[,] matrix, int row, int col, int num, int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (matrix[row, i] == num) { return false; }
            }
            for (int j = 0; j < size; j++)
            {
                if (matrix[j, col] == num) { return false; }
            }

            if (row < 3)
            {
                if (!ColumnCheck(matrix, col, num, 0)) { return false; }
            }
            else if (row >= 3 && row < 6)
            {
                if (!ColumnCheck(matrix, col, num, 3)) { return false; }
            }
            else
            {
                if (!ColumnCheck(matrix, col, num, 6)) { return false; }
            }
            return true;
        }
        private static bool ColumnCheck(int?[,] matrix, int col, int num, int addRow)
        {
            if (col < 3)
            {
                if (!SquareCheck(matrix, num, addRow, 0)) { return false; }
            }
            else if (col >= 3 && col < 6)
            {
                if (!SquareCheck(matrix, num, addRow, 3)) { return false; }
            }
            else
            {
                if (!SquareCheck(matrix, num, addRow, 6)) { return false; }
            }
            return true;
        }
        private static bool SquareCheck(int?[,] matrix, int num, int addRow, int addCol)
        {
            for (int i = 0 + addRow; i < 3 + addRow; i++)
            {
                for (int j = 0 + addCol; j < 3 + addCol; j++)
                {
                    if (matrix[i,j] == num) { return false; }
                }
            }
            return true;
        }
    }
    class GenBoard : CheckBoard
    {

        private static int counter;
        private static bool pass;
        static GenBoard()
        {
            sq = 9;
            counter = 10; //amount of numbers allocated to the board
            board = new int?[sq, sq];
            Random rand = new Random();
            for (int i = 0; i < counter; i++)
            {
                pass = false;
                int num = rand.Next(1, 10);
                while(!pass)
                {
                    int row = rand.Next(0, sq);
                    int col = rand.Next(0, sq);
                    bool check = RuleCheck(board, row, col, num, sq);
                    if (board[row, col] == null && check == true)
                    {
                        board[row, col] = num;
                        pass = true;
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            new GenBoard();
            GenBoard.PrintArray(GenBoard.board, GenBoard.sq);
            int?[,] sudokuBoard = GenBoard.board;
        }
    }
}
