using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Craft : QuestTrigger
{
    public List<Item> itemsToCraft = new List<Item>();

    private void Update()
    {
        if(Input.GetButtonDown("Craft"))
        {
            bool hasAllCrafted = true;
            foreach(Item item in itemsToCraft)
            {
                if(!GameManager.inventory.items.Contains(item))
                {
                    hasAllCrafted = false;
                }
            }

            if(hasAllCrafted)
                Trigger();
        }
    }
}
