using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Buy : QuestTrigger
{
    private Seller seller;
    private bool wasActive;
    private bool hasBoughtAll;

    public List<Item> itemsToBuy;

    public override void ActiveTrigger()
    {
        seller = GetComponent<Seller>();
        wasActive = seller.isActive;
        foreach(Item item in itemsToBuy)
        {
            seller.itemsToSell.Add(item);
        }

        base.ActiveTrigger();
    }

    public override void Trigger()
    {
        seller = GetComponent<Seller>();
        seller.ChangeActivation(true);
        seller.Interact();

        hasBoughtAll = true;
        foreach(Item item in itemsToBuy)
        {
            if(seller.itemsToSell.Contains(item))
            {
                hasBoughtAll = false;
            }
        }

        if(hasBoughtAll)
            base.Trigger();

        seller.isActive = hasBoughtAll;
    }

    public override void DesactiveTrigger()
    {
        seller = GetComponent<Seller>();

        seller.ChangeActivation(wasActive);

        base.DesactiveTrigger();
    }
}
