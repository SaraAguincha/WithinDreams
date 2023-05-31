using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> moowieVanishedDialogue;
    [SerializeField] List<Dialog> demoEndDialogue;
    [SerializeField] Milestones milestones;

    [SerializeField] string moowieVanished;
    [SerializeField] string enterMirrorAllowed;
    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(enterMirrorAllowed))
        {
            StartCoroutine(dialogueManager.ShowDialogue(demoEndDialogue));
        }
        else if (milestones.getBoolMilestone(moowieVanished))
        {
            StartCoroutine(dialogueManager.ShowDialogue(moowieVanishedDialogue));
            milestones.addMilestone(enterMirrorAllowed, true);
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }
}
