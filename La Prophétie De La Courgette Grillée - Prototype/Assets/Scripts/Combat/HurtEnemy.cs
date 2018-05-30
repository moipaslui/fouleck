using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int degatsArme;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ennemi")
        {
            Debug.Log("Ennemi touché.");
            other.GetComponent<EnemyHealthManager>().HurtEnemy(degatsArme);
        }
    }
}
