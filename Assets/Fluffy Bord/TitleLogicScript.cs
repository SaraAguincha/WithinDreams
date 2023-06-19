using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleLogicScript : MonoBehaviour
{

    public SceneName sceneInfo;

    public void startGame()
    {
        SceneManager.LoadScene("Fluffy Bord");
    }

    public void exitGame()
    {
        sceneInfo.setSceneName("House");
        SceneManager.LoadScene("House");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitGame();
        }
    }
}
