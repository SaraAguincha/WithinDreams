using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
{
    private bool isControls;
    public GameObject controlsPanel;

    public static event Action requestControls;

    // Start is called before the first frame update
    void Start()
    {
        isControls = false;
    }

    public void HandleUpdate()
    {
        if (!isControls)
        {
            Controls();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isControls)
        {
            Uncontrols();
        }
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
        isControls = true;
    }

    public void Uncontrols()
    {
        requestControls?.Invoke();
        controlsPanel.SetActive(false);
        isControls = false;
    }
}
