using UnityEngine;

public class CraftMenu : MonoBehaviour
{
    public GameObject craftMenu;

    private CraftSlot[] craftSlots;

    private void Start()
    {
        craftSlots = craftMenu.GetComponentsInChildren<CraftSlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Craft"))
        {
            if(craftMenu.activeSelf)
            {
                Time.timeScale = 1;
                craftMenu.SetActive(false);
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
}
