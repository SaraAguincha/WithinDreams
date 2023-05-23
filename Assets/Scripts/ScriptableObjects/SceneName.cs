using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneName : ScriptableObject
{
    public string sceneName;

    public void setSceneName(string newScene) { sceneName = newScene;}

    public string getSceneName() { return sceneName;}
}
