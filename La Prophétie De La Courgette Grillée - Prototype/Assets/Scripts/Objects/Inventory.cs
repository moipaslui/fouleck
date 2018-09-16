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

    public GameObject itemPrefab;

    public int space = 5;
    public int spacePerSlot = 10;
    public List<Item> items = new List<Item>();
    public List<int> countItems = new List<int>();

    public GameObject itemGameobject;

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

    public void Remove(Item item, bool instanciate)
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

                if (itemPos != -1) // On équilibre aves les autres slots contenants le même Item
                {
                    countItems[itemPos] += 1;
                }

                itemPos = i;
            }
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        if (instanciate)
        {
            GameObject clone = Instantiate(itemGameobject, FindObjectOfType<PlayerControllerIsometric>().transform.position, FindObjectOfType<PlayerControllerIsometric>().transform.rotation);
            clone.GetComponent<ItemPickup>().changeItem(item);
        }
    }

    public bool ItemExists(Item item) // Si il y a au moins nb fois l'item, on return true
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (item == items[i])
            {
                return true;
            }
        }
        return false;
    }
}
