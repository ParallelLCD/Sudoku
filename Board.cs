using System;

namespace sudoku
{
    class Board
    {

        public int?[,] board;
        public int?[,] checkboard;
        public int sq;
        public int num;
        public int row;
        public int col;

        private static int err_counter;
        private static bool pass;
        private static bool redo;



        public Board()
        {
            sq = 9;
            err_counter = 0;
            redo = false;
            board = new int?[sq, sq];
            checkboard = new int?[sq, sq];
            Random rand = new Random();
            num = 0;

            for (int i = 1; i <= sq; i++)
            {
                num++;
                for (int j = 0; j < sq; j++)
                {
                    pass = false;
                    while (!pass)
                    {
                        row = rand.Next(0, sq);
                        col = rand.Next(0, sq);
                        if (board[row, col] == null)
                        {
                            if (DoesBoardUpholdRules(row, col, num))
                            {
                                board[row, col] = num;
                                pass = true;
                            }
                        }
                        else if (err_counter == 1000)
                        {
                            err_counter = 0;
                            j = 0;
                            redo = true;
                            pass = true;
                        }
                        else { err_counter++; }
                    }
                    if (redo)
                    {
                        board = new int?[sq, sq];
                        num = 0;
                        j = sq;
                        i = 0;
                        redo = false;
                    }
                }
            }
        }



        public void SetDifficulty(int counter)
        {
            Random rand = new Random();
            for (int k = 0; k < counter; k++)
            {
                pass = false;
                while (!pass)
                {
                    if (board[row, col] == null)
                    {
                        row = rand.Next(0, sq);
                        col = rand.Next(0, sq);
                    }
                    else
                    {
                        checkboard[row, col] = board[row, col];
                        board[row, col] = null;
                        row = rand.Next(0, sq);
                        col = rand.Next(0, sq);
                        pass = true;
                    }
                }
            }
        }



        public void PrintArray(int?[,] matrix)
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




        public int?[,] InsertNum()
        {
            bool checkValid = false;
            string input;

            do
            {
                Console.WriteLine("Enter a row:");
                input = ReadInput();
                row = Convert.ToInt32(input);

                Console.WriteLine("Enter a column:");
                input = ReadInput();
                col = Convert.ToInt32(input);

                if (IsMoveValid(row - 1, col - 1)) { checkValid = true; }
            } while (!checkValid);


            Console.WriteLine("Enter a number:");
            input = ReadInput();
            num = Convert.ToInt32(input);

            board[row - 1, col - 1] = num;
            return board;
        }



        static private string ReadInput()
        {
            bool move = false;
            string input;

            do
            {
                input = Console.ReadLine();
                move = IsInputValid(input);
            } while (!move);

            return input;
        }



        static private bool IsInputValid(string value)
        {
            int input;

            if (!int.TryParse(value, out input))
            {
                Console.WriteLine("Input is not a number! (1-9):");
                return false;
            }
            if (input > 9)
            {
                Console.WriteLine("Enter a valid number! (1-9):");
                return false;
            }

            return true;
        }




        public bool IsMoveValid(int row, int col)
        {
            if (checkboard[row, col] == null)
            {
                Console.WriteLine("You can only place numbers on empty blocks or replace numbers you set.");
                return false;
            }
            return true;
        }



        public bool IsBoardFull(int?[,] matrix)
        {
            int count = 0;
            for (int i = 0; i < sq; i++)
                for (int j = 0; j < sq; j++)
                    if (matrix[i, j] != null) { count++; }


            if (count == sq*sq) { return true; }
            else { return false; }
        }



        public bool DoesBoardUpholdRules(int row, int col, int num)
        {
            board[row, col] = null;
            
            if(IsRowContainValue(row, num))
                if(IsColContainValue(col, num))
                    if(IsSqareContainValue(row, col, num)) { board[row, col] = num; return true; }

            return false;
        }



        private bool IsRowContainValue(int row, int num)
        {
            for (int i = 0; i < sq; i++)
                if (board[row, i] == num) { return false; }
            return true;
        }



        private bool IsColContainValue(int col, int num)
        {
            for (int i = 0; i < sq; i++)
                if (board[i, col] == num) { return false; }
            return true;
        }



        private bool IsSqareContainValue(int row, int col, int num)
        {

            int addRow = GrabValue(row);
            int addCol = GrabValue(col);

            for (int i = addRow; i < 3 + addRow; i++)
                for (int j = addCol; j < 3 + addCol; j++)
                    if (board[i, j] == num) { return false; }

            return true;
        }



        private int GrabValue(int value)
        {
            for (int i = 0; i < sq; i += 3)
                if (value >= i && value < 3 + i)
                    return i;
            return 0;
        }
    }
}