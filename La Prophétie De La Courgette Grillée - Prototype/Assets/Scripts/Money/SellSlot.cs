using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item itemToBuy;
    public Image icon;
    public Image cross;
    public Image bigCross;
    public SlotInfos slotInfos;

    [HideInInspector]
    public Button button;

    public void RefreshUI()
    {
        if (itemToBuy != null)
        {
            bigCross.enabled = false;
            icon.sprite = itemToBuy.icon;
            icon.color = new Color(1, 1, 1, 1);
            button = GetComponent<Button>();
            if (GameManager.moneyManager.IsBuyable(itemToBuy))
            {
                if (cross)
                {
                    cross.enabled = false;
                }
                button.interactable = true;
            }
            else
            {
                if (cross)
                {
                    cross.enabled = true;
                }
                button.interactable = false;
            }
        }
        else
        {
            bigCross.enabled = true;
            icon.sprite = null;
            icon.color = new Color(0, 0, 0, 0);
            button = GetComponent<Button>();
            cross.enabled = false;
            button.interactable = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemToBuy != null)
        {
            slotInfos.DisplayInfos(itemToBuy);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotInfos.HideInfos();
    }
}
