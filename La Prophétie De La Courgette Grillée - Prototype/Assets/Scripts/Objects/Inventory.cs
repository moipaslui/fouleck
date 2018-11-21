using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class ItemInventory
{
    public Item item;
    public int count;

    public ItemInventory(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}

public class Inventory : MonoBehaviour
{
    public GameObject itemPrefab;

    public int space = 5;
    public int spacePerSlot = 10;
    public List<ItemInventory> items = new List<ItemInventory>();

    [Header("Others")]
    public WeaponOnPlayer weaponOnPlayer;
    public GameObject sellerMenu;

    public bool Add(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(item == items[i].item)
            {
                if (items[i].count < spacePerSlot)
                {
                    items[i].count += 1;
                    UpdateUI();
                    return true;
                }
            }
        }

        if (items.Count < space)
        {
            items.Add(new ItemInventory(item, 1));
            UpdateUI();

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
            if (item == items[i].item)
            {
                if (itemPos != -1) // On équilibre aves les autres slots contenants le même Item
                {
                    items[itemPos].count++;
                }

                if (items[i].count > 1)
                {
                    items[i].count -= 1;
                    itemPos = i;
                }
                else
                {
                    items.RemoveAt(i);
                }
            }
        }

        UpdateUI();

        if (instanciate)
        {
            GameObject clone = Instantiate(itemPrefab, FindObjectOfType<PlayerControllerIsometric>().transform.position, FindObjectOfType<PlayerControllerIsometric>().transform.rotation);
            clone.GetComponent<ItemOnObject>().ChangeItem(item);
        }
    }

    public bool Contains(Item item)
    {
        foreach(ItemInventory itemInventory in items)
        {
            if (item == itemInventory.item)
                return true;
        }
        return false;
    }

    public void RemoveAll()
    {
        items.Clear();
        UpdateUI();
    }
    
    public void ClickOnItem()
    {
        Item selectedItem = FindObjectOfType<EventSystem>().currentSelectedGameObject.GetComponent<InventorySlot>().item;

        if (sellerMenu.activeSelf)
        {
            Seller.currentSeller.BuyItem(selectedItem);
        }
        else if(selectedItem.GetType() == typeof(Repas))
        {
            FindObjectOfType<PlayerHealthManager>().MangeRepas((Repas)selectedItem);
            Remove(selectedItem, false);
        }
        else if(selectedItem.GetType() == typeof(Weapon))
        {
            weaponOnPlayer.ChangeWeapon((Weapon)selectedItem);
            Remove(selectedItem, false);
        }
        else
        {
            Remove(selectedItem, true);
        }
    }

    #region Craft

    public bool IsCraftable(Craftable itemToCraft)
    {
        foreach (Item item in itemToCraft.craftNeed)
        {
            if (!Contains(item))
            {
                return false;
            }
        }
        return true;
    }

    public void CraftItem(Craftable itemToCraft)
    {
        if (IsCraftable(itemToCraft))
        {
            foreach (Item item in itemToCraft.craftNeed)
            {
                Remove(item, false);
            }

            Add(itemToCraft);
        }
    }

    #endregion

    #region UI

    [Header("UI")]
    public Transform slotsPanel;
    private InventorySlot[] slots;

    void Start()
    {
        slots = slotsPanel.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItem(items[i].item, items[i].count);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    #endregion
}
