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

}
