using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] CutsceneManager cutsceneManager;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject dialogueTitleBox;
    [SerializeField] GameObject dialogueImage;
    [SerializeField] Text dialogueText;
    [SerializeField] Text dialogueTitleText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] AudioSource dialogueAudio;

    public static event Action OnShowDialog;
    public static event Action OnCloseDialog;

    List<Dialog> dialogues;
    int currentLine = 0;
    int currentDialogue = 0;
    bool isTypingDialogue, isTypingTitle;

    public CutsceneManager GetCutsceneManager() { return cutsceneManager; }

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

    public void HandleUpdate()
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
                OnCloseDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialogue(string line)
    {
        isTypingDialogue = true;

        dialogueText.text = "";
        dialogueAudio.Play();
        int auxLettersPerSecond = lettersPerSecond;
        bool afterFirst = false;
        foreach (var letter in line.ToCharArray())
        {   if (Input.GetKeyDown(KeyCode.Z) && afterFirst)
                lettersPerSecond = lettersPerSecond * 100;
            dialogueText.text += letter;
            afterFirst = true;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        lettersPerSecond = auxLettersPerSecond;
        dialogueAudio.Stop();
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