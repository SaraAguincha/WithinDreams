using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour, Interactable
{
    // Dialogues
    [SerializeField] List<Dialog> firstInteraction;           //Good mornings, and egg task
    [SerializeField] List<Dialog> duringQuest;                //Reminder of egg task, and tip 
    [SerializeField] List<Dialog> completedQuestBefore;       //Let's eat breakfast -> cutscene
    [SerializeField] List<Dialog> completedQuestAfter;        //Let's eat breakfast -> cutscene
    [SerializeField] List<Dialog> eating;
    [SerializeField] List<Dialog> afterQuest;                 //Go do your homework for tomorrow


    [SerializeField] Milestones milestones;

    //[SerializeField] BooleanValue dadEggInstance;       //Verifies if the item was aquired
    [SerializeField]  public GameObject dadEggInstance;

    // Milestones names
    public string startQuestMilestone;
    public string completedQuestMilestone;
    public string afterQuestMilestone;

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
        if (milestones.getBoolMilestone(afterQuestMilestone))
        {
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
        // Fade in Panel
        if (fadeInPanel != null)
        {
            fadeInPanelInstance = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
        }
        // Eating Dialogue
        eatingDialogue = true;
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(dialogueManager.ShowDialogue(eating));
    }

    IEnumerator eggsAndBaconCutsceneEnd(DialogueManager dialogueManager)
    {
        eatingDialogue = false;
        if (fadeOutPanel != null)
        {
            fadeOutPanelInstance = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        Destroy(fadeInPanelInstance);
        Destroy(fadeOutPanelInstance, 3);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(dialogueManager.ShowDialogue(completedQuestAfter)); 
        cutsceneAllowed = false;
    }
}