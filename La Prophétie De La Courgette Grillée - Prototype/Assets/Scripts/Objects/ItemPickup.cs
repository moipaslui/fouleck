using UnityEngine;

[ExecuteInEditMode]
public class ItemPickup : Interactable
{
    [Header("Item")]
    public Item item;
    
    /* --- EXECUTED IN EDITOR --- */

    private void Update()
    {
        if(item != null && !Application.isPlaying)
        {
            GetComponent<SpriteRenderer>().sprite = item.icon;
            offsetIcon = item.offsetIcon;
            IsometricObject iso = GetComponent<IsometricObject>();
            iso.floorHeight = item.floorHeight;
            iso.PlaceInZ();
        }
    }

    /* -------------------------- */

    public override void Interact()
    {
        base.Interact();

        Debug.Log("Picking up " + item.name);

        bool wasPickedUp = Inventory.instance.Add(item);

        if(wasPickedUp)
            Destroy(gameObject);
    }
}
