class King : Pieces
{
    private readonly PieceColor color;
    private bool firstMove = true;

    public King(PieceColor color)
    {
        this.color = color;
    }

    public King(PieceColor color, int firstMove)
    {
        this.color = color;
        this.firstMove = firstMove == 1;
    }

    public override char GetPieceType()
    {
        return 'K';
    }
    public override PieceColor GetPieceColor()
    {
        return color;
    }
        public bool GetFirst()
    {
        return firstMove;
    }

    public void SetNotFirst()
    {
        firstMove = false;
    }

    public override List<List<int>> PossibleMoves(int x, int y, bool playerOneTurn, Pieces[,] squares, bool loop = false)
    {
        List<List<int>> moves = [];

        if(firstMove)
        {
            if(squares[0,0] is Rook rookB1 && (int)squares[0,0].GetPieceColor() != (playerOneTurn ? 1 : 0) && rookB1.GetFirst())
            {
                var roqueMove = rookB1.PossibleMoves(0, 0, playerOneTurn, squares);

                if(roqueMove.Any(move => move[0] == x && move[1] == (y - 2)) && roqueMove.Any(move => move[0] == x && move[1] == (y - 1)))
                {
                    moves.Add([x, y-2]);
                }
        
            }
            if(squares[0,7] is Rook rookB2 && (int)squares[0,7].GetPieceColor() != (playerOneTurn ? 1 : 0) && rookB2.GetFirst())
            {
                var roqueMove = rookB2.PossibleMoves(0, 7, playerOneTurn, squares);

                if(roqueMove.Any(move => move[0] == x && move[1] == (y + 2)) && roqueMove.Any(move => move[0] == x && move[1] == (y + 1)))
                {
                    moves.Add([x, y+2]);
                }
                
            }
            if(squares[7,0] is Rook rookW1 && (int)squares[7,0].GetPieceColor() != (playerOneTurn ? 1 : 0) && rookW1.GetFirst())
            {
                var roqueMove = rookW1.PossibleMoves(7, 0, playerOneTurn, squares);

                if(roqueMove.Any(move => move[0] == x && move[1] == (y - 2)) && roqueMove.Any(move => move[0] == x && move[1] == (y - 1)))
                {
                    moves.Add([x, y-2]);
                }
            }
            if(squares[7,7] is Rook rookW2 && (int)squares[7,7].GetPieceColor() != (playerOneTurn ? 1 : 0) && rookW2.GetFirst())
            {
                var roqueMove = rookW2.PossibleMoves(7, 7, playerOneTurn, squares);

                if(roqueMove.Any(move => move[0] == x && move[1] == (y + 2)) && roqueMove.Any(move => move[0] == x && move[1] == (y + 1)))
                {
                    moves.Add([x, y+2]);
                }
            }
        }


        if((x+1) < 8)
        {
            if((y-1) >= 0)
            {
                if(squares[x+1, y-1] == null || (int)squares[x+1,y-1].GetPieceColor() == (playerOneTurn ? 1 : 0))
                {
                    moves.Add([x+1, y-1]);
                }
            }
            if((y+1) < 8)
            {
                if(squares[x+1,y+1] == null || (int)squares[x+1,y+1].GetPieceColor() == (playerOneTurn ? 1 : 0))
                {
                    moves.Add([x+1,y+1]);
                }
            }
            if(squares[x+1,y] == null || (int)squares[x+1,y].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([x+1,y]);
            }
        }

        if((x-1) >= 0)
        {
            if((y-1) >= 0)
            {
                if(squares[x-1, y-1] == null || (int)squares[x-1, y-1].GetPieceColor() == (playerOneTurn ? 1 : 0))
                {
                    moves.Add([x-1, y-1]);
                }
            }
            if((y+1) < 8)
            {
                if(squares[x-1, y+1] == null || (int)squares[x-1, y+1].GetPieceColor() == (playerOneTurn ? 1 : 0))
                {
                    moves.Add([x-1, y+1]);
                }
            }
            if(squares[x-1, y] == null || (int)squares[x-1, y].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([x-1, y]);
            }
        }

        if((y-1) >= 0)
        {
            if(squares[x, y-1] == null || (int)squares[x, y-1].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([x, y-1]);
            }  
        }
        if((y+1) < 8)
        {
            if(squares[x, y+1] == null || (int)squares[x, y+1].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([x, y+1]);
            }  
        }


        if(!loop)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(squares[i,j] == null || (int)squares[i,j].GetPieceColor() != (playerOneTurn ? 1 : 0) )
                    {
                        continue;
                    }
                    else if((squares[i,j] is Pawn pawn || squares[i,j] is King king) && (int)squares[i,j].GetPieceColor() == (playerOneTurn ? 1 : 0))
                    {   
                        if(Math.Abs(y - j) <= 3 && Math.Abs(x - i) <= 2)
                        {
                            var piecesMoves = squares[i,j].PossibleMoves(i, j, !playerOneTurn, squares, true);
                            moves.RemoveAll(move => piecesMoves.Any(m => m[0] == move[0] && m[1] == move[1]));
                        }
                    }
                    else
                    {   
                        var piecesMoves = squares[i,j].PossibleMoves(i, j, !playerOneTurn, squares, true);
                        moves.RemoveAll(move => piecesMoves.Any(m => m[0] == move[0] && m[1] == move[1]));
                    }
                }
            }

            if(moves.Any(move => move[1] == y+2) || moves.Any(move => move[1] == y-2))
            {
                if(!moves.Any(move => move[1] == y+1))
                {
                    moves.RemoveAll(moves => moves[1] == y+2);
                }
                if(!moves.Any(move => move[1] == y-1))
                {
                    moves.RemoveAll(moves => moves[1] == y+2);
                }
            }
        }

        return moves;
    }
}