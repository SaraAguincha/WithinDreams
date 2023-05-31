using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Pause, Cutscene }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] PauseManager pauseManager;
    [SerializeField] CutsceneManager cutsceneManager;

    Stack<GameState> statesStack;

    private void Start()
    {
        statesStack = new Stack<GameState>();
        statesStack.Push(GameState.FreeRoam);
        DialogueManager.OnShowDialog += () =>
        {
            statesStack.Push(GameState.Dialog);
        };

        DialogueManager.OnCloseDialog += () =>
        {
            statesStack.Pop();
        };

        PlayerController.requestPause += () =>
        {
            statesStack.Push(GameState.Pause);
        };

        PauseManager.requestUnpause += () =>
        {
            statesStack.Pop();
        };

        CutsceneManager.onStartCutscene += () =>
        {
            statesStack.Push(GameState.Cutscene);
        };

        CutsceneManager.onEndCutscene += () =>
        {
            statesStack.Pop();
        };
    }

    private void Update()
    {
        switch (statesStack.Peek())
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
            case GameState.Cutscene:
                cutsceneManager.HandleUpdate();
                break;
            default: 
                statesStack.Clear();
                statesStack.Push(GameState.FreeRoam);
                break;
        }
    }
}