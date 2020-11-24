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
    [SerializeField]
    public GameObject player1Camera;
    [SerializeField]
    public GameObject player2Camera;

    AudioListener player1CameraAudioListner;
    AudioListener player2CameraAudioListner;

    private Board gameBoard;

    private void Start()
    {
        player1CameraAudioListner = player1Camera.GetComponent<AudioListener>();
        player2CameraAudioListner = player2Camera.GetComponent<AudioListener>();
        gameBoard = board.GetComponent<Board>();
    }

    private void OnEnable()
    {
        EventHandler.SwithToOtherPlayerTeam += SwithToOtherPlayerManager;
    }

    private void OnDisable()
    {
        EventHandler.SwithToOtherPlayerTeam -= SwithToOtherPlayerManager;
    }


    private void SwithToOtherPlayerManager(FigureTeamType figureTeamType)
    {
        if (figureTeamType == FigureTeamType.black)
        {
            playerTitle.text = "Player 1 (black)";
            ToggleCamera(player2Camera, player2CameraAudioListner, false);
            ToggleCamera(player1Camera, player1CameraAudioListner, true);
        }
        else
        {
            playerTitle.text = "Player 2 (white)";
            ToggleCamera(player2Camera, player2CameraAudioListner, true);
            ToggleCamera(player1Camera, player1CameraAudioListner, false);

        }
        gameBoard.SetMainCamera();
        gameBoard.currentTeamTurn = figureTeamType;
    }

    private void ToggleCamera(GameObject camera, AudioListener audioListener, bool isActive)
    {
        camera.SetActive(isActive);
        audioListener.enabled = isActive;
    }

}
