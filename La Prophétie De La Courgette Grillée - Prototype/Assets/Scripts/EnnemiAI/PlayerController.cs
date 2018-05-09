using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed = 8f;

	private Rigidbody2D rb;
	private Vector2 moveVelocity;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed).normalized * speed;
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "ennemi")
			Destroy(gameObject);
	}
}
