using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour 
{
    private PlayerHealthManager pHealth;

    public int enemyDamage;

	// Use this for initialization
	void Start () 
	{
        pHealth = FindObjectOfType<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            pHealth.currentHP -= enemyDamage;
            pHealth.UpdateHearts();
        }
    }
}
