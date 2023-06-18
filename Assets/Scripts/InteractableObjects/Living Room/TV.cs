using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TV : MonoBehaviour, Interactable
{
    [SerializeField] List<Dialog> defaultDialogue;
    [SerializeField] List<Dialog> duringGameQuestDialogue;

    [SerializeField] Milestones milestones;

    public string duringGameQuestMilestone;
    public string fluffyBordAllowed;
    public string afterfluffyBordMilestone;

    public SceneName sceneInfo;
    public GameObject dreamWorldPanel;

    public void Interact(DialogueManager dialogueManager)
    {
        if (milestones.getBoolMilestone(fluffyBordAllowed))
        {
            milestones.addMilestone(afterfluffyBordMilestone, true);
            sceneInfo.setSceneName("Fluffy Bord");
            if (SceneUtility.GetBuildIndexByScenePath("Fluffy Bord Menu") > 0)
                StartCoroutine(DreamWorldCoroutine("Fluffy Bord Menu"));
        }
        else if (milestones.getBoolMilestone(duringGameQuestMilestone))
        {
            milestones.addMilestone(fluffyBordAllowed, true);
            StartCoroutine(dialogueManager.ShowDialogue(duringGameQuestDialogue));
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
