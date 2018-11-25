using System.Collections.Generic;
using UnityEngine;

public class RewardTriggerAT : ActionTrigger
{
    [Header("Rewards")]
    public List<Item> items = new List<Item>();
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
