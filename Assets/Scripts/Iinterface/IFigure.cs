using System.Collections.Generic;


interface IFigure
{
    List<BoardCellId> GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell);
}
