using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class King : FigureDefaultMonoBehavior, IFigure
{
    public bool FirsMove { get; private set; } = true;

    public void AfterMove()
    {
        FirsMove = false;
    }

    public CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure kingDisplaisment = GetKingDisplasment(cellIdsList, boardCell);
        kingDisplaisment.inDanger = GetDengerCellSTructure(cellIdsList, boardCell);
        return kingDisplaisment;
    }

    public CellMarkupStructure GetKingDisplasment(List<BoardCell> cellIdsList, BoardCell boardCell)
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure().Init();
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

            if(FirsMove)
            {
                CellMarkupStructure castleLeft = GetLeftCastleCellsId(cellIdsList, index);
                CellMarkupStructure castleRight = GetRightCastleCellsId(cellIdsList, index);

                localCellIdsListBYType.canBeCastle = localCellIdsListBYType.canBeCastle
                       .Concat(castleLeft.canBeCastle)
                       .Concat(castleRight.canBeCastle)
                       .ToList();

            }
        }
        return localCellIdsListBYType;
    }

    private FigureType GetFigureTypeByCellId(List<BoardCell> cellIdsList, BoardCellId boardCellId)
    {
        int index = cellIdsList.FindIndex(c => c.CellId == boardCellId);
        if (index != -1)
        {
            BoardCell boardCell = cellIdsList[index];
            if (boardCell != null)
            {
                GameObject figure = boardCell.figureOnCell;
                if (figure != null)
                {
                    FigureType type = figure.GetComponent<Figure>().CurrentType;
                    return type;
                }
            }
        }
        return FigureType.none;
    }

    public bool isCellInDanger(List<BoardCell> cellIdsList, BoardCellId cellId)
    {
        bool inDanger = false;
        List<BoardCellId> enem = new List<BoardCellId>();
        int index = cellIdsList.FindIndex(c => c.CellId == cellId);

        // thry x
        CellMarkupStructure forwardRightList = GetForwardRightMoveCellsId(cellIdsList, index);
        CellMarkupStructure forwardLeftList = GetForwardLeftMoveCellsId(cellIdsList, index);
        CellMarkupStructure backwardLeftList = GetBackwardLeftMoveCellsId(cellIdsList, index);
        CellMarkupStructure backwardRightList = GetBackwardRightMoveCellsId(cellIdsList, index);

        List<BoardCellId> enemyListDiogonalsCellsId = forwardRightList.canBeCapture
            .Concat(forwardLeftList.canBeCapture)
            .Concat(backwardLeftList.canBeCapture)
            .Concat(backwardRightList.canBeCapture)
            .Distinct()
            .ToList();
        foreach (BoardCellId id in enemyListDiogonalsCellsId)
        {
            FigureType type = GetFigureTypeByCellId(cellIdsList, id);
            if (type == FigureType.bishop || type == FigureType.queen)
            {
                inDanger = true;
                break;
            }
        }

        // thry +
        if (!inDanger)
        {
            CellMarkupStructure forwardList = GetForwardMoveCellsId(cellIdsList, index);
            CellMarkupStructure backwardList = GetBackwardMoveCellsId(cellIdsList, index);
            CellMarkupStructure rightList = GetRightMoveCellsId(cellIdsList, index);
            CellMarkupStructure leftList = GetLeftMoveCellsId(cellIdsList, index);

            List<BoardCellId> enemyListHorisontalVerticalCellsId = forwardList.canBeCapture
                .Concat(backwardList.canBeCapture)
                .Concat(rightList.canBeCapture)
                .Concat(leftList.canBeCapture)
                .Distinct()
                .ToList();

            foreach (BoardCellId id in enemyListHorisontalVerticalCellsId)
            {
                FigureType type = GetFigureTypeByCellId(cellIdsList, id);
                if (type == FigureType.rook || type == FigureType.queen)
                {
                    inDanger = true;
                    break;
                }
            }
        }

        // knight
        if (!inDanger)
        {
            CellMarkupStructure forwardFork = GetForwardKnightFork(cellIdsList, index);
            CellMarkupStructure backwardFork = GetBackwardKnightFork(cellIdsList, index);
            CellMarkupStructure rightFork = GetRightKnightFork(cellIdsList, index);
            CellMarkupStructure leftFork = GetLeftKnightFork(cellIdsList, index);

            List<BoardCellId> enemyKnights = forwardFork.canBeCapture
                .Concat(backwardFork.canBeCapture)
                .Concat(rightFork.canBeCapture)
                .Concat(leftFork.canBeCapture)
                .Distinct()
                .ToList();
            foreach (BoardCellId id in enemyKnights)
            {
                FigureType type = GetFigureTypeByCellId(cellIdsList, id);
                if (type == FigureType.knight)
                {
                    inDanger = true;
                    break;
                }
            }
        }
        if (inDanger)
        {
            return true;
        }
        return false;
    }

    public List<BoardCellId> GetDengerCellSTructure(List<BoardCell> cellIdsList, BoardCell boardCell, bool withCurrentKingLocation = true )
    {
        CellMarkupStructure localCellIdsListBYType = new CellMarkupStructure().Init();
        CellMarkupStructure awalibleFields = GetKingDisplasment(cellIdsList, boardCell);
        List<BoardCellId> kingsAwalibleCellId = new List<BoardCellId>().Concat(awalibleFields.canBeDreggedTo).Concat(awalibleFields.canBeCapture).ToList();
        if (withCurrentKingLocation)
        {
            kingsAwalibleCellId.Add(boardCell.CellId);
        }
        List<BoardCellId> kingInDangerCells = new List<BoardCellId>();
        foreach (BoardCellId id in kingsAwalibleCellId)
        {
            if (isCellInDanger(cellIdsList, id))
            {
                kingInDangerCells.Add(id);
            }
        }
        return kingInDangerCells;
    }





}
