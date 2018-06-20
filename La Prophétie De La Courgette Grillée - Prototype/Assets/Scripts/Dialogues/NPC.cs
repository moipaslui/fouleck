using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    
    [Header("Spots")]
    public Transform[] patrolSpots;
    public bool randomSpots = false;
    public bool circlePatrol = true;

    [Header("times")]
    public float minStopTime = 1f;
    public float maxStopTime = 3f;

    [HideInInspector]
    public bool canMove;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveVelocity;
    private int directionSpot = 1;
    private int nextSpot;
    private float waitCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;
    }

    private void Update()
    {
        moveVelocity = Vector2.zero;

        if (canMove)
        {
            if (Vector2.Distance(transform.position, patrolSpots[nextSpot].position) < 0.1f) // Is on spot
            {
                if (randomSpots)
                {
                    nextSpot = Random.Range(0, patrolSpots.Length - 1);
                }
                else
                {
                    nextSpot += directionSpot;
                    if (nextSpot < 0 || nextSpot > patrolSpots.Length - 1)
                    {
                        if (circlePatrol)
                        {
                            nextSpot = 0;
                        }
                        else
                        {
                            directionSpot = -directionSpot;
                            nextSpot += directionSpot * 2;
                        }
                    }
                }

                anim.SetBool("isWalking", false);
                waitCounter = Random.Range(minStopTime, maxStopTime);
            }
            else    // Go to spot
            {
                if (waitCounter <= 0)
                {
                    anim.SetBool("isWalking", true);
                    moveVelocity = new Vector2(patrolSpots[nextSpot].position.x - transform.position.x, patrolSpots[nextSpot].position.y - transform.position.y).normalized * speed;
                }
                else
                {
                    waitCounter -= Time.deltaTime;
                }
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        if (moveVelocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveVelocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
