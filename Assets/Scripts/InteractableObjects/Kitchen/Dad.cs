using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour, Interactable
{
    // Dialogues
    [SerializeField] List<Dialog> firstInteraction;             //Good mornings, and egg task
    [SerializeField] List<Dialog> duringQuest;                  //Reminder of egg task, and tip 
    [SerializeField] List<Dialog> completedQuestBefore;         //Let's eat breakfast -> cutscene
    [SerializeField] List<Dialog> completedQuestAfter;          //Let's eat breakfast -> cutscene
    [SerializeField] List<Dialog> eating;
    [SerializeField] List<Dialog> afterQuest;                   //Go do your homework for tomorrow
    [SerializeField] List<Dialog> flopsyQuestHelp;
    [SerializeField] List<Dialog> flopsyQuestHelpAfter;
    [SerializeField] List<Dialog> firstParkCompleted;           //Ended first memory, lunch time, start lunch quest
    [SerializeField] List<Dialog> dadPlateLunchQuest;
    [SerializeField] List<Dialog> girlPlateLunchQuest;
    [SerializeField] List<Dialog> lunchQuestComplete;
    [SerializeField] List<Dialog> afterLunchQuest;



    [SerializeField] Milestones milestones;

    //[SerializeField] BooleanValue dadEggInstance;       //Verifies if the item was aquired
    [SerializeField] public GameObject dadEggInstance;

    // Milestones names
    public string startQuestMilestone;
    public string completedQuestMilestone;
    public string afterQuestMilestone;
    public string disableArrowMilestone;
    public string flopsyQuestMilestone;
    public string afterFlopsyQuestMilestone;
    public string firstParkCompletedMilestone;
    public string dadPlateLunchQuestMilestone;
    public string dadPlateDoneLunchQuestMilestone;
    public string girlPlateLunchQuestMilestone;
    public string girlPlateDoneLunchQuestMilestone;
    public string lunchQuestCompletedMilestone;
    public string afterLunchQuestMilestone;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    private GameObject fadeInPanelInstance;
    private GameObject fadeOutPanelInstance;


    private bool cutsceneAllowed = false;
    private bool eatingDialogue = false;
    private DialogueManager selfDialogueManager;

    private void Awake()
    {
        DialogueManager.OnCloseDialog += eggsAndBaconTrigger;
    }

    private void OnDestroy()
    {
        DialogueManager.OnCloseDialog -= eggsAndBaconTrigger;
    }

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(afterLunchQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(afterLunchQuest));
        }
        else if (milestones.getBoolMilestone(lunchQuestCompletedMilestone))
        {
            milestones.addMilestone(afterLunchQuestMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(lunchQuestComplete));
        }   
        else if (milestones.getBoolMilestone(girlPlateDoneLunchQuestMilestone))
        {
            milestones.addMilestone(lunchQuestCompletedMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(girlPlateLunchQuest));
        }
        else if (milestones.getBoolMilestone(dadPlateDoneLunchQuestMilestone))
        {
            milestones.addMilestone(girlPlateLunchQuestMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(dadPlateLunchQuest));
        }
        else if (milestones.getBoolMilestone(firstParkCompletedMilestone))
        {
            milestones.addMilestone(dadPlateLunchQuestMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(firstParkCompleted));
        }
        else if (milestones.getBoolMilestone(afterFlopsyQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(flopsyQuestHelpAfter));
        }
        else if (milestones.getBoolMilestone(flopsyQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(flopsyQuestHelp));
        }
        else if (milestones.getBoolMilestone(afterQuestMilestone))
        {
            milestones.addMilestone(disableArrowMilestone, true);
            milestones.addMilestone("dreamWorldUnlocked", false);       // She should not be able to go to the dream world when she has to do homework
            StartCoroutine(dialogueManager.ShowDialogue(afterQuest));
        }
        else if (milestones.getBoolMilestone(completedQuestMilestone))
        {
            milestones.addMilestone(afterQuestMilestone, true);
            selfDialogueManager = dialogueManager;
            cutsceneAllowed = true;
            StartCoroutine(dialogueManager.ShowDialogue(completedQuestBefore));
        }
        else if (milestones.getBoolMilestone(startQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(duringQuest));
        }
        else
        {
            dadEggInstance.SetActive(true);
            StartCoroutine(dialogueManager.ShowDialogue(firstInteraction));
            milestones.addMilestone(startQuestMilestone, true);
        }
    }

    public void eggsAndBaconTrigger()
    {
        // Check if its present in the eating dialogue
        if (eatingDialogue)
            StartCoroutine(eggsAndBaconCutsceneEnd(selfDialogueManager));
        // Check if its present in the cutscene
        else if (cutsceneAllowed)
            StartCoroutine(eggsAndBaconCutsceneStart(selfDialogueManager));
    }

    IEnumerator eggsAndBaconCutsceneStart(DialogueManager dialogueManager)
    {
        CutsceneManager cutsceneManager = dialogueManager.GetCutsceneManager();
        // Fade in Panel
        fadeInPanelInstance = cutsceneManager.StartCutscene(fadeInPanel);
        // Eating Dialogue
        eatingDialogue = true;
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(dialogueManager.ShowDialogue(eating));
    }

    IEnumerator eggsAndBaconCutsceneEnd(DialogueManager dialogueManager)
    {
        CutsceneManager cutsceneManager = dialogueManager.GetCutsceneManager();
        eatingDialogue = false;

        // Transition from fade in to fade out
        fadeOutPanelInstance = cutsceneManager.StartCutscene(fadeOutPanel);
        cutsceneManager.EndCutscene(fadeInPanelInstance);
        cutsceneManager.EndCutscene(fadeOutPanelInstance, 3);

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(dialogueManager.ShowDialogue(completedQuestAfter)); 
        cutsceneAllowed = false;
    }
}