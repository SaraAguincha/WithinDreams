using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dialogues;

    [SerializeField] Milestones milestones;

    [SerializeField] public GameObject momInstance;

    public string homeworkQuestMilestone;
    public string unlockedMilestone;
    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(dialogues));

        if (unlockedMilestone != "")
            milestones.addMilestone(unlockedMilestone, true);
    }

    public void Update()
    {
        if (milestones.getBoolMilestone(homeworkQuestMilestone))
        {
            momInstance.SetActive(false);
        }
        else
        {
            momInstance.SetActive(true);
        }
    }
}
