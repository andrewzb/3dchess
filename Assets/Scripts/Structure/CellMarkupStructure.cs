using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CellMarkupStructure
{
    public List<BoardCellId> canBeDreggedTo { get; set; }
    public List<BoardCellId> canBeCapture { get; set; }
    public List<BoardCellId> inDanger { get; set; }
    public List<CastleStructure> canBeCastle { get; set; }
 
    public CellMarkupStructure(bool initialize = true)
    {
        canBeDreggedTo = new List<BoardCellId>();
        canBeCapture = new List<BoardCellId>();
        inDanger = new List<BoardCellId>();
        canBeCastle = new List<CastleStructure>();
    }

    public CellMarkupStructure Init()
    {
        canBeDreggedTo = new List<BoardCellId>();
        canBeCapture = new List<BoardCellId>();
        inDanger = new List<BoardCellId>();
        canBeCastle = new List<CastleStructure>();
        return this;
    }
}
