using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : SingeltonMonoBehavior<Board>
{
    [SerializeField]
    public List<BoardCell> BoardCellsList;
    // figures
    [SerializeField] public GameObject pawn;
    [SerializeField] public GameObject rook;
    [SerializeField] public GameObject bishop;
    [SerializeField] public GameObject queen;
    [SerializeField] public GameObject king;
    [SerializeField] public GameObject knight;
    // figures

    // config for initialization
    public FigureTeamType currentTeamTurn { get; set; }
    private List<BoardCellStruct> defaultBoardStructure;
    // list of state updetes count from default
    private List<TurnChange> ChangeList;
    private Ray lastRay;
    private BoardCell selectedBoardSell = null;
    private CellMarkupStructure markCellsAsCanBeDragToList = new CellMarkupStructure ();
    private Camera mainCamera;


    protected override void Awake()
    {
        base.Awake();
        currentTeamTurn = FigureTeamType.black;
        SetMainCamera();
        defaultBoardStructure = new DefaultBoardConfig().defaultBoardListConfig;
        markCellsAsCanBeDragToList.canBeCapture = new List<BoardCellId>();
        markCellsAsCanBeDragToList.canBeDreggedTo = new List<BoardCellId>();
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
                if ((boardCell != null && boardCell.figureOnCell == null && selectedBoardSell == null) || boardCell == null)
                    return;
                if (selectedBoardSell != null && !isCellMarkapEmpty() && selectedBoardSell.CellId == boardCell.CellId)
                {

                    Debug.Log("1111");
                    selectedBoardSell.ResetCellMark();
                    ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
                    selectedBoardSell = null;
                    ResetMarksCells();
                }
                else if (selectedBoardSell != null && !isCellMarkapEmpty() && selectedBoardSell.CellId != boardCell.CellId)
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
                        ResetMarksCells();
                    }
                    else
                    {
                        if (boardCell.figureOnCell != null)
                        {


                            selectedBoardSell.ResetCellMark();
                            ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
                            boardCell.MarkAsSelected();
                            selectedBoardSell = boardCell;
                            CellMarkupStructure draggableCellIds = boardCell.figureOnCell.GetComponent<IFigure>().GetCellIdsOnWithCanBeDraged(BoardCellsList, boardCell);
                            markCellsAsCanBeDragToList = draggableCellIds;
                            MarkCellsByType(draggableCellIds);

                        }
                    }
                }
                else
                {
                    if (boardCell.figureOnCell.GetComponent<Figure>().TeamType == currentTeamTurn)
                    {
                        boardCell.MarkAsSelected();
                        selectedBoardSell = boardCell;
                        CellMarkupStructure draggableCellIds = boardCell.figureOnCell.GetComponent<IFigure>().GetCellIdsOnWithCanBeDraged(BoardCellsList, boardCell);
                        markCellsAsCanBeDragToList = draggableCellIds;
                        MarkCellsByType(draggableCellIds);
                    }
                }
            }
        }
    }

    private bool isCellMarkapEmpty()
    {
        return markCellsAsCanBeDragToList.canBeCapture.Count == 0 && markCellsAsCanBeDragToList.canBeDreggedTo.Count == 0;

    }

    private void ResetMarksCells()
    {
        markCellsAsCanBeDragToList.canBeCapture = new List<BoardCellId>();
        markCellsAsCanBeDragToList.canBeDreggedTo = new List<BoardCellId>();
    }

    private void RelocateFigureToCell(BoardCell selectedBoardSell, BoardCell boardCell)
    {
        Vector3 localNewFigurePosition = boardCell.GetCellPivotPointVector();
        GameObject localFigure = selectedBoardSell.figureOnCell;
        selectedBoardSell.figureOnCell = null;
        GameObject figureOnCaptureCell = boardCell.figureOnCell;
        if(figureOnCaptureCell != null)
        {
            Destroy(figureOnCaptureCell);
        }
        boardCell.figureOnCell = localFigure;
        localFigure.transform.position = localNewFigurePosition;
        // Call End of turn handler
        EventHandler.CallSwithToOtherPlayerTeamEvent(currentTeamTurn == FigureTeamType.white ? FigureTeamType.black :FigureTeamType.white);
    }

    public void SetMainCamera()
    {
        mainCamera = Camera.main;
    }

    public void MarkCellsByType(CellMarkupStructure CellMarkup)
    {
        foreach (var cell in BoardCellsList)
        {
            int canBeDreggedCouresponeCellIndex = CellMarkup.canBeDreggedTo.FindIndex(id => id == cell.CellId);
            int canBeCapturCouresponeCellIndex = CellMarkup.canBeCapture.FindIndex(id => id == cell.CellId);
            if (canBeDreggedCouresponeCellIndex != -1)
                cell.MarkAsCanBeDragger();
            if (canBeCapturCouresponeCellIndex != -1)
                cell.MarkAsCanBeCapture();
        }
    }

    public void ResetCellsMarkAsADefault(CellMarkupStructure CellMarkup)
    {
        foreach (var cell in BoardCellsList)
        {
            int canBeDreggedCouresponeCellIndex = CellMarkup.canBeDreggedTo.FindIndex(id => id == cell.CellId);
            int canBeCapturCouresponeCellIndex = CellMarkup.canBeCapture.FindIndex(id => id == cell.CellId);
            if (canBeDreggedCouresponeCellIndex != -1)
                cell.ResetCellMark();
            if (canBeCapturCouresponeCellIndex != -1)
                cell.ResetCellMark();
        }
    }

    public bool CellInDraggableList(BoardCell boardCell, CellMarkupStructure CellMarkup)
    {
        int canBeDreggedCouresponeCellIndex = CellMarkup.canBeDreggedTo.FindIndex(id => id == boardCell.CellId);
        int canBeCapturCouresponeCellIndex = CellMarkup.canBeCapture.FindIndex(id => id == boardCell.CellId);
        if (canBeDreggedCouresponeCellIndex != -1 || canBeCapturCouresponeCellIndex != -1)
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
        if (figureType == FigureType.rook)
            return rook;
        if (figureType == FigureType.queen)
            return queen;
        if (figureType == FigureType.king)
            return king;
        if (figureType == FigureType.knight)
            return knight;
        if (figureType == FigureType.bishop)
            return bishop;
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