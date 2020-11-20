using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : SingeltonMonoBehavior<Board>
{
    [SerializeField]
    public List<BoardCell> BoardCellsList;
    // figures
    [SerializeField] public GameObject pawn;
    // figures


    // config for initialization
    private List<BoardCellStruct> defaultBoardStructure;
    // list of state updetes count from default
    private List<TurnChange> ChangeList;
    private Ray lastRay;
    private BoardCell selectedBoardSell = null;
    private List<BoardCellId> markCellsAsCanBeDragToList = null;
    private Camera mainCamera;


    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        defaultBoardStructure = new DefaultBoardConfig().defaultBoardListConfig;
    }

    private void Start()
    {
        InitBoard();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                BoardCell boardCell = hit.collider.gameObject.GetComponent<BoardCell>();
                if (boardCell.figureOnCell == null && selectedBoardSell == null)
                    return;
                if (selectedBoardSell != null && markCellsAsCanBeDragToList != null && selectedBoardSell.CellId == boardCell.CellId)
                {
                    Debug.Log("1111");
                    selectedBoardSell.ResetCellMark();
                    ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
                    selectedBoardSell = null;
                    markCellsAsCanBeDragToList = null;
                }
                else if (selectedBoardSell != null && markCellsAsCanBeDragToList != null && selectedBoardSell.CellId != boardCell.CellId)
                {
                    // if you with selected your's figure
                    // 1 select your figure it will reset selection
                    // 2 select avalible drag cell it will relocat it
                    // Relocat Figure
                    if (CellInDraggableList(boardCell, markCellsAsCanBeDragToList))
                    {
                        RelocateFigureToCell(selectedBoardSell, boardCell);
                        selectedBoardSell.ResetCellMark();
                        ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
                        selectedBoardSell = null;
                        markCellsAsCanBeDragToList = null;
                    }
                    else
                    {
                        selectedBoardSell.ResetCellMark();
                        ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
                        boardCell.MarkAsSelected();
                        selectedBoardSell = boardCell;
                        //List<BoardCellId> draggableCellIds = boardCell.figureOnCell.GetComponent<Figure>().GetCellsOnWhihFigureCanBeDragged1();
                        //List<BoardCellId> draggableCellIds = boardCell.figureOnCell.GetComponent<Figure>().GetCellsOnWhihFigureCanBeDragged();
                        List<BoardCellId> draggableCellIds = boardCell.figureOnCell.GetComponent<IFigure>().GetCellIdsOnWithCanBeDraged(BoardCellsList, boardCell);
                        markCellsAsCanBeDragToList = draggableCellIds;
                        MarkCellsAsCanBeDragTo(draggableCellIds);
                    }
                }
                else
                {
                    boardCell.MarkAsSelected();
                    selectedBoardSell = boardCell;
                    //List<BoardCellId> draggableCellIds = boardCell.figureOnCell.GetComponent<Figure>().GetCellsOnWhihFigureCanBeDragged();
                    List<BoardCellId> draggableCellIds = boardCell.figureOnCell.GetComponent<IFigure>().GetCellIdsOnWithCanBeDraged(BoardCellsList, boardCell);
                    markCellsAsCanBeDragToList = draggableCellIds;
                    MarkCellsAsCanBeDragTo(draggableCellIds);
                }
            }
        }
    }

    private void RelocateFigureToCell(BoardCell selectedBoardSell, BoardCell boardCell)
    {
        Vector3 localNewFigurePosition = boardCell.GetCellPivotPointVector();
        GameObject localFigure = selectedBoardSell.figureOnCell;
        selectedBoardSell.figureOnCell = null;
        boardCell.figureOnCell = localFigure;
        localFigure.transform.position = localNewFigurePosition;
    }

    public void MarkCellsAsCanBeDragTo(List<BoardCellId> draggableCellIds)
    {
        foreach (var cell in BoardCellsList)
        {
            int couresponeCellIndex = draggableCellIds.FindIndex(id => id == cell.CellId);
            if (couresponeCellIndex == -1)
                continue;
            cell.MarkAsCanBeDragger();
        }
    }

    public void ResetCellsMarkAsADefault(List<BoardCellId> draggableCellIds)
    {
        foreach (var cell in BoardCellsList)
        {
            int couresponeCellIndex = draggableCellIds.FindIndex(id => id == cell.CellId);
            if (couresponeCellIndex == -1)
                continue;
            cell.ResetCellMark();
        }
    }

    public bool CellInDraggableList(BoardCell boardCell, List<BoardCellId> cellIdsList)
    {
        int index = cellIdsList.FindIndex(id => id == boardCell.CellId);
        if(index != -1)
        {
            return true;
        }
        return false;
    }



    // #nullable enable
    private GameObject GetFigureByType(FigureType figureType)
    {
        if (figureType == FigureType.pawn)
            return pawn;

        return pawn;
    }
 //   #nullable disable

    public void InitBoard()
    {
        // board initializatin
        // go thrue config and spawn figure set defaul values
        foreach (var cell in BoardCellsList)
        {
            int couresponeCellIndex = defaultBoardStructure.FindIndex(e => e.CellId == cell.CellId);
            if (couresponeCellIndex == -1)
                continue;
            BoardCellStruct couresponeCell = defaultBoardStructure[couresponeCellIndex];
            FigureStructure? cellFigure = couresponeCell.Figure;
            if (cellFigure != null)
            {
                FigureStructure existCellFigure = (FigureStructure)cellFigure;
                GameObject spawnFigure = GetFigureByType(existCellFigure.CurrentType);
                if (spawnFigure != null)
                {
                    cell.SpawnFigure(spawnFigure, existCellFigure);
                }

            }

        }
    }

    public void UpdateBoard()
    {
        // board update
    }


}