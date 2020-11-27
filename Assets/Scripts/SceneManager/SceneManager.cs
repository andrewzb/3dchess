using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI playerTitle;
    [SerializeField]
    public GameObject board;
    //[SerializeField]
    //public GameObject player1Camera;
    //[SerializeField]
    //public GameObject player2Camera;
    [SerializeField]
    public RectTransform rewindListItemContainer;
    [SerializeField]
    public RectTransform rewindListItemPrefab;
    private List<TurnChange> changeList = new List<TurnChange>();

    //AudioListener player1CameraAudioListner;
    //AudioListener player2CameraAudioListner;

    private Board gameBoard;

    private void Start()
    {
        //player1CameraAudioListner = player1Camera.GetComponent<AudioListener>();
        //player2CameraAudioListner = player2Camera.GetComponent<AudioListener>();
        gameBoard = board.GetComponent<Board>();
    }

    private void OnEnable()
    {
        EventHandler.SwithToOtherPlayerTeamEvent += SwithToOtherPlayerManager;
        EventHandler.WriteTurnChangesEvent += WriteTurnChangeInList;
        EventHandler.EmitGameEvent += DisplayBoardEvent;
    }

    private void OnDisable()
    {
        EventHandler.SwithToOtherPlayerTeamEvent -= SwithToOtherPlayerManager;
        EventHandler.WriteTurnChangesEvent -= WriteTurnChangeInList;
        EventHandler.EmitGameEvent -= DisplayBoardEvent;
    }


    private void SwithToOtherPlayerManager(FigureTeamType figureTeamType)
    {
        if (figureTeamType == FigureTeamType.black)
        {
            playerTitle.text = "Player 1 (black)";
            //ToggleCamera(player2Camera, player2CameraAudioListner, false);
            //ToggleCamera(player1Camera, player1CameraAudioListner, true);
        }
        else
        {
            playerTitle.text = "Player 2 (white)";
            //ToggleCamera(player2Camera, player2CameraAudioListner, true);
            //ToggleCamera(player1Camera, player1CameraAudioListner, false);

        }
        //gameBoard.SetMainCamera();
        gameBoard.currentTeamTurn = figureTeamType;
    }

    private void WriteTurnChangeInList(TurnChange currentTurnChange)
    {
        changeList.Add(currentTurnChange);
        AddTurnChangeToRewindSidebar(currentTurnChange);
    }


    private void AddTurnChangeToRewindSidebar(TurnChange currentTurnChange)
    {
        //rewindListItemContainer
        GameObject instance = GameObject.Instantiate(rewindListItemPrefab.gameObject) as GameObject;
        instance.transform.SetParent(rewindListItemContainer, false);
        InitializeItemView(instance, currentTurnChange);

    }

    private void InitializeItemView(GameObject instance, TurnChange currentTurnChange)
    {
        RewindListItem Listitem = instance.GetComponent<RewindListItem>();
        Listitem
            .FluentResetValues(changeList.Count, currentTurnChange.FromCellWithId, currentTurnChange.ToCellWithId)
            .Init();

        return;
    }

    private void ToggleCamera(GameObject camera, AudioListener audioListener, bool isActive)
    {
        camera.SetActive(isActive);
        audioListener.enabled = isActive;
    }

    private void DisplayBoardEvent(GameEvent gameEvent)
    {
        Debug.Log(gameEvent);
    }

}
