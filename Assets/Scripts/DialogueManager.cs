using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject dialogueTitleBox;
    [SerializeField] GameObject dialogueImage;
    [SerializeField] Text dialogueText;
    [SerializeField] Text dialogueTitleText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    List<Dialog> dialogues;
    int currentLine = 0;
    int currentDialogue = 0;
    bool isTypingDialogue, isTypingTitle;

    public IEnumerator ShowDialogue(List<Dialog> dialogues)
    {
        yield return new WaitForEndOfFrame();

        if (dialogues.Count == 0 ) 
        {
            Debug.LogWarning("The object that you are trying to interact with does not have any dialog assigned to it.");
            yield break; 
        }

        OnShowDialog?.Invoke();
        this.dialogues = dialogues;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogues[0].Lines[0]));
        ++currentLine;

        if (dialogues[0].Title != "")
        {
            dialogueTitleBox.SetActive(true);
            StartCoroutine(TypeDialogueTitle(dialogues[0].Title));
        }
        if (dialogues[0].ImagePath != "")
        {
            Sprite ImageSprite = Resources.Load<Sprite>(dialogues[0].ImagePath);
            dialogueImage.GetComponent<Image>().sprite = ImageSprite;
            dialogueImage.SetActive(true);
        }
    }

    public Action GetOnCloseDialog()
    {
        return OnCloseDialog;
    }

    public void HandleUpdate(Action onCloseDialog)
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTypingDialogue && !isTypingTitle)
        {
            // This if statement is awful, someday it will be pretty
            if (currentLine < dialogues[currentDialogue].Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogues[currentDialogue].Lines[currentLine]));
                ++currentLine;
            }
            else if (currentDialogue < dialogues.Count - 1)
            {
                ++currentDialogue;
                currentLine = 0;
                StartCoroutine(TypeDialogue(dialogues[currentDialogue].Lines[currentLine]));
                ++currentLine;
                if (dialogues[currentDialogue].Title != "")
                    StartCoroutine(TypeDialogueTitle(dialogues[currentDialogue].Title));
                
                if (dialogues[currentDialogue].ImagePath != "")
                {
                    Sprite ImageSprite = Resources.Load<Sprite>(dialogues[currentDialogue].ImagePath);
                    dialogueImage.GetComponent<Image>().sprite = ImageSprite;
                    dialogueImage.SetActive(true);
                }
                else
                    dialogueImage.SetActive(false);
            }
            else
            {
                currentLine = 0;
                currentDialogue = 0;
                dialogueBox.SetActive(false);
                dialogueTitleBox.SetActive(false);
                dialogueImage.SetActive(false);
                onCloseDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialogue(string line)
    {
        isTypingDialogue = true;

        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTypingDialogue = false;
    }

    public IEnumerator TypeDialogueTitle(string title)
    {
        isTypingTitle = true;

        dialogueTitleText.text = "";
        foreach (var letter in title.ToCharArray())
        {
            dialogueTitleText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTypingTitle = false;
    }
}