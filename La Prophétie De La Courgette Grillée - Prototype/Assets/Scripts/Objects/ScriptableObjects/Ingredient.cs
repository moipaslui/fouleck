using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Ingredient", menuName = "Item/Ingredient")]
public class Ingredient : Item
{
    [Header("Ingredient")]
    public int quality;
}
