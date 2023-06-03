using Inventory.Model;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;

    public float speed;
    public VectorValue startingPosition;
    private Vector2 input;

    private Animator animator;

    public LayerMask colidableLayer;
    public LayerMask interactableLayer;
    public LayerMask itemLayer;
    public LayerMask transitionLayer;
    public GameObject dreamWorldPanel;

    [SerializeField] 
    DialogueManager dialogueManager;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    public Milestones milestones;

    [SerializeField]
    private AudioSource footsteps;

    public static event Action requestPause;

    private void Start()
    {
        transform.position = startingPosition.initialValue;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SceneTransition.blockPlayerTransition += TransitionDialogue;
    }

    private void OnDestroy()
    {
        SceneTransition.blockPlayerTransition -= TransitionDialogue;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            requestPause?.Invoke();
            return;
        }

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
                input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                footsteps.enabled = true;

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(MoveCharacter(targetPos));
            }
            else
            {
                footsteps.enabled = false;
            }
        }
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact(dialogueManager);

        if (Input.GetKeyDown(KeyCode.Space) && milestones.getBoolMilestone("dreamWorldUnlocked"))
        {
            startingPosition.initialValue = new Vector2(transform.position.x, transform.position.y);
            
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName.Contains("Dream"))
            {
                sceneName = sceneName.Remove(sceneName.Length - 6);
            }
            else
            {
                sceneName = sceneName + " Dream";
            }

            if (SceneUtility.GetBuildIndexByScenePath(sceneName) > 0)
                //SceneManager.LoadScene(sceneName);
                StartCoroutine(DreamWorldCoroutine(sceneName));
        }
        
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
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void TransitionDialogue()
    {
        Interact(dialogueManager);
    }

    void Interact(DialogueManager dialogManager)
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var interactableCollider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        // TODO - There is a bug where if you spam "Z" while entering a transition block the text is redered 2 types
        // Easy to solve but best way is being investigated
        var itemCollider = Physics2D.OverlapCircle(transform.position, 1f, itemLayer);
        var transitionCollider = Physics2D.OverlapCircle(interactPos, 1f, transitionLayer);

        if (interactableCollider != null)
        {
            interactableCollider.GetComponent<Interactable>()?.Interact(dialogManager);
        }

        else if (transitionCollider != null)
        {
            transitionCollider.GetComponent<Interactable>()?.Interact(dialogManager);
        }

         if (itemCollider != null)
        {
            Item item = itemCollider.GetComponent<Item>();
            int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (remainder == 0) item.DestroyItem();
            else
                item.Quantity = remainder;
        }

    }

    IEnumerator MoveCharacter(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
        startingPosition.initialValue = new Vector2(transform.position.x, transform.position.y);
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.01f, colidableLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

    public DialogueManager GetDialogManager() { return dialogueManager; }

}