﻿using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotsPanel;

    private Inventory inventory;
    private InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = slotsPanel.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i], inventory.countItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
