using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [Range(0,10)]
    public int startingTotalHearts = 3;
    [Range(0, 10)]
    public int startingHearts = 3;

    public float timeStunned;
    public float blinkTime;
    public float currentHP;
    public float currentShield;

    public Image[] healthImages;
    public Sprite healthSpriteEmpty;
    public Sprite healthSpriteHalf;
    public Sprite healthSpriteFull;

    public Image[] shieldImages;

    // 1 HEART = 2 HPs

    private int totalHearts;
    private int healthPerHeart = 2;
    private int maxHearts = 10;
    private bool isBlinking = false;

    void Start () 
	{
        maxHearts = healthImages.Length;

        if (startingHearts > startingTotalHearts)
        {
            startingHearts = startingTotalHearts;
        }

        currentHP = startingHearts * healthPerHeart;
        totalHearts = startingTotalHearts;
        UpdateUIHearts();
	}

    private void UpdateUIHearts()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            if (i < startingTotalHearts)
            {
                healthImages[i].enabled = true;

                if (i+1 <= currentHP / 2)
                {
                    healthImages[i].sprite = healthSpriteFull;
                }
                else
                {
                    if ((float)(i+1) - currentHP / 2 == 0.5f) // If there is not a full heart
                    {
                        healthImages[i].sprite = healthSpriteHalf;
                    }
                    else
                    {
                        healthImages[i].sprite = healthSpriteEmpty;
                    }
                }
            }
            else
            {
                healthImages[i].enabled = false;
            }
        }
    }

    public void AddHeartContainer()
    {
        if (++totalHearts > maxHearts)
        {
            totalHearts--;
        }
        else
        {
            currentHP = totalHearts * healthPerHeart;

            UpdateUIHearts();
        }
    }


    public void HurtPlayer(int damage, Vector2 knockback)
    {
        if (!isBlinking)
        {
            currentHP -= damage;
            Debug.Log("Joueur touché !");
            StartCoroutine(Knockback(knockback, damage / 2));
            StartCoroutine(Blink(damage / 2f));
            GetComponent<Animator>().SetBool("isAttacking", false);
            UpdateUIHearts();

            if (currentHP <= 0)
            {
                Debug.Log("The player is dead.");
                Time.timeScale = 0f;
            }
        }
    }

    public void HealPlayer(int heal)
    {
        currentHP += heal;
        UpdateUIHearts();
    }

    public IEnumerator AutoHeal(float timeBeforeHeal, float timeToHeal)
    {
        if(timeToHeal == 0)
        {
            while(true)
            {
                HealPlayer(1);
                yield return new WaitForSeconds(timeBeforeHeal);
            }
        }

        float limitTime = timeToHeal + Time.time;
        while(Time.time < limitTime)
        {
            HealPlayer(1);
            yield return new WaitForSeconds(timeBeforeHeal);
        }
    }

    private IEnumerator Knockback(Vector2 knockbackDirection, float knockbackForce)
    {
        Vector2 initalPosition = transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (Vector2.Distance(initalPosition, transform.position) < 0.8f)
        {
            rb.MovePosition(transform.position + (Vector3)knockbackDirection * 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector2.zero;
    }

    private IEnumerator Blink(float knockbackForce)
    {
        GetComponent<PlayerControllerIsometric>().canMove = false;
        isBlinking = true;
        SpriteRenderer[] enemySprites = GetComponentsInChildren<SpriteRenderer>();

        for (float time = 0f; time < timeStunned * knockbackForce; time += Time.deltaTime)
        {
            for (float countDown = blinkTime; countDown >= 0; countDown -= Time.deltaTime)
            {
                if (time > timeStunned * knockbackForce)
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

        isBlinking = false;
        GetComponent<PlayerControllerIsometric>().canMove = true;
    }
}
