using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoowieAndFlopsyPark : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> afterFirstMomTalkDialogue;
    [SerializeField] List<Dialog> afterDadDialogue;
    [SerializeField] Milestones milestones;

    [SerializeField] string afterFirstMomTalk;
    [SerializeField] string afterDadTalk;
    [SerializeField] string unlockedMomQuestMilestone;

    [SerializeField] public GameObject dad;

    private void Awake()
    {
        if (!milestones.getBoolMilestone(afterFirstMomTalk))
            dad.SetActive(false);
    }

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(afterDadTalk))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterDadDialogue));
        }
        else if (milestones.getBoolMilestone(afterFirstMomTalk))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterFirstMomTalkDialogue));
            milestones.addMilestone(unlockedMomQuestMilestone, true);
            dad.SetActive(true);

        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }
}
