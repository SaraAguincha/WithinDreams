using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerInfo;

    // On triggering, some evaluations should be made. P.e In which state the game is
    // Milestones
    // TODO: fix camera weird movement when transitioning


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInfo.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
