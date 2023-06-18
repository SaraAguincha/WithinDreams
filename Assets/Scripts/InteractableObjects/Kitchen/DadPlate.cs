using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadPlate : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> dadPlateLunchQuestDialogue;
    [SerializeField] List<Dialog> girlPlateLunchQuestDialogue;
    [SerializeField] List<Dialog> lunchQuestCompletedDialogue;

    [SerializeField] Milestones milestones;

    public string dadPlateLunchQuestMilestone;
    public string dadPlateDoneLunchQuestMilestone;
    public string girlPlateLunchQuestMilestone;
    public string girlPlateDoneLunchQuestMilestone;
    public string lunchQuestCompletedMilestone;

    [SerializeField] public GameObject dadPlateInstance;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (milestones.getBoolMilestone(dadPlateDoneLunchQuestMilestone))
        {
            this.spriteRenderer.enabled = true;
        }
    }


    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(lunchQuestCompletedMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(lunchQuestCompletedDialogue));
        }
        else if (milestones.getBoolMilestone(girlPlateLunchQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(girlPlateLunchQuestDialogue));
        }
        else if (milestones.getBoolMilestone(dadPlateLunchQuestMilestone))
        {
            this.spriteRenderer.enabled = true;
            milestones.addMilestone(dadPlateDoneLunchQuestMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(dadPlateLunchQuestDialogue));
        }
    }
}
