using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> questDialogue;


    [SerializeField] Milestones milestones;

    public string questMilestone;
    public string unlockedMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(questMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(questDialogue));
            if (unlockedMilestone != "")
                milestones.addMilestone(unlockedMilestone, true);
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }
}
