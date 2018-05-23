using UnityEngine;

[ExecuteInEditMode]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    [TextArea] public string description;
    public float floorHeight = 0f;
    public int cost;

    [Header("Interactable")]
    public Vector2 offsetIcon;
}
