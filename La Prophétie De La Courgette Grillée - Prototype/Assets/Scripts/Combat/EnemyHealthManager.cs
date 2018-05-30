using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    public int enemyDamage;

    public int numberOfBlink;
    public float blinkTime;

    public ParticleSystem effectOnDying;

    private bool isBlinking = false;

    public void HurtEnemy(int damage)
    {
        if (!isBlinking)
        {
            health -= damage;
            StartCoroutine("Blink");
            if (health <= 0)
            {
                Instantiate(effectOnDying, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<PlayerHealthManager>().HurtPlayer(enemyDamage);
        }
    }

    private IEnumerator Blink()
    {
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
    }
}
