using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : FigureDefaultMonoBehavior, IFigure
{

    public CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure();
        localCellIdsListBYType.canBeCapture = new List<BoardCellId>();
        localCellIdsListBYType.canBeDreggedTo = new List<BoardCellId>();
        BoardCellId currentCellId = boardCell.CellId;
        int index = cellIdsList.FindIndex(c => c.CellId == boardCell.CellId);
        if (index != -1)
        {
            CellMarkupStructure forwardList = GetForwardMoveCellsId(cellIdsList, index);
            CellMarkupStructure backwardList = GetBackwardMoveCellsId(cellIdsList, index);
            CellMarkupStructure rightList = GetRightMoveCellsId(cellIdsList, index);
            CellMarkupStructure leftList = GetLeftMoveCellsId(cellIdsList, index);
            localCellIdsListBYType.canBeCapture = localCellIdsListBYType.canBeCapture
                .Concat(forwardList.canBeCapture)
                .Concat(backwardList.canBeCapture)
                .Concat(rightList.canBeCapture)
                .Concat(leftList.canBeCapture)
                .ToList();
            localCellIdsListBYType.canBeDreggedTo = localCellIdsListBYType.canBeDreggedTo
                .Concat(forwardList.canBeDreggedTo)
                .Concat(backwardList.canBeDreggedTo)
                .Concat(rightList.canBeDreggedTo)
                .Concat(leftList.canBeDreggedTo)
                .ToList();
        }
        return localCellIdsListBYType;
    }
}
