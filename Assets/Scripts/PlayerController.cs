using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;

    public float speed;
    public VectorValue startingPosition;
    private Vector2 input;

    //private Rigidbody2D myRigidBody;

    private Animator animator;

    public LayerMask colidableLayer;
    public LayerMask interactableLayer;
    public LayerMask itemLayer;

    [SerializeField] DialogueManager dialogueManager;

    [SerializeField]
    private InventorySO inventoryData;

    private void Start()
    {
        transform.position = startingPosition.initialValue;
        //myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
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

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(MoveCharacter(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact(dialogueManager);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startingPosition.initialValue = new Vector2(transform.position.x, transform.position.y);
            
            string sceneName = SceneManager.GetActiveScene().name;

            print(sceneName.Contains("Dream"));
            if (sceneName.Contains("Dream"))
            {
                sceneName = sceneName.Remove(sceneName.Length - 6);
            }
            else
            {
                sceneName = sceneName + " Dream";
            }
            //print(sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

    void Interact(DialogueManager dialogManager)
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var interactableCollider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        var itemCollider = Physics2D.OverlapCircle(transform.position, 1f, itemLayer);

        if (interactableCollider != null)
        {
            interactableCollider.GetComponent<Interactable>()?.Interact(dialogManager);
        }

        else if (itemCollider != null)
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