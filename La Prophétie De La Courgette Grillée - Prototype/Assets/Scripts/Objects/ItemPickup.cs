using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : Interactable
{
    [Header("Item")]
    public Item itemToInventory;

    public override void Interact()
    {
        base.Interact();

        Debug.Log("Picking up " + itemToInventory.name);

        bool wasPickedUp = Inventory.instance.Add(itemToInventory);

        if(wasPickedUp)
            Destroy(gameObject);
    }
}
