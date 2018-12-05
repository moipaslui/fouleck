using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    [Header("Weapon")]
    public float damage;
    public Vector2[] colliderPoints;
    public float knockbackForce;
}
