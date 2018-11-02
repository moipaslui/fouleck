using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Seller : Interactable
{
    [Header("Seller")]

    public List<Item> itemsToSell;
    public int currentMoney;

    [Header("UI")]
    
    public GameObject sellerMenu;
    public GameObject slotsPanel;
    public TextMeshProUGUI TMP;


    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    override public void Interact()
    {
        base.Interact();
        
        sellerMenu.SetActive(true);
        RefreshUI();
    }

    override public void EndOfInteraction()
    {
        sellerMenu.SetActive(false);

        base.EndOfInteraction();
    }

    public void SellItem()
    {
        Item i = FindObjectOfType<EventSystem>().currentSelectedGameObject.GetComponent<SellSlot>().itemToBuy;
        if(Inventory.instance.items.Count < 5 && MoneyManager.instance.currentMoney >= i.cost)
        {
            itemsToSell.Remove(i);
            Inventory.instance.Add(i);
            MoneyManager.instance.AddMoney(-i.cost);
            currentMoney += i.cost;
            RefreshUI();
            anim.SetTrigger("Give");
        }
    }

    /*public bool BuyItem(Item item)
    {
        if (!Inventory.instance.items.Contains(item) || itemsToSell.Count >= 9 || currentMoney < item.cost)
        {
            return false;
        }
        else
        {
            itemsToSell.Add(item);
            Inventory.instance.Remove(item, false);
            MoneyManager.instance.currentMoney += item.cost;
            currentMoney -= item.cost;
            return true;
        }
    }*/

    private void RefreshUI()
    {
        TMP.SetText("Vendeur : " + currentMoney + "$");

        List <SellSlot> sellSlots = new List<SellSlot>();
        slotsPanel.GetComponentsInChildren<SellSlot>(false, sellSlots);

        for(int i = 0; i < sellSlots.Count; i++)
        {
            if (i < itemsToSell.Count)
                sellSlots[i].itemToBuy = itemsToSell[i];
            else
                sellSlots[i].itemToBuy = null;
            sellSlots[i].RefreshUI();
        }
    }
}
