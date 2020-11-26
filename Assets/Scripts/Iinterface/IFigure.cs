using System.Collections.Generic;


interface IFigure
{
    CellMarkupStructure GetCellIdsOnWithCanBeDraged(List<BoardCell> cellIdsList, BoardCell boardCell);
    void AfterMove();
}
