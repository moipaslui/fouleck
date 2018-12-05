using UnityEngine;

[RequireComponent(typeof(ItemOnObject))]
public class QuestTrigger_ItemPickup : QuestTrigger
{
    public override void ActiveTrigger()
    {
        base.ActiveTrigger();
        base.ChangeInteractable(true);
    }

    override public void Trigger()
    {
        base.Trigger();
        
        bool wasPickedUp = GameManager.inventory.Add(GetComponent<ItemOnObject>().item, isQuestItem:true);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
