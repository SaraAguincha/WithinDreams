using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> questDialogue;
    [SerializeField] List<Dialog> questCompletedDialogue;

    [SerializeField] Milestones milestones;

    public string questMilestone;
    public string unlockedMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(questMilestone))
        {
            if (milestones.getBoolMilestone(unlockedMilestone)) //In case the item was already picked!
                StartCoroutine(dialogueManager.ShowDialogue(questCompletedDialogue));
            else
            {
                StartCoroutine(dialogueManager.ShowDialogue(questDialogue));
                milestones.addMilestone(unlockedMilestone, true);
            }
        }
    }
}
