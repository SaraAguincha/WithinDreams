using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlPlate : MonoBehaviour, Interactable
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

    [SerializeField] public GameObject girlPlateInstance;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (milestones.getBoolMilestone(girlPlateDoneLunchQuestMilestone))
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
            this.spriteRenderer.enabled = true;
            milestones.addMilestone(girlPlateDoneLunchQuestMilestone, true);
            StartCoroutine(dialogueManager.ShowDialogue(girlPlateLunchQuestDialogue));
        }
        else if (milestones.getBoolMilestone(dadPlateLunchQuestMilestone))
        {
            StartCoroutine(dialogueManager.ShowDialogue(dadPlateLunchQuestDialogue));
        }
    }
}
