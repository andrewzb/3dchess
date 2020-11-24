using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FigureDefaultMonoBehavior : MonoBehaviour
{
    protected Figure figure;

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


    // range by count
    public CellMarkupStructure GetForwardMoveCellSIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0 ; i < count; i++)
        {
            BoardCell boardCell = GetForwardCell(cellIdsList, ref localIndex);
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
                        if (canBeCapture)
                        localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetBackwardMoveCellSIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetForwardRightMoveCellsIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetForwardLeftMoveCellsIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetBackwardLeftMoveCellsIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetBackwardRightMoveCellsIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetRightMoveCellsIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        int rowIndex = (int)(index / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetLeftMoveCellsIdByCount(List<BoardCell> cellIdsList, int index, int count, bool canBeDragged = true, bool canBeCapture = true)
    {
        int rowIndex = (int)(index / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        FigureTeamType figureTeamType = figure.TeamType;
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        int localIndex = index;
        for (int i = 0; i < count; i++)
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
                        if (canBeCapture)
                            localCellIdsListCanBeCapture.Add(boardCell.CellId);
                        break;
                    }

                }
                else
                {
                    if (canBeDragged)
                        localCellIdsListForDragg.Add(boardCell.CellId);
                }
            }
            else
            {
                break;
            }
        }
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        return localCellMarkupStruct;
    }

    // knight

    public CellMarkupStructure GetForwardKnightFork(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        int localIndex = index + 16;
        if (localIndex > 63)
        {
            return localCellMarkupStruct;
        }
        CellMarkupStructure right = GetRightMoveCellsIdByCount(cellIdsList, localIndex, 1);
        CellMarkupStructure left = GetLeftMoveCellsIdByCount(cellIdsList, localIndex, 1);

        localCellMarkupStruct.canBeCapture = localCellMarkupStruct.canBeCapture
            .Concat(right.canBeCapture)
            .Concat(left.canBeCapture)
            .ToList();

        localCellMarkupStruct.canBeDreggedTo = localCellMarkupStruct.canBeDreggedTo
            .Concat(right.canBeDreggedTo)
            .Concat(left.canBeDreggedTo)
            .ToList();

        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetBackwardKnightFork(List<BoardCell> cellIdsList, int index)
    {
        FigureTeamType figureTeamType = figure.TeamType;
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        int localIndex = index - 16;
        if (localIndex < 0)
        {
            return localCellMarkupStruct;
        }
        CellMarkupStructure right = GetRightMoveCellsIdByCount(cellIdsList, localIndex, 1);
        CellMarkupStructure left = GetLeftMoveCellsIdByCount(cellIdsList, localIndex, 1);

        localCellMarkupStruct.canBeCapture = localCellMarkupStruct.canBeCapture
            .Concat(right.canBeCapture)
            .Concat(left.canBeCapture)
            .ToList();

        localCellMarkupStruct.canBeDreggedTo = localCellMarkupStruct.canBeDreggedTo
            .Concat(right.canBeDreggedTo)
            .Concat(left.canBeDreggedTo)
            .ToList();

        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetRightKnightFork(List<BoardCell> cellIdsList, int index)
    {
        int rowIndex = (int)(index / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        FigureTeamType figureTeamType = figure.TeamType;
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        int localIndex = index + 2;
        if (localIndex > hieBoundary | localIndex < lowBoundary)
        {
            return localCellMarkupStruct;
        }
        CellMarkupStructure forward = GetForwardMoveCellSIdByCount(cellIdsList, localIndex, 1);
        CellMarkupStructure backward = GetBackwardMoveCellSIdByCount(cellIdsList, localIndex, 1);

        localCellMarkupStruct.canBeCapture = localCellMarkupStruct.canBeCapture
            .Concat(forward.canBeCapture)
            .Concat(backward.canBeCapture)
            .ToList();

        localCellMarkupStruct.canBeDreggedTo = localCellMarkupStruct.canBeDreggedTo
            .Concat(forward.canBeDreggedTo)
            .Concat(backward.canBeDreggedTo)
            .ToList();

        return localCellMarkupStruct;
    }

    public CellMarkupStructure GetLeftKnightFork(List<BoardCell> cellIdsList, int index)
    {
        int rowIndex = (int)(index / 8) + 1;
        int lowBoundary = ((rowIndex - 1) * 8);
        int hieBoundary = ((rowIndex) * 8) - 1;
        FigureTeamType figureTeamType = figure.TeamType;
        CellMarkupStructure localCellMarkupStruct = new CellMarkupStructure();
        List<BoardCellId> localCellIdsListForDragg = new List<BoardCellId>();
        List<BoardCellId> localCellIdsListCanBeCapture = new List<BoardCellId>();
        localCellMarkupStruct.canBeCapture = localCellIdsListCanBeCapture;
        localCellMarkupStruct.canBeDreggedTo = localCellIdsListForDragg;
        int localIndex = index - 2;
        if (localIndex > hieBoundary | localIndex < lowBoundary)
        {
            return localCellMarkupStruct;
        }
        CellMarkupStructure forward = GetForwardMoveCellSIdByCount(cellIdsList, localIndex, 1);
        CellMarkupStructure backward = GetBackwardMoveCellSIdByCount(cellIdsList, localIndex, 1);

        localCellMarkupStruct.canBeCapture = localCellMarkupStruct.canBeCapture
            .Concat(forward.canBeCapture)
            .Concat(backward.canBeCapture)
            .ToList();

        localCellMarkupStruct.canBeDreggedTo = localCellMarkupStruct.canBeDreggedTo
            .Concat(forward.canBeDreggedTo)
            .Concat(backward.canBeDreggedTo)
            .ToList();

        return localCellMarkupStruct;
    }

}
