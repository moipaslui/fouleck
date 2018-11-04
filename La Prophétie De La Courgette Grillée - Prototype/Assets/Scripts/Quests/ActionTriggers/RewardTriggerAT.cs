using UnityEngine;

public class RewardTriggerAT : ActionTrigger
{
    [Header("Rewards")]
    public Item[] items;
    public int money;
    public int exp;

    public override void Trigger()
    {
        foreach(Item item in items)
        {
            GameManager.inventory.Add(item);
        }

        GameManager.moneyManager.AddMoney(money);
        /// Add exp

        base.Trigger();
    }
}
