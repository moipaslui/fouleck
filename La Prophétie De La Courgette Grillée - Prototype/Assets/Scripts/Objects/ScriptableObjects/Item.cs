using System;
using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Item", menuName = "Item/other")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    [TextArea] public string description;
    public float floorHeight = 0f;
    public float cost;

    [Header("Interactable")]
    public Vector2 offsetIcon;
}
