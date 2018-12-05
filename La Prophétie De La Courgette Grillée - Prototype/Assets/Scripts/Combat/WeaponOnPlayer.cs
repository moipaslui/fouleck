using UnityEngine;

public class WeaponOnPlayer : MonoBehaviour
{
    public Weapon arme;
    public float handDamage = 1f;
    public float damage = 1f;

    [Header("UI")]
    public SpriteRenderer weaponSprite;
    public SpriteRenderer weaponSpriteBack;

    public PolygonCollider2D polygonCollider;

    private Vector2[] handPoints;
    private float knockbackForce = 0.8f;

    public void ChangeWeapon(Weapon arme)
    {
        this.arme = arme;
        if (arme == null)
        {
            weaponSprite.sprite = null;
            weaponSpriteBack.sprite = null;
            polygonCollider.points = handPoints;
        }
        else
        {
            weaponSprite.sprite = arme.icon;
            weaponSpriteBack.sprite = arme.icon;
            polygonCollider.points = arme.colliderPoints;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (polygonCollider.IsTouching(other))
        {
            if (other.tag == "ennemi")
            {
                Debug.Log("Ennemi touché.");
                if (arme != null)
                {
                    Vector2 knockbackDirection = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;
                    other.GetComponent<EnemyHealthManager>().HurtEnemy(arme.damage * damage, knockbackDirection, arme.knockbackForce);
                }
                else
                {
                    Vector2 knockbackDirection = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;
                    other.GetComponent<EnemyHealthManager>().HurtEnemy(handDamage * damage, knockbackDirection, knockbackForce);
                }
            }
        }
    }
}
