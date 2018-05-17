using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found !");
            return;
        }

        instance = this;
    }

    #endregion

    public List<Item> items = new List<Item>();
    public List<int> countItems = new List<int>();
    public int space = 5;
    public int spacePerSlot = 10;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(item == items[i])
            {
                if (countItems[i] < spacePerSlot)
                {
                    countItems[i] += 1;

                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();

                    return true;
                }
            }
        }

        if (items.Count < space)
        {
            items.Add(item);
            countItems.Add(1);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            return true;
        }
        else
        {
            Debug.Log("Not enough room in the inventory.");
            return false;
        }
    }

    public void Remove(Item item)
    {
        int itemPos = -1;
        for (int i = 0; i < items.Count; i++)
        {
            if (item == items[i])
            {
                if (countItems[i] > 1)
                {
                    countItems[i] -= 1;
                }
                else
                {
                    items.RemoveAt(i);
                    countItems.RemoveAt(i);
                }

                if (itemPos != -1) // On équilibre aves les autres slots contennants le même Item
                {
                    countItems[itemPos] += 1;
                }

                itemPos = i;
            }
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
