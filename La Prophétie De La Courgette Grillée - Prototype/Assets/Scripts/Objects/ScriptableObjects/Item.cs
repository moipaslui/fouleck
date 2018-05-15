using UnityEngine;

public class Item : ScriptableObject
{
    public new string name;
    public Sprite icon;
    [TextArea] public string description;
    public int cost;
}
