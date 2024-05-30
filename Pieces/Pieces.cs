public enum PieceColor
{
    White,
    Black
}


class Pieces
{
    public virtual char GetPieceType()
    {
        return ' ';
    }
    public virtual PieceColor GetPieceColor()
    {
        return PieceColor.White;
    }
    public virtual List<List<int>> PossibleMoves(int x, int y, bool playerOneTurn, Pieces[,] squares, bool loop = false) 
    {
        throw new NotImplementedException("Error, use of unimplemented virtual function!");
    }

}