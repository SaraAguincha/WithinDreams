using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstScene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void NewGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
