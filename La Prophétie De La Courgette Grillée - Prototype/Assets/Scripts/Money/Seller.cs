using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Seller : Interactable
{
    [Header("Seller")]

    public List<Item> itemsToSell;
    public int currentMoney;

    [Header("Dialogue Before Selling")]
    public Dialogue dialogue;

    [Header("UI")]
    
    public GameObject sellerMenu;
    public GameObject slotsPanel;
    public TextMeshProUGUI TMP;

    private bool isMenuOpen;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    override public bool Interact()
    {
        if (!isMenuOpen)
        {
            if (!base.Interact())
                return false;

            if (GameManager.dialogueManager.ShowDialogue(dialogue))
            {
                Time.timeScale = 0;
                sellerMenu.SetActive(true);
                isMenuOpen = true;
                RefreshUI();
            }
        }
        else
        {
            Time.timeScale = 1;
            EndOfInteraction();
            isMenuOpen = false;
        }

        return true;
    }

    override public void EndOfInteraction()
    {
        GameManager.dialogueManager.EndDialogue();
        sellerMenu.SetActive(false);

        base.EndOfInteraction();
    }

    public void SellItem()
    {
        Item i = FindObjectOfType<EventSystem>().currentSelectedGameObject.GetComponent<SellSlot>().itemToBuy;
        if(GameManager.inventory.items.Count < 5 && GameManager.moneyManager.currentMoney >= i.cost)
        {
            itemsToSell.Remove(i);
            GameManager.inventory.Add(i);
            GameManager.moneyManager.AddMoney(-i.cost);
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
