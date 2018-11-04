using UnityEngine;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestData : ScriptableObject
{
    [Header("Texts")]
    public string title;
    [TextArea] public string description;

    [Header("Rewards")]
    public Item[] itemsReward;
    public int expReward;
    public int moneyReward;
}
