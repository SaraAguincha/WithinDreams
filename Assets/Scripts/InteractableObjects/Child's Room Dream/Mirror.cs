using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mirror : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> moowieVanishedDialogue;
    [SerializeField] Milestones milestones;

    [SerializeField] string moowieVanished;
    [SerializeField] string enterMirrorAllowed;
    [SerializeField] string afterFirstParkQuest;

    public Vector2 playerPosition;
    public VectorValue playerInfo;
    public SceneName sceneInfo;
    public GameObject dreamWorldPanel;

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(afterFirstParkQuest))
        {
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
        }
        else if (milestones.getBoolMilestone(enterMirrorAllowed))
        {
            playerInfo.initialValue = playerPosition;
            if (SceneUtility.GetBuildIndexByScenePath("Playground") > 0)
                StartCoroutine(DreamWorldCoroutine("Playground"));
        }
        else if (milestones.getBoolMilestone(moowieVanished))
        {
            StartCoroutine(dialogueManager.ShowDialogue(moowieVanishedDialogue));
            milestones.addMilestone(enterMirrorAllowed, true);
        }
        else
            StartCoroutine(dialogueManager.ShowDialogue(defaultDialogue));
    }

    public IEnumerator DreamWorldCoroutine(string sceneName)
    {
        if (dreamWorldPanel != null)
        {
            GameObject panel = Instantiate(dreamWorldPanel, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(panel);
            Destroy(panel, 5);
        }
        yield return new WaitForSeconds(0.85f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
