using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;

    public static event Action requestUnpause;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    public void HandleUpdate()
    {
        if (!isPaused) 
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Unpause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void Unpause()
    {
        requestUnpause?.Invoke();
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Time.timeScale = 1f;
    }
}
