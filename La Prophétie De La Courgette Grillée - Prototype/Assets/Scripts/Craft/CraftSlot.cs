using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Craft craft;
    public Image icon;
    public Image cross;

    private Button button;

    void Start()
    {
        icon.sprite = craft.craftResult.icon;
        button = GetComponent<Button>();
    }

    public void refreshUI()
    {
        if(FindObjectOfType<CraftManager>().isCraftable(craft))
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
