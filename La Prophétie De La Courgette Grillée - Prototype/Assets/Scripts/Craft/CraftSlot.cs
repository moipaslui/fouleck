using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Craftable itemToCraft;
    public Image icon;
    public Image cross;

    private Button button;

    void Awake()
    {
        icon.sprite = itemToCraft.icon;
        button = GetComponent<Button>();
    }

    public void refreshUI()
    {
        if(FindObjectOfType<CraftManager>().isCraftable(itemToCraft))
        {
            cross.enabled = false;
            button.interactable = true;
        }
        else
        {
            cross.enabled = true;
            button.interactable = false;
        }
    }
}
