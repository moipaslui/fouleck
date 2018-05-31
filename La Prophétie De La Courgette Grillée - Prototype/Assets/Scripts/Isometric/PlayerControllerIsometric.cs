using UnityEngine;

public class PlayerControllerIsometric : MonoBehaviour
{
	public float speed = 5f;

    [HideInInspector]
    public bool canMove = true;

	private Rigidbody2D rb;
	private Animator anim;
	private Vector2 moveVelocity;

    private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

    private void Update()
    {
        if (canMove)
        {
            anim.SetBool("isHurt", false);

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
        }
        else
        {
            anim.SetBool("isHurt", true);
        }
	}

	private void FixedUpdate()
	{
        if (canMove)
        {
            rb.velocity = moveVelocity;
        }
	}
}
