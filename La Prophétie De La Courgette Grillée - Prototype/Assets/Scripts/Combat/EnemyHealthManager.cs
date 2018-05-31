using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    public int enemyDamage;
    public float knockbackForce = 2f;

    public int numberOfBlink;
    public float blinkTime;

    public ParticleSystem effectOnDying;

    private bool isBlinking = false;

    public void HurtEnemy(int damage, Vector2 knockback)
    {
        if (!isBlinking)
        {
            health -= damage;
            GetComponent<Rigidbody2D>().velocity = knockback;
            StartCoroutine("Blink");
            if (health <= 0)
            {
                Instantiate(effectOnDying, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            Vector2 knockback = ((Vector2)other.transform.position - (Vector2)transform.position).normalized * knockbackForce;
            other.GetComponent<PlayerHealthManager>().HurtPlayer(enemyDamage, knockback);
        }
    }

    private IEnumerator Blink()
    {
        // Can move = false
        isBlinking = true;
        SpriteRenderer[] enemySprites = GetComponentsInChildren<SpriteRenderer>();

        for (int nbBlink = 0; nbBlink < numberOfBlink; nbBlink++)
        {
            for (float countDown = blinkTime; countDown >= 0; countDown -= Time.deltaTime)
            {
                foreach (SpriteRenderer sprite in enemySprites)
                {
                    sprite.enabled = false;
                }
                yield return new WaitForSeconds(0.01f);
            }

            for (float countDown = 0; countDown <= blinkTime; countDown += Time.deltaTime)
            {
                foreach (SpriteRenderer sprite in enemySprites)
                {
                    sprite.enabled = true;
                }

                yield return new WaitForSeconds(0.01f);
            }
        }
        isBlinking = false;
        // Can move = true
        /* temporaire */
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        /* ---------- */
    }
}
