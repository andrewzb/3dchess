﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bishop : FigureDefaultMonoBehavior, IFigure
{
    public void AfterMove()
    {
        return;
    }

    public CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure().Init();
        BoardCellId currentCellId = boardCell.CellId;
        int index = cellIdsList.FindIndex(c => c.CellId == boardCell.CellId);
        if (index != -1)
        {
            CellMarkupStructure forwardRightList = GetForwardRightMoveCellsId(cellIdsList, index);
            CellMarkupStructure forwardLeftList = GetForwardLeftMoveCellsId(cellIdsList, index);
            CellMarkupStructure backwardLeftList = GetBackwardLeftMoveCellsId(cellIdsList, index);
            CellMarkupStructure backwardRightList = GetBackwardRightMoveCellsId(cellIdsList, index);
            localCellIdsListBYType.canBeCapture = localCellIdsListBYType.canBeCapture
               .Concat(forwardRightList.canBeCapture)
               .Concat(forwardLeftList.canBeCapture)
               .Concat(backwardLeftList.canBeCapture)
               .Concat(backwardRightList.canBeCapture)
               .ToList();
            localCellIdsListBYType.canBeDreggedTo = localCellIdsListBYType.canBeDreggedTo
               .Concat(forwardRightList.canBeDreggedTo)
               .Concat(forwardLeftList.canBeDreggedTo)
               .Concat(backwardLeftList.canBeDreggedTo)
               .Concat(backwardRightList.canBeDreggedTo)
               .ToList();
        }
        return localCellIdsListBYType;
    }
}
