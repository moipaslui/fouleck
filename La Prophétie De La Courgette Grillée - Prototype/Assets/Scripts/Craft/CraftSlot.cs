using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Craftable itemToCraft;
    public Image icon;
    public Image cross;

    private Button button;

    public void RefreshUI()
    {
        icon.sprite = itemToCraft.icon;
        button = GetComponent<Button>();
        if (GameManager.inventory.IsCraftable(itemToCraft) || GameManager.inventory.IsCraftable(itemToCraft, isQuestItem:true))
        {
            if(cross)
            {
                cross.enabled = false;
            }
            button.interactable = true;
        }
        else
        {
            if(cross)
            {
                cross.enabled = true;
            }
            button.interactable = false;
        }
    }
}
