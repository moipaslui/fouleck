using UnityEngine;
using UnityEngine.EventSystems;

public class CraftMenu : MonoBehaviour
{
    public GameObject craftMenu;
    public GameObject craftBases;
    public GameObject craftPizzas;
    public GameObject craftAmeliorations;
    public GameObject AmeliorationsMenu;
    public GameObject AmeliorationsPizzas;

    private CraftSlot[] craftSlots;
    private CraftSlot[] ameliorationCraftSlots;

    private void Start()
    {
        craftBases.SetActive(true);
        craftPizzas.SetActive(true);

        ameliorationCraftSlots = AmeliorationsPizzas.GetComponentsInChildren<CraftSlot>();
        craftSlots = GetComponentsInChildren<CraftSlot>();
        RefreshUI();
        
        craftBases.SetActive(false);
        craftPizzas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Craft"))
        {
            Time.timeScale = 1;
            if (craftMenu.activeSelf)
            {
                craftMenu.SetActive(false);
            }
            else if (craftBases.activeSelf)
            {
                craftBases.SetActive(false);
            }
            else if (craftPizzas.activeSelf)
            {
                craftPizzas.SetActive(false);
            }
            else if (craftAmeliorations.activeSelf)
            {
                craftAmeliorations.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                craftMenu.SetActive(true);

                RefreshUI();
            }
        }
    }

    public void RefreshUI()
    {
        foreach (CraftSlot cs in craftSlots)
        {
            cs.RefreshUI();
        }

        foreach(CraftSlot cs in ameliorationCraftSlots)
        {
            cs.RefreshUI();
        }
    }

    public void CraftClick()
    {
        Craftable itemToCraft = FindObjectOfType<EventSystem>().currentSelectedGameObject.GetComponent<CraftSlot>().itemToCraft;
        GameManager.inventory.CraftItem(itemToCraft);
        RefreshUI();
    }

    public void GoCraftMenu()
    {
        craftMenu.SetActive(true);
        craftBases.SetActive(false);
        craftPizzas.SetActive(false);
        craftAmeliorations.SetActive(false);
        RefreshUI();
    }

    public void GoCraftBases()
    {
        craftMenu.SetActive(false);
        craftBases.SetActive(true);
        RefreshUI();
    }

    public void GoCraftPizzas()
    {
        craftMenu.SetActive(false);
        craftPizzas.SetActive(true);
        RefreshUI();
    }

    public void GoCraftAmeliorations()
    {
        craftMenu.SetActive(false);
        craftAmeliorations.SetActive(true);
        AmeliorationsMenu.SetActive(true);
        RefreshUI();
    }

    public void SetAmeliorationBasilique(Repas pizza)
    {
        ameliorationCraftSlots[0].itemToCraft = pizza;
        ameliorationCraftSlots[0].RefreshUI();
    }

    public void SetAmeliorationOeuf(Repas pizza)
    {
        ameliorationCraftSlots[1].itemToCraft = pizza;
        ameliorationCraftSlots[1].RefreshUI();
    }

    public void SetAmeliorationMiel(Repas pizza)
    {
        ameliorationCraftSlots[2].itemToCraft = pizza;
        ameliorationCraftSlots[2].RefreshUI();
    }

    public void GoAmeliorationsPizzas()
    {
        AmeliorationsMenu.SetActive(false);
    }
}
