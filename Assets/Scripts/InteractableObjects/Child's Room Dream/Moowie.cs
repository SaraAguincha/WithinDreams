using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moowie : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> preInteraction;
    [SerializeField] List<Dialog> startQuest;
    [SerializeField] List<Dialog> duringQuest;
    [SerializeField] List<Dialog> allSet;
    [SerializeField] List<Dialog> completedQuest;

    [SerializeField] Milestones milestones;

    [SerializeField] BooleanValue moowieBookInstance;

    public string interactionMilestone;
    public string startQuestMilestone;
    public string allSetMilestone;
    public string completedQuestMilestone;
    public string unlockedMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        // I wish this could be a switch, but it can't be :(
        if (milestones.getBoolMilestone(completedQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(completedQuest));
        }
        else if (milestones.getBoolMilestone(allSetMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(allSet));
            milestones.addMilestone(completedQuestMilestone, true);
            if (unlockedMilestone != "")
                milestones.addMilestone(unlockedMilestone, true);
        }
        else if (milestones.getBoolMilestone(startQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(duringQuest));
        }
        else if (milestones.getBoolMilestone(interactionMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(startQuest));
            milestones.addMilestone(startQuestMilestone, true);
            moowieBookInstance.setTrue();
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(preInteraction));
        
    }
}
