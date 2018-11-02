using UnityEngine;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour
{
    public Item itemToBuy;
    public Image icon;
    public Image cross;
    public Image bigCross;

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
            if (MoneyManager.instance.IsBuyable(itemToBuy))
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
            icon.color = new Color(0, 0, 0, 0);
            button = GetComponent<Button>();
            cross.enabled = false;
            button.interactable = false;
        }
    }
}
