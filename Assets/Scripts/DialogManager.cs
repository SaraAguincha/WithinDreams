using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject dialogTitleBox;
    [SerializeField] Text dialogText;
    [SerializeField] Text dialogTitle;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    Dialog dialog;
    int currentLine = 0;
    bool isTypingDialog, isTypingTitle;

    public IEnumerator ShowDialog(Dialog dialog)
    {

        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();
        this.dialog = dialog;
        Debug.Log(dialog == null ? "True" : "False");
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
        if (dialog.Title != "")
        {
            dialogTitleBox.SetActive(true);
            StartCoroutine(TypeDialogTitle(dialog.Title));
        }
    }

    public Action GetOnCloseDialog()
    {
        return OnCloseDialog;
    }

    public void HandleUpdate(Action onCloseDialog)
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTypingDialog && !isTypingTitle)
        {
            ++currentLine;
            Debug.Log(dialog == null ? "True" : "False");
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                dialogTitleBox.SetActive(false);
                onCloseDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        isTypingDialog = true;

        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            Debug.Log(line);
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTypingDialog = false;
    }

    public IEnumerator TypeDialogTitle(string title)
    {
        isTypingTitle = true;

        dialogTitle.text = "";
        foreach (var letter in title.ToCharArray())
        {
            dialogTitle.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTypingTitle = false;
    }
}