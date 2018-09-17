using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Repas", menuName = "Item/Craftable/Repas")]
public class Repas : Craftable
{
    [Header("Repas")]
    public int heal;
}
