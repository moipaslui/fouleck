using System.Collections.Generic;

public class ActiveSellerAT : ActionTrigger
{
    public Seller seller;
    public List<Item> itemsToAdd;

    public override void Trigger()
    {
        seller.ChangeActivation(true);
        
        foreach(Item item in itemsToAdd)
        {
            seller.itemsToSell.Add(item);
        }

        base.Trigger();
    }
}
