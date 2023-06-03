using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstScene;
    public string mainMenuScene;
    public string controlsScene;
    public SceneName loadScene;

    public void NewGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadScene.getSceneName());
    }

    public void Controls()
    {
        SceneManager.LoadScene(controlsScene);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
