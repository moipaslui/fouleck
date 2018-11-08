using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public GameObject itemPrefab;

    public int space = 5;
    public int spacePerSlot = 10;
    public List<Item> items = new List<Item>();
    public List<int> countItems = new List<int>();

    [Header("Others")]
    public WeaponOnPlayer weaponOnPlayer;
    public GameObject sellerMenu;

    public bool Add(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(item == items[i])
            {
                if (countItems[i] < spacePerSlot)
                {
                    countItems[i] += 1;

                    return true;
                }
            }
        }

        if (items.Count < space)
        {
            items.Add(item);
            countItems.Add(1);

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

        UpdateUI();

        if (instanciate)
        {
            GameObject clone = Instantiate(itemPrefab, FindObjectOfType<PlayerControllerIsometric>().transform.position, FindObjectOfType<PlayerControllerIsometric>().transform.rotation);
            clone.GetComponent<ItemOnObject>().ChangeItem(item);
        }
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
            if (!items.Contains(item))
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
                slots[i].AddItem(items[i], countItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    #endregion
}
