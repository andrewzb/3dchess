﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pawn : FigureDefaultMonoBehavior, IFigure
{
    private bool firsMove = true;

    public CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure();
        localCellIdsListBYType.canBeCapture = new List<BoardCellId>();
        localCellIdsListBYType.canBeDreggedTo = new List<BoardCellId>();

        FigureTeamType teamType = figure.TeamType;

        BoardCellId currentCellId = boardCell.CellId;
        int index = cellIdsList.FindIndex(c => c.CellId == boardCell.CellId);
        if (teamType == FigureTeamType.black)
        {
            CellMarkupStructure leftForward = GetForwardLeftMoveCellsIdByCount(cellIdsList, index, 1, false, true);
            CellMarkupStructure rightForward = GetForwardRightMoveCellsIdByCount(cellIdsList, index, 1, false, true);
            CellMarkupStructure forward = GetForwardMoveCellSIdByCount(cellIdsList, index, firsMove ? 2 : 1, true, false);
            localCellIdsListBYType = forward;
            localCellIdsListBYType.canBeCapture = localCellIdsListBYType.canBeCapture.Concat(leftForward.canBeCapture).Concat(rightForward.canBeCapture).ToList();
        }
        else
        {
            CellMarkupStructure leftForward = GetBackwardLeftMoveCellsIdByCount(cellIdsList, index, 1, false, true);
            CellMarkupStructure rightForward = GetBackwardRightMoveCellsIdByCount(cellIdsList, index, 1, false, true);
            CellMarkupStructure forward = GetBackwardMoveCellSIdByCount(cellIdsList, index, firsMove ? 2 : 1, true, false);
            localCellIdsListBYType = forward;
            localCellIdsListBYType.canBeCapture = localCellIdsListBYType.canBeCapture.Concat(leftForward.canBeCapture).Concat(rightForward.canBeCapture).ToList();
        }
        firsMove = false;




        return localCellIdsListBYType;
    }

}
