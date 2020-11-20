using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBoardConfig
{
    public List<BoardCellStruct> defaultBoardListConfig = null;
    public DefaultBoardConfig ()
    {
        defaultBoardListConfig = PopulateDefaultBoardListConfig();
    }

    private List<BoardCellStruct> PopulateDefaultBoardListConfig()
    {
        List<BoardCellStruct> tempConfig = new List<BoardCellStruct>()
        {
            new BoardCellStruct(BoardCellId.A1, BoardCellType.white, new FigureStructure(BoardFirureId.white_pawn_1, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.B1, BoardCellType.black),
            new BoardCellStruct(BoardCellId.C1, BoardCellType.white, new FigureStructure(BoardFirureId.black_pawn_1, FigureType.pawn, FigureType.pawn, FigureTeamType.black))
        };

        return tempConfig;
    }
}
