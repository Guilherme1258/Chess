class Pawn: Pieces
{
    private bool firstMove = true;
    private bool ghost = false;
    private bool assistGhost = false;
    private readonly PieceColor color;

    public Pawn(PieceColor color)
    {
        this.color = color;
    }
    public Pawn(PieceColor color, int firstMove)
    {
        this.color = color;
        this.firstMove = firstMove == 1;
    }
    public Pawn(PieceColor color, int firstMove, int ghost)
    {
        this.color = color;
        this.firstMove = firstMove == 1;
        this.ghost = ghost == 1;
    }

    public override char GetPieceType()
    {
        return 'P';
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

    public void SetGhost()
    {
        ghost = !ghost;
    }

    public bool GetGhost()
    {
        return ghost;
    }

    public void SetAssistGhost()
    {
        assistGhost = !assistGhost;
    }
    public bool GetAssistGhost()
    {
        return assistGhost;
    }

    public override List<List<int>> PossibleMoves(int x, int y, bool playerOneTurn, Pieces[,] squares, bool loop = false)
    {
        List<List<int>> moves = [];

        if(playerOneTurn)
        {
            if(firstMove)
            {
                moves.Add([x-2,y]);
            }

            if((x-1) >= 0)
            {
                if((y-1) >= 0)
                {
                    if((!(squares[x-1,y-1] == null) && squares[x-1,y-1].GetPieceColor() == PieceColor.Black) || loop)
                    {
                        moves.Add([x-1, y-1]);
                    }
                    else if(!(squares[x,y-1] == null) && squares[x, y-1].GetPieceColor() == PieceColor.Black && squares[x, y-1] is Pawn pawn && pawn.GetGhost())
                    {
                        moves.Add([x-1, y-1]);
                    }
                }

                if((y+1) < 8)
                {
                    if((!(squares[x-1,y+1] == null) && squares[x-1,y+1].GetPieceColor() == PieceColor.Black) || loop)
                    {
                        moves.Add([x-1, y+1]);
                    }
                    else if(!(squares[x,y+1] == null) && squares[x,y+1].GetPieceColor() == PieceColor.Black && squares[x, y+1] is Pawn pawn && pawn.GetGhost() )
                    {
                        moves.Add([x-1, y+1]);
                    }
                }

                if(squares[x-1,y] == null && !loop)
                {
                    moves.Add([x-1,y]);
                }
            }
        }
        else 
        {
            if(firstMove)
            {
                moves.Add([x+2,y]);
            }

            if((x+1) < 8)
            {
                if((y-1) >= 0)
                {
                    if((!(squares[x+1, y-1] == null) && squares[x+1, y-1].GetPieceColor() == PieceColor.White) || loop)
                    {
                        moves.Add([x+1, y-1]);
                    }
                    if(!(squares[x, y-1] == null) && squares[x, y-1].GetPieceColor() == PieceColor.White && squares[x, y-1] is Pawn pawn && pawn.GetGhost())
                    {
                        moves.Add([x+1, y-1]);
                    }
                }

                if((y+1) < 8)
                {
                    if((!(squares[x+1, y+1] == null) && squares[x+1, y+1].GetPieceColor() == PieceColor.White) || loop)
                    {
                        moves.Add([x+1, y+1]);
                    }
                    if(!(squares[x, y+1] == null) && squares[x, y+1].GetPieceColor() == PieceColor.White && squares[x, y+1] is Pawn pawn && pawn.GetGhost())
                    {
                        moves.Add([x+1, y+1]);
                    }
                }

                if(squares[x+1,y] == null)
                {
                    moves.Add([x+1,y]);
                }
            }
        }

        return moves;
    }

}