using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public int loot = 1;

    public void Drop(Item itemPopping, Transform trans)
    {
        if (itemPopping != null)
        {
            for(int i=0; i < loot; i++)
            {
                GameObject clone = Instantiate(itemPrefab, new Vector2(trans.position.x + ((float)i - (float)loot / 2f), trans.position.y), trans.rotation);
                clone.GetComponent<ItemOnObject>().ChangeItem(itemPopping);
            }
        }
    }
}
