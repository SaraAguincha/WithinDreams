using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mom : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> firstDialogue;
    [SerializeField] List<Dialog> parkDialogue;
    [SerializeField] List<Dialog> afterMailboxDialogue;
    [SerializeField] List<Dialog> afterFirstDadDialogue;

    [SerializeField] Milestones milestones;

    [SerializeField] public GameObject momInstance;

    [SerializeField] string firstParkVisit;
    [SerializeField] string afterMailbox;

    public string homeworkQuestMilestone;
    public string unlockedMilestone;
    public string unlockedParkMilestone;
    public string unlockedSecondParkMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(unlockedSecondParkMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterFirstDadDialogue));
        }
        else if (milestones.getBoolMilestone(afterMailbox))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterMailboxDialogue));
            milestones.addMilestone(unlockedSecondParkMilestone, true);
        }
        else if (milestones.getBoolMilestone(firstParkVisit))
        {
            StartCoroutine(dialogueManager.ShowDialogue(parkDialogue));
            milestones.addMilestone(unlockedParkMilestone, true);
        }
        else
        {
            StartCoroutine(dialogueManager.ShowDialogue(firstDialogue));

            if (unlockedMilestone != "")
                milestones.addMilestone(unlockedMilestone, true);
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Playground")
        {
            momInstance.SetActive(true);
        }
        else if (milestones.getBoolMilestone(homeworkQuestMilestone))
        {
            momInstance.SetActive(false);
        }
        else
        {
            momInstance.SetActive(true);
        }
    }
}
