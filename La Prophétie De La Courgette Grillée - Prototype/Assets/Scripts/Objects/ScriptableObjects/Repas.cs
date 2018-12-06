using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Repas", menuName = "Item/Craftable/Repas")]
public class Repas : Craftable
{
    [Header("Repas")]
    public int heal;
    [Range(0, 5)] public int shieldBuff = 0;
    [Range(0.5f, 3f)] public float damageBuff = 1f;
    [Range(0.5f, 3f)] public float speedBuff = 1f;
    [Range(0.5f, 3f)] public float lootBuff = 1f;
    [Range(0f, 10f)] public float lifeBuff = 0f;
    public int timeOfBuff = 120;
}