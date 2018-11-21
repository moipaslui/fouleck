using UnityEngine;

public class QuestTrigger_Craft : QuestTrigger
{
    public Item[] itemsToCraft;

    private void Update()
    {
        if(Input.GetButtonDown("Craft"))
        {
            bool hasAllCrafted = true;
            foreach(Item item in itemsToCraft)
            {
                if(!GameManager.inventory.Contains(item))
                {
                    hasAllCrafted = false;
                }
            }

            if(hasAllCrafted)
                Trigger();
        }
    }
}
