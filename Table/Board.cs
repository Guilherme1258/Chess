using System;
class Board
{
    public Pieces[,] squares = new Pieces[8,8];

    public Board()
    {
        for(int i = 0; i < 8; i++) 
        {
            squares[1,i] = new Pawn(PieceColor.Black);
            squares[6,i] = new Pawn(PieceColor.White);
        }
        squares[0,0] = new Rook(PieceColor.Black);
        squares[0,7] = new Rook(PieceColor.Black);
        squares[0,1] = new Knight(PieceColor.Black);
        squares[0,6] = new Knight(PieceColor.Black);
        squares[0,2] = new Bishop(PieceColor.Black);
        squares[0,5] = new Bishop(PieceColor.Black);
        squares[0,3] = new Queen(PieceColor.Black);
        squares[0,4] = new King(PieceColor.Black);

        squares[7,0] = new Rook(PieceColor.White);
        squares[7,7] = new Rook(PieceColor.White);
        squares[7,1] = new Knight(PieceColor.White);
        squares[7,6] = new Knight(PieceColor.White);
        squares[7,2] = new Bishop(PieceColor.White);
        squares[7,5] = new Bishop(PieceColor.White);
        squares[7,3] = new Queen(PieceColor.White);
        squares[7,4] = new King(PieceColor.White);
    }

    public Board(int x) {}

    public static Board SetBoard(string board)
    {
        Board newBoard = new(1);
        string[] pieces = board.Split("\n");
        int first = 0;
        int ghost = 0;

        foreach(string piece in pieces)
        {
            if (piece == "" || piece.Length < 4)
            {
                continue;
            }
            string[] data = piece.Split(" ");
            string pieceType = data[0];
            int x = int.Parse(data[1]);
            int y = int.Parse(data[2]);
            int color = int.Parse(data[3]);

            if(pieceType == "P")
            {
                first = int.Parse(data[4]);
                ghost = int.Parse(data[5]);
                newBoard.squares[x,y] = new Pawn((PieceColor)color, first, ghost);
            }
            else if(pieceType == "R")
            {   
                first = int.Parse(data[4]);
                newBoard.squares[x,y] = new Rook((PieceColor)color, first);
            }
            else if(pieceType == "H")
            {
                newBoard.squares[x,y] = new Knight((PieceColor)color);
            }
            else if(pieceType == "B")
            {
                newBoard.squares[x,y] = new Bishop((PieceColor)color);
            }
            else if(pieceType == "Q")
            {
                newBoard.squares[x,y] = new Queen((PieceColor)color);
            }
            else if(pieceType == "K")
            {   
                first = int.Parse(data[4]);
                newBoard.squares[x,y] = new King((PieceColor)color, first);
            }
        }
        return newBoard;
    }

