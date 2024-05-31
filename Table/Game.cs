using System;

class Game
{
    private Board board;

    public Game()
    {
        board = new Board();
    }
    
    public void StartGame()
    {
        bool playervsPlayer = Settings.PlayerChoosePlayers();
        
        if(playervsPlayer) 
        {
            PlayerVsPlayes();
        }
        else
        {
            // PlayerVsComputer();
        }
    }

    private void PlayerVsPlayes()
    {
        bool gameOver = false;
        bool exit = false;
        bool playerOneTurn = true;
        bool keyWord;
        string? piece;
        string? movePosition;
        List<List<int>> moves = [];


        Console.WriteLine("Player 1 is blue and Player 2 is red");
        ShowMenu();

        while(!gameOver && !exit)
        {
            board.PrintBoard();
            keyWord = false;
            piece = null;

            if(playerOneTurn)
            {
                Console.WriteLine("\nPlayer 1's turn");
            }
            else
            {
                Console.WriteLine("\nPlayer 2's turn");
            }

            try
            {
                do
                {
                    Console.Write("Enter the piece you want to move: ");
                    piece = Console.ReadLine()?.ToUpper();
                    if(piece == null)
                    {
                        continue;
                    }
                    else if(piece == "EXIT" || piece == "RESTART" || piece == "SAVE" || piece == "LOAD" || piece == "HELP" || piece == "DELETE")
                    {
                        keyWord = true;
                        break;
                    }
                    else if(!board.IsValidPiece(piece, playerOneTurn))
                    {
                        board.PrintBoard();
                        Console.WriteLine("\nInvalid input. Please enter a letter beteween A and G, and a number betewwen 1 and 8\n");
                        continue;
                    }
                    else
                    {
                        moves = board.PrintBoardMove(piece!, playerOneTurn);

                        if(moves.Count == 0)
                        {
                            Console.WriteLine("\nInvalid input. This piece cannot move.\n");
                            continue;
                        }

                        break;
                    }

                }while(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            if(!keyWord)
            {
                try
                {
                    do 
                    {
                        Console.Write("\nEnter the position you want to move to, or type 'retry' to move another piece: ");
                        movePosition = Console.ReadLine()?.ToUpper();

                        if(movePosition == null || piece == null)
                        {
                            continue;
                        }
                        else if(movePosition == "EXIT" || movePosition == "RESTART" || movePosition == "SAVE" || movePosition == "LOAD" || movePosition == "HELP" || movePosition == "DELETE" || movePosition == "RETRY")
                        {
                            piece = movePosition;
                            break;
                        }
                        else if(!Board.IsValidMoviment(movePosition, moves))
                        {
                            Console.WriteLine("This piece can't be moved to this position\n");
                            continue;
                        }
                        else
                        {
                            board.Move(piece, movePosition, ref gameOver);
                            playerOneTurn = !playerOneTurn;
                            break;
                        }

                    }while(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            if(piece == "EXIT")
            {
                exit = true;
            }
            else if(piece == "RESTART")
            {
                board = new Board();
            }
            else if(piece == "DELETE")
            {
                Settings.DeleteSave();
            }
            else if(piece == "SAVE")
            {
                Settings.Save(board, playerOneTurn);
            }
            else if(piece == "LOAD")
            {
                Settings.Load(ref board, ref playerOneTurn);
            }
            else if(piece == "HELP")
            {
                ShowMenu();
            }

            if(piece!.Length == 2)
            {
                board.VerifyGame(playerOneTurn, ref gameOver);
            }
        }


        if(gameOver)
        {
            board.PrintBoard();
            MsgWinner(playerOneTurn);
        }
    }

    // private void PlayerVsComputer()
    // {
        // bool gameOver = false;
        // bool playerOneTurn = true;

        // while(!gameOver)
        // {
        //     Console.WriteLine("Enter the piece you want to move: ");
        //     string piece = Console.ReadLine();
        //     Console.WriteLine("Enter the position you want to move to: ");
        //     string position = Console.ReadLine();

        //     if(piece == "exit")
        //     {
        //         gameOver = true;
        //     }
        //     else if(piece == "restart")
        //     {
        //         board = new Board();
        //         board.PrintBoard();
        //     }
        //     else if(piece == "save")
        //     {
        //         // Save game
        //     }
        //     else if(piece == "load")
        //     {
        //         // Load game
        //     }

        //     // board.MovePiece(piece, position);
        //     board.PrintBoard();
        // }
    // }

    static void ShowMenu() 
    {
        Console.WriteLine("\nMenu:");
        Console.WriteLine("To play, just choose the coordinates of the piece you want to move and the square it will go to, for example G2 to E2 (if the move is allowed).");
        Console.WriteLine("At any time you can type the keyword to below");
        Console.WriteLine("\nType 'restart' to restart the game");
        Console.WriteLine("Type 'delete' to delete a save");
        Console.WriteLine("Type 'save' to save the game");
        Console.WriteLine("Type 'load' to load a game");
        Console.WriteLine("Type 'exit' to exit the game");
        Console.WriteLine("Type 'help' to show the menu again\n");
    }

    static void MsgWinner(bool playerOneTurn)
    {
        if(playerOneTurn)
        {
            Console.WriteLine("\n\nPlayer 2 Won!!!");
            Console.WriteLine("Type any key to return");
            Console.Read();
        }
        else
        {
            Console.WriteLine("\n\nPlayer 1 Won!!!");
            Console.WriteLine("Type any key to return");
            Console.Read();
        }
    }
}
