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
            //Black
            // main figures
            new BoardCellStruct(BoardCellId.A1, BoardCellType.white, new FigureStructure(BoardFirureId.black_rook_1, FigureType.rook, FigureType.rook, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.B1, BoardCellType.black, new FigureStructure(BoardFirureId.black_knight_1, FigureType.knight, FigureType.knight, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.C1, BoardCellType.white, new FigureStructure(BoardFirureId.black_bishop_1, FigureType.bishop, FigureType.bishop, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.D1, BoardCellType.black, new FigureStructure(BoardFirureId.black_queen_1, FigureType.queen, FigureType.queen, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.E1, BoardCellType.white, new FigureStructure(BoardFirureId.black_king_1, FigureType.king, FigureType.king, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.F1, BoardCellType.black, new FigureStructure(BoardFirureId.black_bishop_2, FigureType.bishop, FigureType.bishop, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.G1, BoardCellType.white, new FigureStructure(BoardFirureId.black_knight_2, FigureType.knight, FigureType.knight, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.H1, BoardCellType.black, new FigureStructure(BoardFirureId.black_rook_2, FigureType.rook, FigureType.rook, FigureTeamType.black)),
            //pawns
            new BoardCellStruct(BoardCellId.A2, BoardCellType.black, new FigureStructure(BoardFirureId.black_pawn_1, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.B2, BoardCellType.white, new FigureStructure(BoardFirureId.black_pawn_2, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.C2, BoardCellType.black, new FigureStructure(BoardFirureId.black_pawn_3, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.D2, BoardCellType.white, new FigureStructure(BoardFirureId.black_pawn_4, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.E2, BoardCellType.black, new FigureStructure(BoardFirureId.black_pawn_5, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.F2, BoardCellType.white, new FigureStructure(BoardFirureId.black_pawn_6, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.G2, BoardCellType.black, new FigureStructure(BoardFirureId.black_pawn_7, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            new BoardCellStruct(BoardCellId.H2, BoardCellType.white, new FigureStructure(BoardFirureId.black_pawn_8, FigureType.pawn, FigureType.pawn, FigureTeamType.black)),
            //White
            //main figures
            new BoardCellStruct(BoardCellId.A8, BoardCellType.black, new FigureStructure(BoardFirureId.white_rook_1, FigureType.rook, FigureType.rook, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.B8, BoardCellType.white, new FigureStructure(BoardFirureId.white_knight_1, FigureType.knight, FigureType.knight, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.C8, BoardCellType.black, new FigureStructure(BoardFirureId.white_bishop_1, FigureType.bishop, FigureType.bishop, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.D8, BoardCellType.white, new FigureStructure(BoardFirureId.white_queen_1, FigureType.queen, FigureType.queen, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.E8, BoardCellType.black, new FigureStructure(BoardFirureId.white_king_1, FigureType.king, FigureType.king, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.F8, BoardCellType.white, new FigureStructure(BoardFirureId.white_bishop_2, FigureType.bishop, FigureType.bishop, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.G8, BoardCellType.black, new FigureStructure(BoardFirureId.white_knight_2, FigureType.knight, FigureType.knight, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.H8, BoardCellType.white, new FigureStructure(BoardFirureId.white_rook_2, FigureType.rook, FigureType.rook, FigureTeamType.white)),
             //pawns
            new BoardCellStruct(BoardCellId.A7, BoardCellType.white, new FigureStructure(BoardFirureId.white_pawn_1, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.B7, BoardCellType.black, new FigureStructure(BoardFirureId.white_pawn_2, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.C7, BoardCellType.white, new FigureStructure(BoardFirureId.white_pawn_3, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.D7, BoardCellType.black, new FigureStructure(BoardFirureId.white_pawn_4, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.E7, BoardCellType.white, new FigureStructure(BoardFirureId.white_pawn_5, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.F7, BoardCellType.black, new FigureStructure(BoardFirureId.white_pawn_6, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.G7, BoardCellType.white, new FigureStructure(BoardFirureId.white_pawn_7, FigureType.pawn, FigureType.pawn, FigureTeamType.white)),
            new BoardCellStruct(BoardCellId.H7, BoardCellType.black, new FigureStructure(BoardFirureId.white_pawn_8, FigureType.pawn, FigureType.pawn, FigureTeamType.white))
        };

        return tempConfig;
    }
}
