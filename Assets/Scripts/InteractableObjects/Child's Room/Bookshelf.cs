using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> questDialogue;
    [SerializeField] List<Dialog> questCompletedDialogue;


    [SerializeField] Milestones milestones;

    public string questMilestone;
    public string unlockedMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(questMilestone))
        {
            if (milestones.getBoolMilestone(unlockedMilestone))
                StartCoroutine(dialogueManager.ShowDialogue(questCompletedDialogue));
            else
            {
                StartCoroutine(dialogueManager.ShowDialogue(questDialogue));
                milestones.addMilestone(unlockedMilestone, true);
            }
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }
}
