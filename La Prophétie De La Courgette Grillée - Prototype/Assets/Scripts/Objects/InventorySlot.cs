using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public GameObject countItem;
    public Item item;
    private int count;

    public void AddItem(Item newItem, int newCount)
    {
        item = newItem;
        count = newCount;

        icon.sprite = item.icon;
        icon.enabled = true;

        countItem.SetActive(true);
        countItem.GetComponentInChildren<Text>().text = "" + count;

        GetComponent<Button>().interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        count = 0;

        icon.sprite = null;
        icon.enabled = false;

        countItem.SetActive(false);
        countItem.GetComponentInChildren<Text>().text = "0";

        GetComponent<Button>().interactable = false;
    }
}
