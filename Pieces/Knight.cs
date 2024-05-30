class Knight : Pieces
{
    private readonly PieceColor color;

    public Knight(PieceColor color)
    {
        this.color = color;
    }

    public override char GetPieceType()
    {
        return 'H';
    }
    public override PieceColor GetPieceColor()
    {
        return color;
    }

     public override List<List<int>> PossibleMoves(int x, int y, bool playerOneTurn, Pieces[,] squares, bool loop = false)
    {
        List<List<int>> moves = [];
        
        //up and right
        if(x-2 >= 0 && y+1 < 8)
        {
            moves.Add([x-2, y+1]);
        }
        //up and left
        if(x-2 >= 0 && y-1 >= 0)
        {
            moves.Add([x-2, y-1]);
        }
        //down and left
        if(x+2 < 8 && y-1 >= 0)
        {
            moves.Add([x+2, y-1]);
        }
        // down and right
        if(x+2 < 8 && y+1 < 8)
        {
            moves.Add([x+2, y+1]);
        }
        // left and up
        if(x-1 >= 0 && y-2 >= 0)
        {
            moves.Add([x-1, y-2]);
        }
        // left and down
        if(x+1 < 8 && y-2 >= 0)
        {
            moves.Add([x+1, y-2]);
        }
        // right and up
        if(x-1 >= 0 && y+2 < 8)
        {
            moves.Add([x-1, y+2]);
        }
        // rigth and down
        if(x+1 < 8 && y+2 < 8)
        {
            moves.Add([x+1, y+2]);
        }
        
        return moves;
    } 
    
}