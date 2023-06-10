using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mom : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> firstDialogue;
    [SerializeField] List<Dialog> parkDialogue;

    [SerializeField] Milestones milestones;

    [SerializeField] public GameObject momInstance;

    [SerializeField] string firstParkVisit;

    public string homeworkQuestMilestone;
    public string unlockedMilestone;
    public string unlockedParkMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(firstParkVisit))
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
