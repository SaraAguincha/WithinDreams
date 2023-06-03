using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPaper : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dialogues;
    [SerializeField] Milestones milestones;
    public string unlockedMilestone;

    [SerializeField] public GameObject arrowInstance;

    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(dialogues));

        if (unlockedMilestone != "")
        {
            arrowInstance.SetActive(false);
            milestones.addMilestone(unlockedMilestone, true);
        }
    }
}
