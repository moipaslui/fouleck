using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public QuestData questData;
    public bool isActive;
    public List<QuestTrigger> questTriggers;
    public int line;

    public void StartQuest()
    {
        isActive = true;


        /// UI
    }

    public void EndQuest()
    {
        isActive = false;

        // Rewards
        foreach (Item item in questData.itemsReward)
            GameManager.inventory.Add(item);

        GameManager.moneyManager.AddMoney(questData.moneyReward);
        GameManager.expManager.AddExperience(questData.expReward);


        /// UI
    }
}
