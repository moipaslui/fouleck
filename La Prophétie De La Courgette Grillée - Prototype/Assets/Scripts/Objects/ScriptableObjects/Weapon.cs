using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Header("Weapon")]
    public int damage;
    public Vector2[] colliderPoints;
}
