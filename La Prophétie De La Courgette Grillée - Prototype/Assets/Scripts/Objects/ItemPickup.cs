using UnityEngine;

[RequireComponent(typeof(IsometricObject))]
[RequireComponent(typeof(ItemOnObject))]
public class ItemPickup : Interactable
{    
    public override bool Interact()
    {
        if (!base.Interact())
            return false;

        bool wasPickedUp = GameManager.inventory.Add(GetComponent<ItemOnObject>().item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }

        return true;
    }
}
