using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularInteractable : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dialogues;
    [SerializeField] Milestones milestones;
    public string unlockedMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(dialogues));
        
        if (unlockedMilestone != "")
            milestones.addMilestone(unlockedMilestone, true);
    }
}
