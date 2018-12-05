using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Seller : Interactable
{
    [Header("Seller")]

    [HideInInspector]
    public static Seller currentSeller;
    public List<Item> itemsToSell;
    public float currentMoney;
    [Range(0, 1)]
    public float buyMargin = 0.9f;

    [Header("Dialogue Before Selling")]
    public Dialogue dialogue;

    [Header("UI")]
    
    public GameObject sellerMenu;
    public GameObject slotsPanel;
    public TextMeshProUGUI TMP;

    [HideInInspector]
    public bool isNextInteractionQuest;

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
                currentSeller = this;
                isMenuOpen = true;
                RefreshUI();
            }
        }
        else
        {
            EndOfInteraction();
        }

        return true;
    }

    override public void EndOfInteraction()
    {
        isNextInteractionQuest = false;
        Time.timeScale = 1;
        GameManager.dialogueManager.EndDialogue();
        sellerMenu.SetActive(false);
        currentSeller = null;
        isMenuOpen = false;

        base.EndOfInteraction();
    }

    public void SellItem()
    {
        Item i = FindObjectOfType<EventSystem>().currentSelectedGameObject.GetComponent<SellSlot>().itemToBuy;
        if(GameManager.inventory.items.Count < 5 && GameManager.moneyManager.currentMoney >= i.cost)
        {
            itemsToSell.Remove(i);
            GameManager.inventory.Add(i, isNextInteractionQuest);
            GameManager.moneyManager.AddMoney(-i.cost);
            currentMoney += i.cost;
            RefreshUI();
            anim.SetTrigger("Give");
        }
    }

    public bool BuyItem(Item item)
    {
        if (!GameManager.inventory.Contains(item) || itemsToSell.Count >= 9 || currentMoney < item.cost * buyMargin || isNextInteractionQuest)
        {
            return false;
        }
        else
        {
            itemsToSell.Add(item);
            GameManager.inventory.Remove(item, false);
            GameManager.moneyManager.AddMoney(item.cost * buyMargin);
            currentMoney -= item.cost * buyMargin;
            RefreshUI();
            anim.SetTrigger("Give");
            return true;
        }
    }

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
