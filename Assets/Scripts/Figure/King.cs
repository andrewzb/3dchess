using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class King : FigureDefaultMonoBehavior, IFigure
{
    public CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure();
        localCellIdsListBYType.canBeCapture = new List<BoardCellId>();
        localCellIdsListBYType.canBeDreggedTo = new List<BoardCellId>();
        FigureTeamType teamType = figure.TeamType;
        BoardCellId currentCellId = boardCell.CellId;
        int index = cellIdsList.FindIndex(c => c.CellId == boardCell.CellId);
        if (index != -1)
        {
            CellMarkupStructure forward = GetForwardMoveCellSIdByCount(cellIdsList, index, 1);
            CellMarkupStructure backward = GetBackwardMoveCellSIdByCount(cellIdsList, index, 1);
            CellMarkupStructure right = GetRightMoveCellsIdByCount(cellIdsList, index, 1);
            CellMarkupStructure left = GetLeftMoveCellsIdByCount(cellIdsList, index, 1);
            CellMarkupStructure forwardLeft = GetForwardLeftMoveCellsIdByCount(cellIdsList, index, 1);
            CellMarkupStructure forwardRight = GetForwardRightMoveCellsIdByCount(cellIdsList, index, 1);
            CellMarkupStructure backwardLeft = GetBackwardLeftMoveCellsIdByCount(cellIdsList, index, 1);
            CellMarkupStructure backwardRight = GetBackwardRightMoveCellsIdByCount(cellIdsList, index, 1);

            localCellIdsListBYType.canBeCapture = localCellIdsListBYType.canBeCapture
               .Concat(forward.canBeCapture)
               .Concat(backward.canBeCapture)
               .Concat(right.canBeCapture)
               .Concat(left.canBeCapture)
               .Concat(forwardLeft.canBeCapture)
               .Concat(forwardRight.canBeCapture)
               .Concat(backwardLeft.canBeCapture)
               .Concat(backwardRight.canBeCapture)
               .ToList();

            localCellIdsListBYType.canBeDreggedTo = localCellIdsListBYType.canBeDreggedTo
               .Concat(forward.canBeDreggedTo)
               .Concat(backward.canBeDreggedTo)
               .Concat(right.canBeDreggedTo)
               .Concat(left.canBeDreggedTo)
               .Concat(forwardLeft.canBeDreggedTo)
               .Concat(forwardRight.canBeDreggedTo)

               .Concat(backwardLeft.canBeDreggedTo)
               .Concat(backwardRight.canBeDreggedTo)
               .ToList();
        }
        return localCellIdsListBYType;
    }
}
