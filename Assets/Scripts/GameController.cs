using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Pause }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] PauseManager pauseManager;

    GameState state;

    private void Start()
    {
        DialogueManager.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogueManager.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };

        PlayerController.requestPause += () =>
        {
            state = GameState.Pause;
        };

        PauseManager.requestUnpause += () =>
        {
            state = GameState.FreeRoam;
        };
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.FreeRoam:
                playerController.HandleUpdate();
                break;
            case GameState.Dialog:
                dialogueManager.HandleUpdate();
                break;
            case GameState.Pause:
                pauseManager.HandleUpdate();
                break;
            default: 
                state = GameState.FreeRoam;
                break;
        }
    }
}