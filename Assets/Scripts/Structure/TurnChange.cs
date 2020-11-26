public struct TurnChange
{
    public BoardCellId FromCellWithId { get; set; }
    public BoardCellId ToCellWithId { get; set; }
    public float TurnTimeSpawn { get; set; }

    public TurnChange(BoardCellId fromCellWithId, BoardCellId toCellWithId, float turnTimeSpawn = 10f)
    {
        FromCellWithId = fromCellWithId;
        ToCellWithId = toCellWithId;
        TurnTimeSpawn = turnTimeSpawn;
    }

    public override string ToString()
    {
        return $"fromCellWithId: {FromCellWithId}, toCellWithId: {ToCellWithId}, TurnTimeSpawn: {TurnTimeSpawn}";
    }
}