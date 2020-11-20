using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoardCellStruct
{
    public BoardCellId CellId { get; }
    public BoardCellType CellType { get; }
    public FigureStructure? Figure { get; }

    public BoardCellStruct(BoardCellId cellId, BoardCellType cellType, FigureStructure figure)
    {
        CellId = cellId;
        CellType = cellType;
        Figure = figure;
    }

    public BoardCellStruct(BoardCellId cellId, BoardCellType cellType)
    {
        CellId = cellId;
        CellType = cellType;
        Figure = null;
    }


}
