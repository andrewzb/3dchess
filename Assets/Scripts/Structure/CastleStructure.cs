public struct CastleStructure
{
    public BoardCellId KingFrom { get; private set; }
    public BoardCellId KingTo { get; private set; }
    public BoardCellId RookFrom { get; private set; }
    public BoardCellId RookTo { get; private set; }

    public void Init(BoardCellId kingFrom, BoardCellId kingTo, BoardCellId rookFrom, BoardCellId rookTo)
    {
        KingFrom = kingFrom;
        KingTo = kingTo;
        RookFrom = rookFrom;
        RookTo = rookTo;
    }
}
