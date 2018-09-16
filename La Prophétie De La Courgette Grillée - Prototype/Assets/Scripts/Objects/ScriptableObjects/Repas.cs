using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Repas", menuName = "Item/Repas")]
public class Repas : Item
{
    [Header("Repas")]
    public int heal;
}
