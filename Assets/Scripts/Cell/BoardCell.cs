using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    [SerializeField]
    public BoardCellId CellId;
    [SerializeField]
    public GameObject cellMarker;
    [SerializeField]
    public GameObject cellBase;
    [SerializeField]
    public BoardCellType cellType;
    [SerializeField]
    public Material defaultWhiteMaterial;
    [SerializeField]
    public Material defaultBlackMaterial;
    [SerializeField]
    public Material selectedMaterial;
    [SerializeField]
    public Material canBeDragedMaterial;
    [SerializeField]
    public Material cantBeDragedMaterial;
    [SerializeField]
    public Material captureCellMaterial;
    public GameObject figureOnCell = null;

    private ActionColorsType markStatus = ActionColorsType.normal;

    public void ResetCellMark()
    {
        if (cellType == BoardCellType.black)
            cellMarker.GetComponent<MeshRenderer>().material = defaultBlackMaterial;
        else
            cellMarker.GetComponent<MeshRenderer>().material = defaultWhiteMaterial;
        markStatus = ActionColorsType.normal;

    }


    public void MarkAsSelected ()
    {
        if (markStatus != ActionColorsType.selected)
        {
            markStatus = ActionColorsType.selected;
            cellMarker.GetComponent<MeshRenderer>().material = selectedMaterial;
        }

    }

    public void MarkAsCanBeDragger()
    {
        if (markStatus != ActionColorsType.canBeDraged)
        {
            markStatus = ActionColorsType.canBeDraged;
            cellMarker.GetComponent<MeshRenderer>().material = canBeDragedMaterial;
        }
    }

    public void MarkAsCanBeCapture()
    {
        if (markStatus != ActionColorsType.captureCell)
        {
            markStatus = ActionColorsType.captureCell;
            cellMarker.GetComponent<MeshRenderer>().material = captureCellMaterial;
        }
    }

    public void InitCell()
    {

    }


    public void SpawnFigure(GameObject BoardFigure, FigureStructure figureStructure)
    {
        GameObject figureGameObject = Instantiate(BoardFigure, transform.position, Quaternion.identity);
        Figure figure = figureGameObject.GetComponent<Figure>();
        figureOnCell = figure.GetInitFigure(figureStructure.FirureId, figureStructure.InitialType, figureStructure.CurrentType, figureStructure.TeamType);
    }


    public Vector3 GetCellPivotPointVector()
    {
        return gameObject.transform.position;
    }

}
