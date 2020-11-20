using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField]
    public Material defaultWhiteMaterial;
    [SerializeField]
    public Material defaultBlackMaterial;
    [SerializeField]
    public GameObject figureMesh;
    public BoardFirureId FirureId { get; set; }
    public FigureType InitialType { get; set; }
    public FigureType CurrentType { get; set;  }
    public FigureTeamType TeamType { get; set; }

    public GameObject GetInitFigure(
        BoardFirureId firureId,
        FigureType initialType,
        FigureType currentType,
        FigureTeamType teamType
    )
    {
        FirureId = firureId;
        InitialType = initialType;
        CurrentType = currentType;
        TeamType = teamType;
        if (teamType == FigureTeamType.black)
            figureMesh.GetComponent<MeshRenderer>().material = defaultBlackMaterial;
        else
            figureMesh.GetComponent<MeshRenderer>().material = defaultWhiteMaterial;

        return gameObject;
    }

    public List<BoardCellId> GetCellsOnWhihFigureCanBeDragged()
    {
        return new List<BoardCellId>()
        {
            BoardCellId.B1,
            BoardCellId.B2,
            BoardCellId.C2,
            BoardCellId.D2,
            BoardCellId.D1
        };
    }

    public List<BoardCellId> GetCellsOnWhihFigureCanBeDragged1()
    {
        return new List<BoardCellId>()
        {
            BoardCellId.C7,
            BoardCellId.C8,
            BoardCellId.E5,
            BoardCellId.D4,
            BoardCellId.F7
        };
    }


}
