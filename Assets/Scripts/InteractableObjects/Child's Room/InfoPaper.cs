using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPaper : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dialogues;
    [SerializeField] Milestones milestones;
    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(dialogues));
        milestones.addMilestone("dreamWorldUnlocked", true);
    }
}