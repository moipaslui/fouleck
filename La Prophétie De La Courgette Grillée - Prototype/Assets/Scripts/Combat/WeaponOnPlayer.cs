using UnityEngine;

[ExecuteInEditMode]
public class WeaponOnPlayer : MonoBehaviour
{
    public Weapon arme;
    public float knockbackForce = 3f;

    [Header("UI")]
    public SpriteRenderer weaponSprite;
    public SpriteRenderer weaponSpriteBack;

    public PolygonCollider2D polygonCollider;

    public bool isMakingNewCollider;

    private Vector2[] handPoints;

    public void Update()
    {
        if(!Application.isPlaying)
        {
            if(isMakingNewCollider)
            {
                if(arme != null)
                {
                     arme.colliderPoints = polygonCollider.points;
                }
                else
                {
                    handPoints = polygonCollider.points;
                }
            }
            else
            {
                ChangeWeapon(arme);
            }
        }
    }

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
        if(other.tag == "ennemi")
        {
            Debug.Log("Ennemi touché.");
            Vector2 knockback = ((Vector2)other.transform.position - (Vector2)transform.position).normalized * knockbackForce;
            other.GetComponent<EnemyHealthManager>().HurtEnemy(arme.damage, knockback);
        }
    }
}
