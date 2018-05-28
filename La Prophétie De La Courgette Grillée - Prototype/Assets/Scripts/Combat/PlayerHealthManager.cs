using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour 
{
    private int maxHP;
    public float currentHP;

    private int maxHearts = 10;
    public int startingHearts = 3;

    private int healthPerHeart = 2;

    public bool playerAlive;

    public Image[] healthImages;
    public Sprite[] healthSprites;

    public int HealthPerHeart
    {
        get
        {
            return healthPerHeart;
        }

        set
        {
            healthPerHeart = value;
        }
    }


    // Use this for initialization
    void Start () 
	{
        playerAlive = true;

        currentHP = startingHearts * healthPerHeart;
        maxHP = maxHearts * healthPerHeart;

        CheckHealthAmount();
	}

	// Update is called once per frame
	void Update () 
	{
        if (currentHP == 0)
        {
            playerAlive = false;
            Destroy(gameObject);
        }
	}

    void CheckHealthAmount()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            if (startingHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if(empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (currentHP >= i * healthPerHeart) 
                {
                    image.sprite = healthSprites[healthSprites.Length-1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currentHP));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }

    public void AddHeartContainer()
    {
        startingHearts++;
        startingHearts = Mathf.Clamp(startingHearts, 0, maxHearts);

        currentHP = startingHearts * healthPerHeart;
        maxHP = maxHearts * healthPerHeart;

        CheckHealthAmount();
    }
}
