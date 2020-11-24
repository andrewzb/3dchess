using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour, IFigure
{
    private bool firsMove = true;

    public CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure();
        localCellIdsListBYType.canBeCapture = new List<BoardCellId>();
        localCellIdsListBYType.canBeDreggedTo = new List<BoardCellId>();
        BoardCellId currentCellId = boardCell.CellId;
        int index = cellIdsList.FindIndex(c => c.CellId == boardCell.CellId);
        if (firsMove)
        {
            firsMove = false;
            localCellIdsListBYType.canBeDreggedTo = new List<BoardCellId>();
            localCellIdsListBYType.canBeDreggedTo.Add(cellIdsList[index + 8].CellId);
            localCellIdsListBYType.canBeDreggedTo.Add(cellIdsList[index + 16].CellId);
        }
        else
        {
            localCellIdsListBYType.canBeDreggedTo = new List<BoardCellId>();
            localCellIdsListBYType.canBeDreggedTo.Add(cellIdsList[index + 8].CellId);
        }
        localCellIdsListBYType.canBeCapture = new List<BoardCellId>();
        return localCellIdsListBYType;
    }

}
