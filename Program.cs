using System;

namespace sudoku
{     
    class Program
    {

        static void Main(string[] args)
        {
            Board gen1 = new Board();
            Board gen2 = new Board();
            int?[,] currentBoard = gen1.board;
            int?[,] nextBoard = gen2.board;

            bool check = false;
            bool checkOptions = false;
            bool checkPlaying = false;
            bool checkNewGame = false;
            bool confirmExit = false;
            bool checkGen = false;

            string input;
            int counter;

            Console.WriteLine("Welcome to Sudoku.\nPress any key to continue");
            Console.ReadKey();
            Console.WriteLine();

            do
            {

                do
                {
                    Console.WriteLine("Select a difficulty:\n[1]Easy [2]Medium [3]Hard");
                    check = false;
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": gen1.SetDifficulty(50); checkOptions = true; break;
                        case "2": gen1.SetDifficulty(60); checkOptions = true; break;
                        case "3": gen1.SetDifficulty(70); checkOptions = true; break;
                        default: Console.WriteLine("Enter a valid input"); checkOptions = false; break;
                    }

                } while (!checkOptions);



                do
                {
                    checkOptions = false;
                    checkPlaying = false;
                    counter = 0;
                    

                    do
                    {
                        gen1.PrintArray(currentBoard);
                        Console.WriteLine("[1]Insert Number [2]Generate New Board [3]Exit");
                        input = Console.ReadLine();

                        switch (input)
                        {
                            case "1": checkOptions = true; checkPlaying = true; break;
                            case "2": checkOptions = true; 
                                Console.WriteLine("Are You Sure?(y/n)");
                                if(ConfirmYesOrNo()) { checkGen = true; checkOptions = true; }
                                else { checkGen = false; checkOptions = true; }
                                break;
                            case "3": checkOptions = true; confirmExit = true;  break;
                            default: Console.WriteLine("Enter a valid input"); checkOptions = false; break;
                        }

                    } while (!checkOptions);


                    if (checkPlaying)
                    {

                        currentBoard = gen1.InsertNum();
                        

                        if (gen1.IsBoardFull(currentBoard))
                        {
                            for (int i = 0; i < gen1.sq; i++)
                            {
                                for (int j = 0; j < gen1.sq; j++)
                                {
                                    int temp = currentBoard[i, j].Value;
                                    if (gen1.DoesBoardUpholdRules(i, j, currentBoard[i, j].Value)) { counter++; }
                                    else { currentBoard[i, j] = temp; }
                                }
                            }

                            if (counter == gen1.sq * gen1.sq)
                            {
                                Console.WriteLine("YOU ARE WINNER!");
                                gen1.PrintArray(currentBoard);
                                check = true;
                                Console.WriteLine("Play Again?(y/n)");
                                if (!ConfirmYesOrNo()) { checkNewGame = true; }
                                else { checkGen = true; }
                            }

                            else
                                Console.WriteLine("Board isn't correct!\nPlease check again");
                        }


                    }

                    else if (confirmExit)
                    {
                        Console.WriteLine("Are You Sure?(y/n)");
                        if (ConfirmYesOrNo()) { check = true; checkNewGame = true; }
                    }

                    if (checkGen)
                    {
                        gen1 = gen2;
                        currentBoard = nextBoard;
                        gen2 = new Board();
                        nextBoard = gen2.board;
                        check = true;
                        checkGen = false;
                    }


                } while (!check);


            } while (!checkNewGame);
        }



        static private bool ConfirmYesOrNo()
        {
            bool check;
            string input;

            do
            {
                input = Console.ReadLine();
                if (input == "y" || input == "Y") { return true; }
                else if (input == "n" || input == "N") { return false; }
                else { Console.WriteLine("Enter a valid input"); check = false; }

            } while (!check);

            return false;
        }
    }
}

