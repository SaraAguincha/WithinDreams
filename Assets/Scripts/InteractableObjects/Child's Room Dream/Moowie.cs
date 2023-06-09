using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Moowie : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> preInteraction;
    [SerializeField] List<Dialog> startQuest;
    [SerializeField] List<Dialog> duringQuest;
    [SerializeField] List<Dialog> allSet;
    [SerializeField] List<Dialog> completedQuest;
    [SerializeField] List<Dialog> startHomeworkQuest;
    [SerializeField] List<Dialog> duringHomeworkQuest;
    [SerializeField] List<Dialog> homeworkAllSet;
    [SerializeField] List<Dialog> cutsceneDialogue;
    [SerializeField] List<Dialog> afterFirstParkQuest;
    [SerializeField] List<Dialog> firstParkCompleted;

    [SerializeField] Milestones milestones;

    [SerializeField] BooleanValue moowieBookInstance;
    [SerializeField] BooleanValue flopsyInstance;
    [SerializeField] public GameObject flopsy;
    [SerializeField] public GameObject moowieInstance;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    private GameObject fadeInPanelInstance;
    private GameObject fadeOutPanelInstance;

    private DialogueManager selfDialogueManager;

    private bool dialogueActive = false;
    private bool cutsceneAllowed = false;

    public string interactionMilestone;
    public string startQuestMilestone;
    public string allSetMilestone;
    public string completedQuestMilestone;
    public string unlockedInitialMilestone;
    public string startHomeworkQuestMilestone;
    public string duringHomeworkMilestone;
    public string homeworkAllSetMilestone;
    public string completedHomeworkQuestMilestone;
    public string afterFirstParkMilestone;
    public string firstParkCompletedMilestone;

    private void Awake()
    {
        DialogueManager.OnCloseDialog += mirrorTrigger;

        if (milestones.getBoolMilestone(afterFirstParkMilestone))
        {
            moowieInstance.SetActive(true);
            flopsy.SetActive(true);
        }
        else if (milestones.getBoolMilestone(completedHomeworkQuestMilestone))
            moowieInstance.SetActive(false);
        else
            moowieInstance.SetActive(true);
    }

    private void OnDestroy()
    {
        DialogueManager.OnCloseDialog -= mirrorTrigger;
    }

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone("afterLunchQuestCompleted"))
        {
            StartCoroutine(dialogueManager.ShowDialogue(preInteraction));
        }
        else if (milestones.getBoolMilestone(firstParkCompletedMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(firstParkCompleted));
        }
        else if (milestones.getBoolMilestone(afterFirstParkMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterFirstParkQuest));
            milestones.addMilestone(firstParkCompletedMilestone, true);
        }
        else if (milestones.getBoolMilestone(homeworkAllSetMilestone))
        {
            flopsy.SetActive(true);
            cutsceneAllowed = true;
            selfDialogueManager = dialogueManager;
            StartCoroutine(dialogueManager.ShowDialogue(homeworkAllSet));
            milestones.addMilestone(completedHomeworkQuestMilestone, true);
        }
        else if (milestones.getBoolMilestone(duringHomeworkMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(duringHomeworkQuest));
        }
        else if (milestones.getBoolMilestone(startHomeworkQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(startHomeworkQuest));
            if (!milestones.getBoolMilestone(duringHomeworkMilestone))
                milestones.addMilestone(duringHomeworkMilestone, true);
            flopsyInstance.setTrue();
        }
        else if (milestones.getBoolMilestone(completedQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(completedQuest));
        }
        else if (milestones.getBoolMilestone(allSetMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(allSet));
            milestones.addMilestone(completedQuestMilestone, true);
            if (unlockedInitialMilestone != "")
                milestones.addMilestone(unlockedInitialMilestone, true);
        }
        else if (milestones.getBoolMilestone(startQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(duringQuest));
        }
        else if (milestones.getBoolMilestone(interactionMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(startQuest));
            milestones.addMilestone(startQuestMilestone, true);
            moowieBookInstance.setTrue();
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(preInteraction));
    }

    public void mirrorTrigger()
    {
        // Check if its present in the eating dialogue
        if (dialogueActive)
            StartCoroutine(mirrorCutsceneEnd(selfDialogueManager));
        // Check if its present in the cutscene
        else if (cutsceneAllowed)
            StartCoroutine(mirrorCutsceneStart(selfDialogueManager));
    }

    IEnumerator mirrorCutsceneStart(DialogueManager dialogueManager)
    {
        CutsceneManager cutsceneManager = dialogueManager.GetCutsceneManager();
        // Fade in Panel
        fadeInPanelInstance = cutsceneManager.StartCutscene(fadeInPanel);
        // Eating Dialogue
        dialogueActive = true;
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(dialogueManager.ShowDialogue(cutsceneDialogue));
    }

    IEnumerator mirrorCutsceneEnd(DialogueManager dialogueManager)
    {
        CutsceneManager cutsceneManager = dialogueManager.GetCutsceneManager();
        dialogueActive = false;

        // Transition from fade in to fade out
        fadeOutPanelInstance = cutsceneManager.StartCutscene(fadeOutPanel);
        cutsceneManager.EndCutscene(fadeInPanelInstance);
        cutsceneManager.EndCutscene(fadeOutPanelInstance, 3);

        flopsyInstance.setFalse();
        flopsy.SetActive(false);
        cutsceneAllowed = false;
        moowieInstance.SetActive(false);

        yield return new WaitForSeconds(0.5f);
    }

}
