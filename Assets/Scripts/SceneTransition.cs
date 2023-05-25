using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, Interactable
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerInfo;
    public SceneName sceneInfo;

    [SerializeField] List<Dialog> dialogues;

    public static event Action blockPlayerTransition;
    
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
            else
            {
                blockPlayerTransition?.Invoke();
            }
        }
    }

    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(dialogues));
    }
}
