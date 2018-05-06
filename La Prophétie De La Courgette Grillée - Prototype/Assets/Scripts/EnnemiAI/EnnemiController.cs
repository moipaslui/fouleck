using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{
	// Public
	[Header("Speed")]
	public float walkSpeed;
	public float runSpeed;
	public float rotationSpeed;

	[Header("times")]
	public float minStopTime;
	public float maxStopTime;

	[Header("Spots")]
	public Transform[] patrolSpots;

	// Private
	private Rigidbody2D rb;

	private FieldOfView FOW;
	private Transform target;

	private int directionSpot = 1;
	private int nextSpot;

	private float waitCounter;

	private Vector2 moveVelocity;
	private Vector3 lastTargetPosition;

	private bool isChasing;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		FOW = GetComponent<FieldOfView>();
	}

	void Update ()
	{
		rb.angularVelocity = 0;
		target = FOW.target;

		if(target != null)
			isChasing = true;

		/* --- Patrol mode --- */
		if(!isChasing)
		{
			moveVelocity = Vector2.zero;

			if(Vector3.Distance(transform.position, patrolSpots[nextSpot].position) < 0.1f) // Is on spot
			{
				nextSpot += directionSpot;
				if(nextSpot < 0 || nextSpot > patrolSpots.Length - 1)
				{
					directionSpot = -directionSpot;
					nextSpot += directionSpot * 2;
				}

				waitCounter = Random.Range(minStopTime, maxStopTime);
			}
			else 	// Go to spot
			{
				if(waitCounter <= 0)
				{
					if(Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, patrolSpots[nextSpot].position - transform.position)) < 0.1)
					{
						moveVelocity = new Vector2(patrolSpots[nextSpot].position.x - transform.position.x, patrolSpots[nextSpot].position.y - transform.position.y).normalized * walkSpeed;
					}
					else
					{
						transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, patrolSpots[nextSpot].position - transform.position), Time.deltaTime * rotationSpeed * walkSpeed / (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, patrolSpots[nextSpot].position - transform.position)) * Mathf.Deg2Rad));
					}
				}
				else
					waitCounter -= Time.deltaTime;
			}
		}

		/* --- Chasing target --- */
		else
		{
			if(target != null)
			{
				lastTargetPosition = target.position;
				moveVelocity = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized * runSpeed;
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, target.position - transform.position), Time.deltaTime * rotationSpeed * runSpeed / (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, target.position - transform.position)) * Mathf.Deg2Rad));
			}
			else
			{
				if(Vector3.Distance(transform.position, lastTargetPosition) < 0.1f)
				{
					isChasing = false;
					float minDistance = Vector3.Distance(transform.position, patrolSpots[0].position);
					for(int i = 0; i < patrolSpots.Length; i++)
					{
						if(Vector3.Distance(transform.position, patrolSpots[i].position) < minDistance)
						{
							nextSpot = i;
							minDistance = Vector3.Distance(transform.position, patrolSpots[i].position);
						}
					}
					waitCounter = Random.Range(minStopTime, maxStopTime);
				}
				else
				{
					moveVelocity = new Vector2(lastTargetPosition.x - transform.position.x, lastTargetPosition.y - transform.position.y).normalized * walkSpeed;
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, lastTargetPosition - transform.position), Time.deltaTime * rotationSpeed * walkSpeed / (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, lastTargetPosition - transform.position)) * Mathf.Deg2Rad));
				}
			}
		}
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}
}
