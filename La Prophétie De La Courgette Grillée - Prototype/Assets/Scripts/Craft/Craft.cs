using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Craft", menuName = "Craft")]
public class Craft : ScriptableObject
{
    public List<Ingredient> craftNeeds;
    public Repas craftResult;
}
