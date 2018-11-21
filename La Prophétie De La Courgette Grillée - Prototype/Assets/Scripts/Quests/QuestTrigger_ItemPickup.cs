using UnityEngine;

[RequireComponent(typeof(ItemOnObject))]
public class QuestTrigger_ItemPickup : QuestTrigger
{
    override public void Trigger()
    {
        base.Trigger();

        // A modifier pour ajouter dans les objets de quêtes
        bool wasPickedUp = GameManager.inventory.Add(GetComponent<ItemOnObject>().item);


        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
