using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CellMarkupStructure
{
    public List<BoardCellId> canBeDreggedTo { get; set; }
    public List<BoardCellId> canBeCapture { get; set; }

}
