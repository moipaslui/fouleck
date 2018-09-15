using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    private EventSystem ES;
    private InventoryUI IUI;

    private void Start()
    {
        ES = FindObjectOfType<EventSystem>();
        IUI = FindObjectOfType<InventoryUI>();
    }

    void Update ()
    {
        if (ES.currentSelectedGameObject)
        {
            if (Input.GetButtonDown("Clear"))
            {
                if (ES.currentSelectedGameObject.GetComponent<InventorySlot>())
                {
                    Item selectedItem = ES.currentSelectedGameObject.GetComponent<InventorySlot>().item;
                    Inventory.instance.Remove(selectedItem);
                    IUI.UpdateUI();
                }
            }
            else if (Input.GetButtonDown("CraftClick"))
            {
                if (ES.currentSelectedGameObject.GetComponent<CraftSlot>())
                {
                    Craft craft = ES.currentSelectedGameObject.GetComponent<CraftSlot>().craft;
                    FindObjectOfType<CraftManager>().CraftItem(craft);
                    FindObjectOfType<CraftMenu>().RefreshUI();
                }
            }
        }
    }
}
