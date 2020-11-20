using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour, IFigure
{
    private bool firsMove = true;
    private Figure figure;

    private void Awake()
    {
        figure = gameObject.GetComponent<Figure>();
    }



    public List<BoardCellId> GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        List<BoardCellId> localCellIdsList = new List<BoardCellId>();
        BoardCellId currentCellId = boardCell.CellId;
        int index = cellIdsList.FindIndex(c => c.CellId == boardCell.CellId);
        if (firsMove)
        {
            firsMove = false;
            localCellIdsList.Add(cellIdsList[index + 8].CellId);
            localCellIdsList.Add(cellIdsList[index + 16].CellId);
        }
        else
        {
            localCellIdsList.Add(cellIdsList[index + 8].CellId);
        }
        return localCellIdsList;
    }

}
