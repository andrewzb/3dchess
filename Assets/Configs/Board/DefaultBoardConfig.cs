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
            //new BoardCellStruct(BoardCellId.A1, BoardCellType.white, new FigureStructure(BoardFirureId.white_pawn_1, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
           // new BoardCellStruct(BoardCellId.B1, BoardCellType.black),
            //new BoardCellStruct(BoardCellId.C1, BoardCellType.white, new FigureStructure(BoardFirureId.black_pawn_1, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            //new BoardCellStruct(BoardCellId.B1, BoardCellType.black, new FigureStructure(BoardFirureId.black_rook_1, FigureType.rook, FigureType.rook, FigureTeamType.black)),
            //new BoardCellStruct(BoardCellId.E5, BoardCellType.white, new FigureStructure(BoardFirureId.black_bishop_1, FigureType.bishop, FigureType.bishop, FigureTeamType.black)),

            new BoardCellStruct(BoardCellId.A1, BoardCellType.white, new FigureStructure(BoardFirureId.black_rook_1, FigureType.rook, FigureType.rook, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.B1, BoardCellType.black, new FigureStructure(BoardFirureId.black_knight_1, FigureType.knight, FigureType.knight, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.C1, BoardCellType.white, new FigureStructure(BoardFirureId.black_bishop_1, FigureType.bishop, FigureType.bishop, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.D1, BoardCellType.black, new FigureStructure(BoardFirureId.black_queen_1, FigureType.queen, FigureType.queen, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.E1, BoardCellType.white, new FigureStructure(BoardFirureId.black_king_1, FigureType.king, FigureType.king, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.F1, BoardCellType.black, new FigureStructure(BoardFirureId.black_bishop_2, FigureType.bishop, FigureType.bishop, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.G1, BoardCellType.white, new FigureStructure(BoardFirureId.black_knight_2, FigureType.knight, FigureType.knight, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.H1, BoardCellType.black, new FigureStructure(BoardFirureId.black_rook_2, FigureType.rook, FigureType.rook, FigureTeamType.black))

            //new BoardCellStruct(BoardCellId.A8, BoardCellType.black, new FigureStructure(BoardFirureId.white_rook_1, FigureType.rook, FigureType.rook, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.B8, BoardCellType.white, new FigureStructure(BoardFirureId.white_knight_1, FigureType.knight, FigureType.knight, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.C8, BoardCellType.black, new FigureStructure(BoardFirureId.white_bishop_1, FigureType.bishop, FigureType.bishop, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.D8, BoardCellType.white, new FigureStructure(BoardFirureId.white_queen_1, FigureType.queen, FigureType.queen, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.E8, BoardCellType.black, new FigureStructure(BoardFirureId.white_king_1, FigureType.king, FigureType.king, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.F8, BoardCellType.white, new FigureStructure(BoardFirureId.white_bishop_2, FigureType.bishop, FigureType.bishop, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.G8, BoardCellType.black, new FigureStructure(BoardFirureId.white_knight_2, FigureType.knight, FigureType.knight, FigureTeamType.white)),
            //new BoardCellStruct(BoardCellId.H8, BoardCellType.white, new FigureStructure(BoardFirureId.white_rook_2, FigureType.rook, FigureType.rook, FigureTeamType.white)),
        };

        return tempConfig;
    }
}
