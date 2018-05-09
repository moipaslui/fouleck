using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerIsometric : MonoBehaviour
{

	public float speed = 5f;

	private Rigidbody2D rb;
	private Animator anim;
	private Vector2 moveVelocity;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
		if(moveVelocity == Vector2.zero)
		{
			anim.SetBool("isMoving", false);
		}
		else
		{
			anim.SetBool("isMoving", true);
			anim.SetFloat("moveX", moveVelocity.x / speed);
			anim.SetFloat("moveY", moveVelocity.y / speed);
			if(moveVelocity.x > 0)
			{
				transform.rotation = Quaternion.Euler(0f, 180f, 0f);
			}
			else if(moveVelocity.x < 0)
			{
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}
}
