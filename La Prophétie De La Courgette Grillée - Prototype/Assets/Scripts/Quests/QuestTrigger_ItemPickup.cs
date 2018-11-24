﻿using UnityEngine;

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

        // A modifier pour ajouter dans les objets de quêtes
        GameManager.inventory.Add(GetComponent<ItemOnObject>().item);

        Destroy(gameObject);
    }
}
