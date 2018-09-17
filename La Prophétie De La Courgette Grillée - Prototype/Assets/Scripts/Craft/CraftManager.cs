using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public bool isCraftable(Craftable itemToCraft)
    {
        foreach (Item item in itemToCraft.craftNeed)
        {
            if(!Inventory.instance.ItemExists(item))
            {
                Debug.Log("Missing required items");
                return false;
            }
        }
        return true;
    }

    public void CraftItem(Craftable itemToCraft)
    {
        if(isCraftable(itemToCraft))
        {
            foreach(Item item in itemToCraft.craftNeed)
            {
                Inventory.instance.Remove(item, false);
            }
            
            Inventory.instance.Add(itemToCraft);
        }
    }
}
