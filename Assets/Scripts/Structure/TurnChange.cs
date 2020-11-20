public struct TurnChange
{
    public BoardCellId FromCellWithId { get; set; }
    public BoardCellId ToCellWithId { get; set; }
    public float TurnTimeSpawn { get; set; }

    public override string ToString()
    {
        return $"fromCellWithId: {FromCellWithId}, toCellWithId: {ToCellWithId}, TurnTimeSpawn: {TurnTimeSpawn}";
    }
}