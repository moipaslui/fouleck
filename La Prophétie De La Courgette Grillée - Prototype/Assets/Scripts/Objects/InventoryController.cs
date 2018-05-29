using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour
{
    private EventSystem ES;

    private void Start()
    {
        ES = FindObjectOfType<EventSystem>();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Clear"))
        {
            Item selectedItem = ES.currentSelectedGameObject.GetComponent<InventorySlot>().item;
            Inventory.instance.Remove(selectedItem);
        }
    }
}
