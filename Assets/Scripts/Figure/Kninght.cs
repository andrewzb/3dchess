using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Kninght : FigureDefaultMonoBehavior, IFigure
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
            CellMarkupStructure forwardFork = GetForwardKnightFork(cellIdsList, index);
            CellMarkupStructure backwardFork = GetBackwardKnightFork(cellIdsList, index);
            CellMarkupStructure rightFork = GetRightKnightFork(cellIdsList, index);
            CellMarkupStructure leftFork = GetLeftKnightFork(cellIdsList, index);

            localCellIdsListBYType.canBeCapture = localCellIdsListBYType.canBeCapture
               .Concat(forwardFork.canBeCapture)
               .Concat(backwardFork.canBeCapture)
               .Concat(rightFork.canBeCapture)
               .Concat(leftFork.canBeCapture)
               .ToList();


            localCellIdsListBYType.canBeDreggedTo = localCellIdsListBYType.canBeDreggedTo
               .Concat(forwardFork.canBeDreggedTo)
               .Concat(backwardFork.canBeDreggedTo)
               .Concat(rightFork.canBeDreggedTo)
               .Concat(leftFork.canBeDreggedTo)
               .ToList();
        }
        return localCellIdsListBYType;
    }
}
