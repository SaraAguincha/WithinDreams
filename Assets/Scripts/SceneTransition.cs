using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerInfo;
    public SceneName sceneInfo;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInfo.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            sceneInfo.setSceneName(sceneToLoad);
        }
    }
}
