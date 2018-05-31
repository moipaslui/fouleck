using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int degatsArme;
    public float knockbackForce = 3f;

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "ennemi")
        {
            Debug.Log("Ennemi touché.");
            Vector2 knockback = ((Vector2)other.transform.position - (Vector2)transform.position).normalized * knockbackForce;
            other.GetComponent<EnemyHealthManager>().HurtEnemy(degatsArme, knockback);
        }
    }
}
