using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPaper : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dialogues;
    public void Interact(DialogueManager dialogueManager)
    {
        StartCoroutine(dialogueManager.ShowDialogue(dialogues));
    }
}