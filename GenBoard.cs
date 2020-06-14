using System;

namespace sudoku
{
    class GenBoard : CheckBoard
    {

        private static int err_counter;
        private static bool pass;
        private static bool redo;



        public GenBoard()
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
                            if (RuleCheck(board, row, col, num))
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

        

        public void diffculty (int counter)
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
    }
}
