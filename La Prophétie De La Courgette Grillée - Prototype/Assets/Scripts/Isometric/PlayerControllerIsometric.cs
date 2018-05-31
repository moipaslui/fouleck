using UnityEngine;

public class PlayerControllerIsometric : MonoBehaviour
{
	public float speed = 5f;

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

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}
}
