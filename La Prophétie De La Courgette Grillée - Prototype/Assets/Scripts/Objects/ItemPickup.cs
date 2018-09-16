using UnityEngine;

[ExecuteInEditMode]
public class ItemPickup : Interactable
{
    [Header("Item")]
    public Item item;

    /* --- EXECUTED IN EDITOR --- */

    private void Update()
    {
        if (item != null && !Application.isPlaying)
        {
            Refresh();
        }
    }

    /* -------------------------- */

    public override void Interact()
    {
        base.Interact();

        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }

    public void changeItem(Item item)
    {
        this.item = item;
        Refresh();
    }

    private void Refresh()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
        offsetIcon = item.offsetIcon;
        IsometricObject iso = GetComponent<IsometricObject>();
        iso.RefreshSprite();
        iso.floorHeight = item.floorHeight;
        iso.PlaceInZ();
    }
}
