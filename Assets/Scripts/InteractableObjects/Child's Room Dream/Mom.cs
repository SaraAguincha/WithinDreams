using System;
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

    private void Awake()
    {
        if (milestones.getBoolMilestone(homeworkQuestMilestone))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(firstDialogue));

        if (unlockedMilestone != "")
            milestones.addMilestone(unlockedMilestone, true);
    }
}
