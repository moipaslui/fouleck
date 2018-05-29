using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int degatsArme;

    private EnemyHealthManager enemyHealth;

	// Use this for initialization
	void Start ()
    {
        enemyHealth = FindObjectOfType<EnemyHealthManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ennemi")
        {
            Debug.Log("Ennemi touché.");
            enemyHealth.health -= degatsArme;
        }
    }
}
