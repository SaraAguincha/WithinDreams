using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> afterFirstMomTalkDialogue;
    [SerializeField] Milestones milestones;

    [SerializeField] string afterFirstMomTalk;
    [SerializeField] string unlockedMomQuestMilestone;

    [SerializeField] public GameObject dad;

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(afterFirstMomTalk))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterFirstMomTalkDialogue));
            milestones.addMilestone(unlockedMomQuestMilestone, true);
            dad.SetActive(true);

        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }
}
