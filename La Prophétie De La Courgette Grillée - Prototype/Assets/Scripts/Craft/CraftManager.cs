using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public bool isCraftable(Craft c)
    {
        foreach (Ingredient ingredient in c.craftNeeds)
        {
            if(!Inventory.instance.ItemExists(ingredient))
            {
                Debug.Log("Missing required ingredients");
                return false;
            }
        }
        return true;
    }

    public void CraftItem(Craft c)
    {
        if(isCraftable(c))
        {
            foreach(Ingredient ingredient in c.craftNeeds)
            {
                Inventory.instance.Remove(ingredient);
            }
            
            Inventory.instance.Add(c.craftResult);
        }
    }
}
