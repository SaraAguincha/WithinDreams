using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour, Interactable
{
    // Dialogues
    [SerializeField] List<Dialog> firstInteraction;     //Good mornings, and egg task
    [SerializeField] List<Dialog> duringQuest;          //Reminder of egg task, and tip 
    [SerializeField] List<Dialog> completedQuest;       //Let's eat breakfast -> cutscene
    [SerializeField] List<Dialog> afterQuest;       //Go do your homework for tomorrow


    [SerializeField] Milestones milestones;

    //[SerializeField] BooleanValue dadEggInstance;       //Verifies if the item was aquired
    [SerializeField]  public GameObject dadEggInstance;

    // Milestones names
    public string startQuestMilestone;
    public string completedQuestMilestone;
    public string afterQuestMilestone;


    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(afterQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterQuest));
        }
        else if (milestones.getBoolMilestone(completedQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(completedQuest));
            milestones.addMilestone(afterQuestMilestone, true);
        }
        else if (milestones.getBoolMilestone(startQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(duringQuest));
        }
        else
        {
            dadEggInstance.SetActive(true);
            StartCoroutine(dialogueManager.ShowDialogue(firstInteraction));
            milestones.addMilestone(startQuestMilestone, true);
            

        }

    }
}
