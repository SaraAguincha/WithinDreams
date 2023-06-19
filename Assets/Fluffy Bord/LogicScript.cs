using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public AudioSource dingSFX;
    private bool isGameOverActive;

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        dingSFX.Play();
    }

    public void restartGame()
    {
        isGameOverActive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        isGameOverActive = true;
        gameOverScreen.SetActive(true);
    }

    public void exitGame()
    {
        SceneManager.LoadScene("Fluffy Bord Menu");
    }

    private void Update()
    {
        if (isGameOverActive && Input.GetKeyDown(KeyCode.Return))
        {
            restartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameOverActive = false;
            exitGame();
        }
    }
}
