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
    private Ray lastRay;
    private BoardCell selectedBoardSell = null;
    private CellMarkupStructure markCellsAsCanBeDragToList = new CellMarkupStructure ();
    private Camera mainCamera;
    // kings Refs
    private BoardCell blackKingCell;
    private BoardCell whiteKingCell;


    protected override void Awake()
    {
        base.Awake();
        currentTeamTurn = FigureTeamType.black;
        SetMainCamera();
        defaultBoardStructure = new DefaultBoardConfig().defaultBoardListConfig;
        markCellsAsCanBeDragToList.Init();
    }

    private void Start()
    {
        InitBoard();
    }

    private bool IsCellMarkapEmpty()
    {
        return markCellsAsCanBeDragToList.canBeCapture.Count == 0 && markCellsAsCanBeDragToList.canBeDreggedTo.Count == 0 && markCellsAsCanBeDragToList.canBeCastle.Count == 0;
    }

    private bool IsNoCellHitOrAnableMoveTo(BoardCell boardCell)
    {
        return (boardCell != null && boardCell.figureOnCell == null && selectedBoardSell == null) || boardCell == null;
    }

    private bool TryToDragToCurrentPosition(BoardCell boardCell)
    {
        return selectedBoardSell != null && !IsCellMarkapEmpty() && selectedBoardSell.CellId == boardCell.CellId;
    }   

    private bool TryToDragToNotCurrentPosition(BoardCell boardCell)
    {
        return selectedBoardSell != null && !IsCellMarkapEmpty() && selectedBoardSell.CellId != boardCell.CellId;
    }

    public bool CanBeMoveTo(BoardCell boardCell, CellMarkupStructure cellMarkup)
    {
        int canBeDreggedCouresponeCellIndex = cellMarkup.canBeDreggedTo.FindIndex(id => id == boardCell.CellId);
        int canBeCapturCouresponeCellIndex = cellMarkup.canBeCapture.FindIndex(id => id == boardCell.CellId);
        int canBeCastleCouresponeCellIndex = cellMarkup.canBeCastle.FindIndex(castleStruct => castleStruct.KingTo == boardCell.CellId);
        int isInDangerCouresponeCellIndex = cellMarkup.inDanger.FindIndex(id => id == boardCell.CellId);

        if (canBeDreggedCouresponeCellIndex != -1 || canBeCapturCouresponeCellIndex != -1 || canBeCastleCouresponeCellIndex != -1)
        {
            if (isInDangerCouresponeCellIndex == -1)
            {
                return true;
            }
        }
        return false;
    }

    public bool isBelongToCurrentPlayer(BoardCell boardCell)
    {
        return boardCell.figureOnCell.GetComponent<Figure>().TeamType == currentTeamTurn;
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
                if (IsNoCellHitOrAnableMoveTo(boardCell))
                    return;
                if (TryToDragToCurrentPosition(boardCell))
                {
                    selectedBoardSell.ResetCellMark();
                    ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
                    selectedBoardSell = null;
                    ResetMarksCells();
                }
                else if (TryToDragToNotCurrentPosition(boardCell))
                {
                    // if you with selected your's figure
                    // 1 select your figure it will reset selection
                    // 2 select avalible drag cell it will relocat it
                    // Relocat Figure
                    // TURN
                    if (CanBeMoveTo(boardCell, markCellsAsCanBeDragToList))
                    {
                        // ToDo if moved figure is king reset it cell in refs
                        if (isCastle(boardCell, markCellsAsCanBeDragToList))
                        {
                            MakeCastle(selectedBoardSell, boardCell);
                        }
                        else
                        {
                            RelocateFigureToCell(selectedBoardSell, boardCell);
                        }
                            ResetCellAfterTurn();
                        // MakeCastle && RelocateFigureToCell emit end Of turn
                        GameEvent gameEvent = getEventAfterTurn();
                        if (gameEvent != GameEvent.none)
                        {
                            EventHandler.CallGameEvent(gameEvent);
                        }

                    }
                    // TURN
                    else
                    {

                        // Reselect Figure of your Team
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
                    if (isBelongToCurrentPlayer(boardCell))
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

    private void ResetCellAfterTurn()
    {
        selectedBoardSell.ResetCellMark();
        ResetCellsMarkAsADefault(markCellsAsCanBeDragToList);
        selectedBoardSell = null;
        ResetMarksCells();
    }

    private GameEvent getEventAfterTurn()
    {
        BoardCell currentKingCell;
        if (currentTeamTurn == FigureTeamType.white)
        {
            currentKingCell = whiteKingCell;
        }
        else
        {
            currentKingCell = blackKingCell;
        }
        King king = currentKingCell.figureOnCell.GetComponent<King>();
        bool isCheck = king.isCellInDanger(BoardCellsList, currentKingCell.GetComponent<BoardCell>().CellId);
        // chen fo unability to move
        List<BoardCellId> kingInDangerCellidList = king.GetDengerCellSTructure(BoardCellsList, currentKingCell, false);
        bool stalemate = kingInDangerCellidList.Count == 8;

        if(stalemate && isCheck)
        {
            return GameEvent.checkmate;
        }
        if(stalemate)
        {
            return GameEvent.stalemate;
        }        
        if(isCheck)
        {
            return GameEvent.check;
        }

        return GameEvent.none;

    }

    /*private bool isTheKingInDanger(BoardCell boardCell)
    {
        int index = markCellsAsCanBeDragToList.inDanger.FindIndex(id => id == boardCell.CellId);
        if (index != -1)
        {
            return true;
        }
        return false;
    }*/

    private void ResetMarksCells()
    {
        markCellsAsCanBeDragToList.Init();
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
        localFigure.GetComponent<IFigure>().AfterMove();
        Figure figure = localFigure.GetComponent<Figure>();
        if (figure.CurrentType == FigureType.king)
        {
            if (figure.TeamType == FigureTeamType.white)
            {
                whiteKingCell = boardCell;
            }
            else
            {
                blackKingCell = boardCell;
            }
        }
        // Call End of turn handler
        EventHandler.CallSwithToOtherPlayerTeamEvent(currentTeamTurn == FigureTeamType.white ? FigureTeamType.black :FigureTeamType.white);
        WriteTurnChanges(selectedBoardSell, boardCell);
        //CalculateKingDanger();
    }

    /*private void CalculateKingDanger()
    {
        CellMarkupStructure draggableCellIds = blackKingCell.figureOnCell.GetComponent<King>().GetCellIdsOnWithCanBeDraged(BoardCellsList, blackKingCell);
    }*/

    private bool isCastle(BoardCell boardCell, CellMarkupStructure cellMarkup)
    {
        int canBeCastleCouresponeCellIndex = cellMarkup.canBeCastle.FindIndex(castleStruct => castleStruct.KingTo == boardCell.CellId);
        if (canBeCastleCouresponeCellIndex != -1)
        {
            return true;
        }
        return false;
    }

    private void MakeCastle(BoardCell kingFromCell, BoardCell kingToCell)
    {
        int courespondetIndex = markCellsAsCanBeDragToList.canBeCastle.FindIndex(castleStructure => castleStructure.KingTo == kingToCell.CellId);
        if (courespondetIndex != -1)
        {
            BoardCell rookFromCell = GetBoardCellByIndex(markCellsAsCanBeDragToList.canBeCastle[courespondetIndex].RookFrom);
            BoardCell rookToCell = GetBoardCellByIndex(markCellsAsCanBeDragToList.canBeCastle[courespondetIndex].RookTo);
            GameObject rook = rookFromCell.figureOnCell;
            GameObject king = kingFromCell.figureOnCell;
            if (rook != null && king != null)
            {
                Vector3 localNewKingPosition = kingToCell.GetCellPivotPointVector();
                Vector3 localNewRookPosition = rookToCell.GetCellPivotPointVector();
                kingFromCell.figureOnCell = null;
                rookFromCell.figureOnCell = null;
                rookToCell.figureOnCell = rook;
                kingToCell.figureOnCell = king;
                rook.transform.position = localNewRookPosition;
                king.transform.position = localNewKingPosition;
                rook.GetComponent<IFigure>().AfterMove();
                king.GetComponent<IFigure>().AfterMove();
                Figure localKing = king.GetComponent<Figure>();
                if (localKing.TeamType == FigureTeamType.white)
                {
                    whiteKingCell = kingToCell;
                }
                else
                {
                    blackKingCell = kingToCell;
                }
                EventHandler.CallSwithToOtherPlayerTeamEvent(currentTeamTurn == FigureTeamType.white ? FigureTeamType.black : FigureTeamType.white);
                WriteTurnChanges(kingFromCell, kingToCell);
            }
        }
    }

    private BoardCell GetBoardCellByIndex(BoardCellId boardCellId)
    {
        int index = BoardCellsList.FindIndex(cell => cell.CellId == boardCellId);
        return BoardCellsList[index];
    }


    public void SetMainCamera()
    {
        mainCamera = Camera.main;
    }

    public void MarkCellsByType(CellMarkupStructure cellMarkup)
    {
        foreach (var cell in BoardCellsList)
        {
            int canBeDreggedCouresponeCellIndex = cellMarkup.canBeDreggedTo.FindIndex(id => id == cell.CellId);
            int canBeCapturCouresponeCellIndex = cellMarkup.canBeCapture.FindIndex(id => id == cell.CellId);
            int canBeCastleCouresponeCellIndex = cellMarkup.canBeCastle.FindIndex(castleStruct => castleStruct.KingTo == cell.CellId);
            int isInDanger = cellMarkup.inDanger.FindIndex(id => id == cell.CellId);
            if (canBeDreggedCouresponeCellIndex != -1)
                cell.MarkAsCanBeDragger();
            if (canBeCapturCouresponeCellIndex != -1)
                cell.MarkAsCanBeCapture();
            if (canBeCastleCouresponeCellIndex != -1)
                cell.MarkAsCastled();           
            if (isInDanger != -1)
                cell.MarkAsInDanger();
        }
    }

    public void ResetCellsMarkAsADefault(CellMarkupStructure cellMarkup)
    {
        foreach (var cell in BoardCellsList)
        {
            int canBeDreggedCouresponeCellIndex = cellMarkup.canBeDreggedTo.FindIndex(id => id == cell.CellId);
            int canBeCapturCouresponeCellIndex = cellMarkup.canBeCapture.FindIndex(id => id == cell.CellId);
            int canBeCastleCouresponeCellIndex = cellMarkup.canBeCastle.FindIndex(castleStruct => castleStruct.KingTo == cell.CellId);
            int isInDanger = cellMarkup.inDanger.FindIndex(id => id == cell.CellId);
            if (canBeDreggedCouresponeCellIndex != -1)
                cell.ResetCellMark();
            if (canBeCapturCouresponeCellIndex != -1)
                cell.ResetCellMark();
            if (canBeCastleCouresponeCellIndex != -1)
                cell.ResetCellMark();
            if (isInDanger != -1)
                cell.ResetCellMark();
        }
    }

    private void InitKingsRefs()
    {
        BoardCell blackKingCell = GetBoardCellByIndex(BoardCellId.E8);
        BoardCell whiteKingCell = GetBoardCellByIndex(BoardCellId.E1);
        if(blackKingCell != null && whiteKingCell != null)
        {
            this.blackKingCell = blackKingCell;
            this.whiteKingCell = whiteKingCell;
        }
    }

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
        InitKingsRefs();
    }

    private void WriteTurnChanges(BoardCell fromBoardCell, BoardCell toBoardCell)
    {
        TurnChange currentTurnChange = new TurnChange(fromBoardCell.CellId, toBoardCell.CellId);
        EventHandler.CallWriteTurnChangesEvent(currentTurnChange);
    }

    public void UpdateBoard()
    {
        // board update
    }
}