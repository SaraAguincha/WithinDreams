using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boris : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> startGameQuest;
    [SerializeField] List<Dialog> duringGameQuest;
    [SerializeField] List<Dialog> afterFluffyBordQuest;

    [SerializeField] Milestones milestones;

    public string afterLunchQuestMilestone;
    public string duringGameQuestMilestone;
    public string afterFluffyBordMilestone;
    public string secondMirrorEnterMilestone;

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(afterFluffyBordMilestone))
        {
            milestones.addMilestone(secondMirrorEnterMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(afterFluffyBordQuest));
        }
        else if (milestones.getBoolMilestone(duringGameQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(duringGameQuest));
        }
        else if (milestones.getBoolMilestone(afterLunchQuestMilestone))
        {
            milestones.addMilestone(duringGameQuestMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(startGameQuest));
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }
}
