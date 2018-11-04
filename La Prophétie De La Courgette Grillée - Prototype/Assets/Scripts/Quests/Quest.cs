using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestData questData;

    public void StartQuest()
    {
        GameManager.questManager.activeQuests.Add(this);


        /// UI
    }

    public void EndQuest()
    {
        GameManager.questManager.activeQuests.Remove(this);

        // Rewards
        foreach (Item item in questData.itemsReward)
            GameManager.inventory.Add(item);

        GameManager.moneyManager.AddMoney(questData.moneyReward);
        /// add exp


        /// UI
    }
}
