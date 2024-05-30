class Queen : Pieces
{
    private readonly PieceColor color;

    public Queen(PieceColor color)
    {
        this.color = color;
    }

    public override char GetPieceType()
    {
        return 'Q';
    }
    public override PieceColor GetPieceColor()
    {
        return color;
    }

    public override List<List<int>> PossibleMoves(int x, int y, bool playerOneTurn, Pieces[,] squares, bool loop = false)
    {
        List<List<int>> moves = [];

        for(int i = y+1; i < 8; i++)
        {
            if(squares[x,i] == null)
            {
                moves.Add([x,i]);
            }
            else if((int)squares[x,i].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([x,i]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else 
            {
                break;
            }
        }
        for(int i = y-1; i >= 0; i--)
        {
            if(squares[x,i] == null)
            {
                moves.Add([x,i]);
            }
            else if((int)squares[x,i].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([x,i]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        for(int i = x-1; i >= 0; i--)
        {
            if(squares[i,y] == null)
            {
                moves.Add([i,y]);
            }
            else if((int)squares[i,y].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([i,y]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        for(int i = x+1; i < 8; i++)
        {
            if(squares[i,y] == null)
            {
                moves.Add([i,y]);
            }
            else if((int)squares[i,y].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([i,y]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        for(int i = x+1, j = y+1; i < 8 && j < 8; i++, j++)
        {
            if(squares[i,j] == null)
            {
                moves.Add([i,j]);
            }
            else if((int)squares[i,j].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([i,j]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else 
            {
                break;
            }
        }
        for(int i = x-1, j = y+1; i >= 0 && j < 8; i--, j++)
        {
            if(squares[i,j] == null)
            {
                moves.Add([i,j]);
            }
            else if((int)squares[i,j].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([i,j]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else 
            {
                break;
            }
        }
        for(int i = x-1, j = y-1; i >= 0 && j >= 0; i--, j--)
        {
            if(squares[i,j] == null)
            {
                moves.Add([i,j]);
            }
            else if((int)squares[i,j].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([i,j]);

                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else 
            {
                break;
            }
        }
        for(int i = x+1, j = y-1; i < 8 && j >= 0; i++, j--)
        {
            if(squares[i,j] == null)
            {
                moves.Add([i,j]);
            }
            else if((int)squares[i,j].GetPieceColor() == (playerOneTurn ? 1 : 0))
            {
                moves.Add([i,j]);
                
                if(!loop || squares[x,i] is not King)
                {
                    break;
                }
            }
            else 
            {
                break;
            }
        }
        
        return moves;
    } 
    
}