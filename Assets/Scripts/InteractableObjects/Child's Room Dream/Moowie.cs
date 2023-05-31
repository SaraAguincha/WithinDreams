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
    [SerializeField] List<Dialog> startHomeworkQuest;
    [SerializeField] List<Dialog> homeworkAllSet;
    [SerializeField] List<Dialog> completedHomeworkQuest;

    [SerializeField] Milestones milestones;

    [SerializeField] BooleanValue moowieBookInstance;
    [SerializeField] BooleanValue moowieFriendInstance;
    [SerializeField] public GameObject meowieInstance;

    public string interactionMilestone;
    public string startQuestMilestone;
    public string allSetMilestone;
    public string completedQuestMilestone;
    public string unlockedInitialMilestone;
    public string startHomeworkQuestMilestone;
    public string duringHomeworkMilestone;
    public string homeworkAllSetMilestone;
    public string completedHomeworkQuestMilestone;

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(completedHomeworkQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(completedHomeworkQuest));
        }
        else if (milestones.getBoolMilestone(homeworkAllSetMilestone))
        {
            meowieInstance.SetActive(true);
            StartCoroutine(dialogueManager.ShowDialogue(homeworkAllSet));
            milestones.addMilestone(completedHomeworkQuestMilestone, true);
        }
        else if (milestones.getBoolMilestone(startHomeworkQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(startHomeworkQuest));
            if (!milestones.getBoolMilestone(duringHomeworkMilestone))
                milestones.addMilestone(duringHomeworkMilestone, true);
            moowieFriendInstance.setTrue();
        }
        else if (milestones.getBoolMilestone(completedQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(completedQuest));
        }
        else if (milestones.getBoolMilestone(allSetMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(allSet));
            milestones.addMilestone(completedQuestMilestone, true);
            if (unlockedInitialMilestone != "")
                milestones.addMilestone(unlockedInitialMilestone, true);
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
