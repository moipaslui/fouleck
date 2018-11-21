using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [Range(0,10)]
    public int startingTotalHearts = 3;
    [Range(0, 10)]
    public int startingHearts = 3;
    
    public int numberOfBlink;
    public float blinkTime;
    public float currentHP;

    public Image[] healthImages;

    public Sprite healthSpriteEmpty;
    public Sprite healthSpriteHalf;
    public Sprite healthSpriteFull;

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

                if (i+1 <= (float)currentHP / 2)
                {
                    healthImages[i].sprite = healthSpriteFull;
                }
                else
                {
                    if ((float)(i+1) - (float)currentHP / 2 == 0.5f) // If there is not a full heart
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

            GetComponent<Rigidbody2D>().velocity = knockback;

            StartCoroutine("Blink");

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
        Debug.Log("Joueur soigné !");
        UpdateUIHearts();
    }

    private IEnumerator Blink()
    {
        GetComponent<PlayerControllerIsometric>().canMove = false;
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
        GetComponent<PlayerControllerIsometric>().canMove = true;
    }


    public void MangeRepas(Repas repas)
    {
        HealPlayer(repas.heal);
        /// Ajouter les "resistances"
    }
}
