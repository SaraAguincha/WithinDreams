using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static event Action onStartCutscene;
    public static event Action onEndCutscene;

    public void HandleUpdate()
    {
        
    }

    public GameObject StartCutscene(GameObject panel)
    {
        onStartCutscene?.Invoke();
        if (panel != null) 
        {
            return Instantiate(panel, Vector3.zero, Quaternion.identity);
        }
        return null;
    }

    public void EndCutscene(GameObject instance, float time)
    {
        onEndCutscene?.Invoke();
        if (instance != null)
        {
            Destroy(instance, time);
        }
    }

    public void EndCutscene(GameObject instance) 
    {
        onEndCutscene?.Invoke();
        if (instance != null)
        {
            Destroy(instance);
        }
    }
}
