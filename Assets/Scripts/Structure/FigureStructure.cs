public struct FigureStructure
{
    public BoardFirureId FirureId { get; }
    public FigureType InitialType { get; }
    public FigureType CurrentType { get; }
    public FigureTeamType TeamType { get; }

    public FigureStructure(BoardFirureId firureId, FigureType initialType, FigureType currentType, FigureTeamType teamType)
    {
        FirureId = firureId;
        InitialType = initialType;
        CurrentType = currentType;
        TeamType = teamType;
    }
}