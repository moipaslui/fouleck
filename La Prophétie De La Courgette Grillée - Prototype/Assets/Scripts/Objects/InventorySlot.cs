using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public GameObject countItem;
    public Item item;
    public SlotInfos slotInfos;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && slotInfos != null)
        {
            slotInfos.DisplayInfos(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(slotInfos != null)
            slotInfos.HideInfos();
    }
}
