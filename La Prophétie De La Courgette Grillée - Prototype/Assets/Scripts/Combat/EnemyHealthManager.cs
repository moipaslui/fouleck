using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float health;
    public int enemyDamage;
    public float knockbackForce = 2f;

    public float timeStunned;
    public float blinkTime;

    public GameObject itemPrefab;
    public Item itemToPop;
    public ParticleSystem effectOnDying;

    private bool isBlinking = false;

    public void HurtEnemy(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        if (!isBlinking)
        {
            GetComponent<EnnemiController>().ennemiFace.sprite = GetComponent<EnnemiController>().hurtFace;
            GetComponent<Animator>().SetBool("isWalking", false);
            GetComponent<Animator>().SetBool("isAttacking", false);
            health -= damage;
            StartCoroutine(Knockback(knockbackDirection, knockbackForce));
            StartCoroutine(Blink(knockbackForce));
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            Vector2 knockbackDirection = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;
            other.GetComponent<PlayerHealthManager>().HurtPlayer(enemyDamage, knockbackDirection);
        }
    }

    private IEnumerator Knockback(Vector2 knockbackDirection, float knockbackForce)
    {
        Vector2 initalPosition = transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while(Vector2.Distance(initalPosition, transform.position) < 0.8f)
        {
            rb.MovePosition(transform.position + (Vector3)knockbackDirection * 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Blink(float knockbackForce)
    {
        GetComponent<EnnemiController>().canMove = false;
        isBlinking = true;
        SpriteRenderer[] enemySprites = GetComponentsInChildren<SpriteRenderer>();

        for(float time = 0f; time < timeStunned * knockbackForce; time += Time.deltaTime)
        {
            for (float countDown = blinkTime; countDown >= 0; countDown -= Time.deltaTime)
            {
                if(time > timeStunned * knockbackForce)
                    break;

                foreach (SpriteRenderer sprite in enemySprites)
                {
                    sprite.enabled = false;
                }
                yield return new WaitForEndOfFrame();
            }

            for (float countDown = 0; countDown <= blinkTime; countDown += Time.deltaTime)
            {
                if (time > timeStunned * knockbackForce)
                    break;

                foreach (SpriteRenderer sprite in enemySprites)
                {
                    sprite.enabled = true;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        foreach (SpriteRenderer sprite in enemySprites)
        {
            sprite.enabled = true;
        }

        isBlinking = false;
        GetComponent<EnnemiController>().canMove = true;
    }

    private void Die()
    {
        Instantiate(effectOnDying, transform.position, transform.rotation);
        if (itemToPop != null)
        {
            GameObject clone = Instantiate(itemPrefab, transform.position, transform.rotation);
            clone.GetComponent<ItemOnObject>().ChangeItem(itemToPop);
        }
        GameManager.expManager.AddExperience(15);
        Destroy(gameObject);
    }
}
