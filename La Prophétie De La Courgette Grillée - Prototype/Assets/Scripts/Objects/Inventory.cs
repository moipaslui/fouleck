using System;
using System.Collections;
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
    public List<ItemInventory> questItems = new List<ItemInventory>();

    [Header("Others")]
    public WeaponOnPlayer weaponOnPlayer;
    public GameObject sellerMenu;

    public bool Add(Item item, bool isQuestItem = false)
    {
        List<ItemInventory> tempItems = items;
        if (isQuestItem)
            tempItems = questItems;

        foreach(ItemInventory itemInventory in tempItems)
        {
            if(item == itemInventory.item)
            {
                if (itemInventory.count < spacePerSlot)
                {
                    itemInventory.count += 1;
                    UpdateUI(isQuestItem);
                    return true;
                }
            }
        }

        if (tempItems.Count < space)
        {
            tempItems.Add(new ItemInventory(item, 1));
            UpdateUI(isQuestItem);

            return true;
        }
        else
        {
            Debug.Log("Not enough room in the inventory.");
            return false;
        }
    }

    public void Remove(Item item, bool instanciate, bool isQuestItem = false)
    {
        List<ItemInventory> tempItems = items;
        if (isQuestItem)
            tempItems = questItems;

        int itemPos = -1;
        for (int i = 0; i < tempItems.Count; i++)
        {
            if (item == tempItems[i].item)
            {
                if (itemPos != -1) // On équilibre aves les autres slots contenants le même Item
                {
                    tempItems[itemPos].count++;
                }

                if (tempItems[i].count > 1)
                {
                    tempItems[i].count -= 1;
                    itemPos = i;
                }
                else
                {
                    tempItems.RemoveAt(i);
                }
            }
        }

        UpdateUI(isQuestItem);

        if (instanciate)
        {
            GameObject clone = Instantiate(itemPrefab, FindObjectOfType<PlayerControllerIsometric>().transform.position, FindObjectOfType<PlayerControllerIsometric>().transform.rotation);
            clone.GetComponent<ItemOnObject>().ChangeItem(item);
        }
    }

    public bool Contains(Item item, bool isQuestItem = false)
    {
        List<ItemInventory> tempItems = items;
        if (isQuestItem)
            tempItems = questItems;

        foreach (ItemInventory itemInventory in tempItems)
        {
            if (item == itemInventory.item)
                return true;
        }
        return false;
    }

    public void RemoveAll(bool isQuestItem = false)
    {
        items.Clear();
        UpdateUI(isQuestItem);
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
            GetComponent<BuffManager>().MangerRepas((Repas)selectedItem);
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

    public bool IsCraftable(Craftable itemToCraft, bool isQuestItem = false)
    {
        foreach (Item item in itemToCraft.craftNeed)
        {
            if (!Contains(item, isQuestItem))
            {
                return false;
            }
        }
        return true;
    }

    public void CraftItem(Craftable itemToCraft)
    {
        bool isQuest;

        if (IsCraftable(itemToCraft))
            isQuest = false;
        else if (IsCraftable(itemToCraft, isQuestItem: true))
            isQuest = true;
        else
            return;

        foreach (Item item in itemToCraft.craftNeed)
        {
            Remove(item, false, isQuest);
        }

        Add(itemToCraft, isQuest);
    }

    #endregion

    #region UI

    [Header("UI")]
    public RectTransform slotsPanel;
    private InventorySlot[] slots;
    public RectTransform questSlotsPanel;
    private InventorySlot[] questSlots;

    void Start()
    {
        slots = slotsPanel.GetComponentsInChildren<InventorySlot>();
        questSlots = questSlotsPanel.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI(bool isQuestItem)
    {
        List<ItemInventory> tempItems = items;
        InventorySlot[] tempSlots = slots;
        if (isQuestItem)
        {
            tempItems = questItems;
            tempSlots = questSlots;
        }

        for (int i = 0; i < tempSlots.Length; i++)
        {
            if (i < tempItems.Count)
            {
                tempSlots[i].AddItem(tempItems[i].item, tempItems[i].count);
            }
            else
            {
                tempSlots[i].ClearSlot();
            }
        }

        if(isQuestItem)
        {
            StartCoroutine(QuestSlotsPanelMove());
        }
    }

    private IEnumerator QuestSlotsPanelMove()
    {
        while (Mathf.Abs(questSlotsPanel.localPosition.x - (-55 * questItems.Count)) > 5f)
        {
            if(questSlotsPanel.localPosition.x < -55 * questItems.Count)
                questSlotsPanel.localPosition = new Vector3(questSlotsPanel.localPosition.x + Time.deltaTime * 100, 0);
            else
                questSlotsPanel.localPosition = new Vector3(questSlotsPanel.localPosition.x - Time.deltaTime * 100, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
}
