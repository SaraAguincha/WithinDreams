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
    
    [SerializeField] Milestones milestones;
    public string neededMilestone;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
           if (milestones.getBoolMilestone(neededMilestone) || neededMilestone == "")
           {
                playerInfo.initialValue = playerPosition;
                SceneManager.LoadScene(sceneToLoad);
                sceneInfo.setSceneName(sceneToLoad);
            }
            
        }
    }
}
