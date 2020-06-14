using System;

namespace sudoku
{     
    class Program
    {

        static void Main(string[] args)
        {
            GenBoard gen1 = new GenBoard();
            GenBoard gen2 = new GenBoard();
            int?[,] currentBoard = gen1.board;
            int?[,] nextBoard = gen2.board;

            bool check = false;
            bool checkOptions = false;
            bool checkPlaying = false;
            bool checkValid = false;
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
                    Console.WriteLine("Select a diffculty:\n[1]Easy [2]Medium [3]Hard");
                    check = false;
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1": gen1.diffculty(50); checkOptions = true; break;
                        case "2": gen1.diffculty(60); checkOptions = true; break;
                        case "3": gen1.diffculty(70); checkOptions = true; break;
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
                                if(nodInput()) { checkGen = true; checkOptions = true; }
                                else { checkGen = false; checkOptions = true; }
                                break;
                            case "3": checkOptions = true; confirmExit = true;  break;
                            default: Console.WriteLine("Enter a valid input"); checkOptions = false; break;
                        }
                    } while (!checkOptions);


                    if (checkPlaying)
                    {

                        while (!checkValid)
                        {
                            Console.WriteLine("Enter a row:");
                            input = ReadInput();
                            gen1.row = Convert.ToInt32(input);

                            Console.WriteLine("Enter a column:");
                            input = ReadInput();
                            gen1.col = Convert.ToInt32(input);

                            if (gen1.CheckIfMoveIsValid(gen1.row - 1, gen1.col - 1)) { checkValid = true; }
                        }
                        checkValid = false;



                        Console.WriteLine("Enter a number:");
                        input = ReadInput();
                        gen1.num = Convert.ToInt32(input);

                        currentBoard[gen1.row - 1, gen1.col - 1] = gen1.num;



                        if (gen1.CheckIfFull(currentBoard))
                        {
                            for (int i = 0; i < gen1.sq; i++)
                            {
                                for (int j = 0; j < gen1.sq; j++)
                                {
                                    int temp = currentBoard[i, j].Value;
                                    if (gen1.RuleCheck(currentBoard, i, j, currentBoard[i, j].Value)) { counter++; }
                                    else { currentBoard[i, j] = temp; }
                                }
                            }

                            if (counter == gen1.sq * gen1.sq)
                            {
                                Console.WriteLine("YOU ARE WINNER!");
                                gen1.PrintArray(currentBoard);
                                check = true;
                                Console.WriteLine("Play Again?(y/n)");
                                if (!nodInput()) { checkNewGame = true; }
                                else { checkGen = true; }
                            }

                            else
                            {
                                Console.WriteLine("Board isn't correct!\nPlease check again");
                            }
                        }


                    }

                    else if (confirmExit)
                    {
                        Console.WriteLine("Are You Sure?(y/n)");
                        if (nodInput()) { check = true; checkNewGame = true; }
                    }

                    if (checkGen)
                    {
                        gen1 = gen2;
                        currentBoard = nextBoard;
                        gen2 = new GenBoard();
                        nextBoard = gen2.board;
                        check = true;
                        checkGen = false;
                    }


                } while (!check);


            } while (!checkNewGame);
        }



        static private bool nodInput()
        {
            bool check;
            string input;

            do
            {
                input = Console.ReadLine();
                if (input == "y" || input == "Y") { check = true; return true; }
                else if (input == "n" || input == "N") { check = true; return false; }
                else { Console.WriteLine("Enter a valid input"); check = false; }

            } while (!check);
            return false;
        }



        static private string ReadInput()
        {
            bool move = false;
            string input;

            do
            {
                input = Console.ReadLine();
                move = CheckInput(input);
            } while (!move);

            return input;
        }
        


        static private bool CheckInput(string value)
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
    }
}

