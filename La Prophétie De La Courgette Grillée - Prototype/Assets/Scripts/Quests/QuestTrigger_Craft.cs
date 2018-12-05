using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Craft : QuestTrigger
{
    public List<Item> itemsToCraft = new List<Item>();

    public override void ActiveTrigger()
    {
        base.ActiveTrigger();
        FindObjectOfType<CraftMenu>().isQuest = true;
    }

    public override void DesactiveTrigger()
    {
        base.DesactiveTrigger();
        FindObjectOfType<CraftMenu>().isQuest = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Craft"))
        {
            bool hasAllCrafted = true;
            foreach(Item item in itemsToCraft)
            {
                if(!GameManager.inventory.Contains(item, isQuestItem:true))
                {
                    hasAllCrafted = false;
                }
            }

            if(hasAllCrafted)
                Trigger();
        }
    }
}
