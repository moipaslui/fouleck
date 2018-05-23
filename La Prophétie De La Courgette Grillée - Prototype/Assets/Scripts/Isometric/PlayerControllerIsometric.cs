using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerIsometric : MonoBehaviour
{
    [Header("GamePlay")]
	public float speed = 5f;

    [Header("Interactibles")]
    public float interactibleRadius = 3f;
    public LayerMask interactibleLayerMask;
    public GameObject ButtonIconInteractable;

    [HideInInspector]
    public Transform focus;

	private Rigidbody2D rb;
	private Animator anim;
	private Vector2 moveVelocity;
    private EventSystem ES;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        ES = FindObjectOfType<EventSystem>();
	}

    private void Update()
    {
        // Movements
        moveVelocity = new Vector2(Input.GetAxisRaw("HorizontalMovement"), Input.GetAxisRaw("VerticalMovement")).normalized * speed;
        if (moveVelocity == Vector2.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("moveX", moveVelocity.x / speed);
            anim.SetFloat("moveY", moveVelocity.y / speed);
            if (moveVelocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (moveVelocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }


        // Interactions
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, interactibleRadius, interactibleLayerMask);
        if (targets.Length > 0)
        {
            float minDistance = Vector2.Distance(transform.position, targets[0].transform.position);
            focus = targets[0].transform;
            foreach (Collider2D target in targets)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
                if (distanceToTarget < minDistance)
                { 
                    minDistance = distanceToTarget;
                    focus = target.transform;
                }
            }
            ButtonIconInteractable.transform.position = new Vector3
                                                        ( focus.transform.position.x + focus.GetComponent<Interactable>().offsetIcon.x,
                                                          focus.transform.position.y + focus.GetComponent<Interactable>().offsetIcon.y,
                                                          focus.transform.position.z - 0.001f
                                                        );
            ButtonIconInteractable.SetActive(true);
        }
        else
        {
            ButtonIconInteractable.SetActive(false);
            focus = null;
        }
        
        if (Input.GetButtonDown("Interact") && focus != null)
        {
            focus.GetComponent<Interactable>().Interact();
        }


        // Inventory
        if(Input.GetButtonDown("Clear"))
        {
            Item selectedItem = ES.currentSelectedGameObject.GetComponent<InventorySlot>().item;
            Inventory.instance.Remove(selectedItem);
        }
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactibleRadius);
    }
}
