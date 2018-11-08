using System.Collections.Generic;

public class ActiveSellerAT : ActionTrigger
{
    public Seller seller;
    public List<Item> itemsToAdd;
    public int money;

    public override void Trigger()
    {
        seller.ChangeActivation(true);
        
        foreach(Item item in itemsToAdd)
        {
            seller.itemsToSell.Add(item);
        }

        seller.currentMoney = money;

        base.Trigger();
    }
}
