using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureDefaultMonoBehavior : MonoBehaviour
{
    private Figure figure;

    private void Awake()
    {
        figure = gameObject.GetComponent<Figure>();
    }

    public BoardCell GetForwardCell(List<BoardCell> cellIdsList, ref int index)
    {
        index += 8;
        if (index > 63 || index < 0)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetBackwardCell(List<BoardCell> cellIdsList, ref int index)
    {
        index -= 8;
        if (index > 63 || index < 0)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetRightCell(List<BoardCell> cellIdsList, ref int index, int lowBoundary, int hieBoundary)
    {
        index++;
        if (index > hieBoundary | index < lowBoundary)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetLeftCell(List<BoardCell> cellIdsList, ref int index, int lowBoundary, int hieBoundary)
    {
        index--;
        if (index > hieBoundary || index < lowBoundary)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetForwardRightCell(List<BoardCell> cellIdsList, ref int index)
    {
        int localIndex = index + 8;
        int rowIndex = (int)(localIndex / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        index+=9;
        if (index > hieBoundary || index < lowBoundary || index > 63)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetForwardLeftCell(List<BoardCell> cellIdsList, ref int index)
    {
        int localIndex = index + 8;
        int rowIndex = (int)(localIndex / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        index += 7;
        if (index > hieBoundary || index < lowBoundary || index > 63)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetBackwardRightCell(List<BoardCell> cellIdsList, ref int index)
    {
        int localIndex = index - 8;
        int rowIndex = (int)(localIndex / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        index -= 7;
        if (index > hieBoundary || index < lowBoundary || index < 0)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public BoardCell GetBackwardLeftCell(List<BoardCell> cellIdsList, ref int index)
    {
        int localIndex = index - 8;
        int rowIndex = (int)(localIndex / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        index -= 9;
        if (index > hieBoundary || index < lowBoundary || index < 0)
        {
            return null;
        }
        return cellIdsList[index];
    }

    public CellMarkupStructure GetForwardMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetForwardCell(cellIdsList, ref localIndex);
            if(boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }
    
                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetBackwardMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetBackwardCell(cellIdsList, ref localIndex);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetRightMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        int rowIndex = (int)(index / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetRightCell(cellIdsList, ref localIndex, lowBoundary, hieBoundary);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }
                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetLeftMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        int rowIndex = (int)(index / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetLeftCell(cellIdsList, ref localIndex, lowBoundary, hieBoundary);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }
                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetForwardRightMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetForwardRightCell(cellIdsList, ref localIndex);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetForwardLeftMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetForwardLeftCell(cellIdsList, ref localIndex);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }


    public CellMarkupStructure GetBackwardRightMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetBackwardRightCell(cellIdsList, ref localIndex);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetBackwardLeftMoveCellsId(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        do
        {
            BoardCell boardCell = GetBackwardLeftCell(cellIdsList, ref localIndex);
            if (boardCell != null)
            {
                if (boardCell.figureOnCell != null)
                {
                    Figure localFigureOnCell = boardCell.figureOnCell.GetComponent<Figure>();
                    if (localFigureOnCell.TeamType == figureTeamType)
                    {
                        break;
                    }
                    else
                    {
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        } while (true);
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

}
