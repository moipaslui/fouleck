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
    private bool wasActive;

    private void Start()
    {
        craftSlots = GetComponentsInChildren<CraftSlot>();
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
            cs.refreshUI();
        }
    }

    public void CraftClick()
    {
        Craftable itemToCraft = FindObjectOfType<EventSystem>().currentSelectedGameObject.GetComponent<CraftSlot>().itemToCraft;
        Inventory.instance.CraftItem(itemToCraft);
        RefreshUI();
    }

    public void GoCraftMenu()
    {
        craftMenu.SetActive(true);
        craftBases.SetActive(false);
        craftPizzas.SetActive(false);
        craftAmeliorations.SetActive(false);
    }

    public void GoCraftBases()
    {
        craftMenu.SetActive(false);
        craftBases.SetActive(true);
    }

    public void GoCraftPizzas()
    {
        craftMenu.SetActive(false);
        craftPizzas.SetActive(true);
    }

    public void GoCraftAmeliorations()
    {
        craftMenu.SetActive(false);
        craftAmeliorations.SetActive(true);
    }

    public void GoAmeliorationsPizzas()
    {
        AmeliorationsMenu.SetActive(false);
        AmeliorationsPizzas.SetActive(true);
    }
}