    public void PrintBoard()
    {
        Console.WriteLine();
        Console.Write(' ');
        for(int i = 0; i < 8; i++)
        {
            Console.Write(" " + (i+1) + " ");
        }

        for(int i = 0; i < 8; i++)
        {
            Console.WriteLine();
            Console.Write((char)(65 + i));
            for(int j = 0; j < 8; j++)
            {
                if(squares[i,j] != null)
                {
                    if(squares[i,j].GetPieceColor() == PieceColor.Black)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + squares[i,j].GetPieceType() + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" " + squares[i,j].GetPieceType() + " ");
                        Console.ResetColor();
                    }   
                }
                else
                {
                    Console.Write(" - ");
                }
            }
        }
        Console.WriteLine();
    }  

    public List<List<int>> PrintBoardMove(string piece, bool playerOneTurn)
    {
        int xP = piece[0] - 65;
        int yP = (int)char.GetNumericValue(piece[1]) - 1;
        var moves = squares[xP, yP].PossibleMoves(xP, yP, playerOneTurn, squares);

        Console.WriteLine();
        Console.Write(' ');
        for(int i = 0; i < 8; i++)
        {
            Console.Write(" " + (i+1) + " ");
        }

        for(int i = 0; i < 8; i++)
        {
            Console.WriteLine();
            Console.Write((char)(65 + i));
            for(int j = 0; j < 8; j++)
            {
                if(squares[i,j] != null)
                {
                    if(squares[i,j].GetPieceColor() == PieceColor.Black)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + squares[i,j].GetPieceType() + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" " + squares[i,j].GetPieceType() + " ");
                        Console.ResetColor();
                    }   
                }
                else if(moves.Any(move => move[0] == i && move[1] == j))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" o ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" - ");
                }
            }
        }
        Console.WriteLine();

        return moves;
    }  

    public string GetBoard()
    {
        string board = "";
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(squares[i,j] != null)
                {
                    if(squares[i,j] is Pawn pawn)
                    {
                        board += squares[i,j].GetPieceType() + " " + i + " " + j + " " + (int)squares[i,j].GetPieceColor() + " " + (pawn.GetFirst() ? 1:0) + " " + (pawn.GetGhost() ? 1:0) +"\n";
                    }
                    else if(squares[i,j] is Rook rook)
                    {
                        board += squares[i,j].GetPieceType() + " " + i + " " + j + " " + (int)squares[i,j].GetPieceColor() + " " + (rook.GetFirst() ? 1:0) + "\n";
                    }
                    else if(squares[i,j] is King king)
                    {
                        board += squares[i,j].GetPieceType() + " " + i + " " + j + " " + (int)squares[i,j].GetPieceColor() + " " + (king.GetFirst() ? 1:0) + "\n";
                    }
                    else
                    {
                        board += squares[i,j].GetPieceType() + " " + i + " " + j + " " + (int)squares[i,j].GetPieceColor() + "\n";
                    }
                }
            }
        }
        return board;
    }  

    public bool IsValidPiece(string piece, bool playerOneTurn)
    {
        if(piece.Length != 2)
        {
            return false;
        }
        
        int x = (int)piece[0] - 65;
        int y = (int)char.GetNumericValue(piece[1]) - 1;

        if(x > 7 || y > 7 || x < 0 || y < 0)
        {
            return false;
        }
        else if(squares[x,y] == null)
        {
            return false;
        }
        else if((int)squares[x,y].GetPieceColor() == (playerOneTurn ? 1 : 0))
        {
            return false;
        }
       return true;
    }

    public static bool IsValidMoviment(string move, List<List<int>> allowedMoves) 
    {
        if(move.Length != 2)
        {
            return false;
        }

        int xM = move[0] - 65;
        int yM = (int)char.GetNumericValue(move[1]) - 1;

        return allowedMoves.Any(m => m[0] == xM && m[1] == yM);
    }

    public void Move(string piece, string move, ref bool gameOver)
    {
        int xP = piece[0] - 65;
        int yP = (int)char.GetNumericValue(piece[1]) - 1;
        int xM = move[0] - 65;
        int yM = (int)char.GetNumericValue(move[1]) - 1;   

        if(squares[xM, yM] is King)
        {
            gameOver = true;
        }

        squares[xM, yM] = squares[xP,yP];
        squares[xP,yP] = null!;

        if(squares[xM, yM] is Pawn pawn)
        {
            MovePawn(ref pawn, xP, yP, xM, yM);
        }
        else if(squares[xM, yM] is Rook rook)
        {
            MoveRook(ref rook);
        }
        else if(squares[xM, yM] is King king)
        {
            MoveKing(ref king, xP, yP, xM, yM);
        }

        return;        
    }

    private void MovePawn(ref Pawn pawn, int xP, int yP, int xM, int yM)
    {
        if(pawn.GetFirst())
        {
            if(Math.Abs(xP - xM) == 2)
            {
                pawn.SetAssistGhost();
            }
            pawn.SetNotFirst();
        } 
        if(Math.Abs(yP - yM) != 0)
        {
            if(pawn.GetPieceColor() == PieceColor.White)
            {
                if(squares[xM + 1, yM] is Pawn pawn1 && pawn1.GetPieceColor() == PieceColor.Black && pawn1.GetGhost())
                {
                    squares[xM + 1, yM] = null!;
                }
            }
            else if(pawn.GetPieceColor() == PieceColor.Black)
            {
                if(squares[xM - 1, yM] is Pawn pawn1 && pawn1.GetPieceColor() == PieceColor.White && pawn1.GetGhost())
                {
                    squares[xM - 1, yM] = null!;
                }
            }
        }
        if(xM == 7 || xM == 0)
        {
            squares[xM, yM] = Promote(squares[xM, yM].GetPieceColor());
        }
    }

    private void MoveRook(ref Rook rook)
    {
        if(rook.GetFirst())
        {
            rook.SetNotFirst();
        }
    }

    private void MoveKing(ref King king, int xP, int yP, int xM, int yM)
    {
        if(king.GetFirst())
        {
            king.SetNotFirst();
        }
        if(yP - yM == 2)
        {
            if(king.GetPieceColor() == PieceColor.White)
            {
                squares[xP, yP - 1] = squares[7,0];
                squares[7,0] = null!;
            }
            if(king.GetPieceColor() == PieceColor.Black)
            {
                squares[xP, yP - 1] = squares[0,0];
                squares[0,0] = null!;
            }
        }
        if(yP - yM == -2)
        {
            if(king.GetPieceColor() == PieceColor.Black)
            {
                squares[xP, yP + 1] = squares[0,7];
                squares[0,7] = null!;
            }
            if(king.GetPieceColor() == PieceColor.White)
            {
                squares[xP, yP + 1] = squares[7,7];
                squares[7,7] = null!;
            }
        }
    }

    Pieces Promote(PieceColor color)
    {
        int? choose = 0;
        Console.WriteLine("\nChoose which piece your pawn will be promoted to:");
        
        try
        {
            do
            {
                Console.WriteLine("1.Rook\n2.Knight\n3.Bishop\n4.Queen");
                string? input = Console.ReadLine();

                if(input != "")
                {
                    choose = (int)char.GetNumericValue(input![0]);
                }
                if(choose >= 1 && choose <=4)
                {
                    break;
                }
      
                Console.WriteLine("\nType a number between 1 and 4!\n\n");
                
            }while(true);
        }
        catch (Exception e)
        {
            Console.Write(e);
        }

        return choose switch
        {
            1 => new Rook(color, 0),
            2 => new Knight(color),
            3 => new Bishop(color),
            4 => new Queen(color),
            _ => null!,
        };
    }

    public void VerifyGame(bool playerOneTurn, ref bool gameOver)
    {
        List<List<int>> moves = new();
        List<List<int>> movK = new();
        int xK = 0;
        int yK = 0;

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(squares[i,j] != null && (int)squares[i,j].GetPieceColor() == (playerOneTurn ? 1 : 0))
                {
                    moves.AddRange(squares[i,j].PossibleMoves(i, j, !playerOneTurn, squares));
                }
                else if(squares[i,j] is King king && (int)king.GetPieceColor() != (playerOneTurn ? 1 : 0))
                {
                    xK = i;
                    yK = j;
                    movK = king.PossibleMoves(i, j, playerOneTurn, squares);
                }

                if(squares[i,j] is Pawn pawn) 
                {
                    if(pawn.GetAssistGhost() == true && pawn.GetGhost() == false)
                    {
                        pawn.SetGhost();
                        pawn.SetAssistGhost();
                    }
                    else if(pawn.GetGhost() == true)
                    {
                        pawn.SetGhost();
                    }
                }
            }
        }
        
        if(moves.Any(m => m[0] == xK && m[1] == yK))
        {
            gameOver = movK.Count == 0;
            return;
        }
    }
}
