using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteraction : Interactable
{
    public override bool Interact()
    {
        if (!base.Interact())
            return false;

        GameManager.saveManager.SaveData();

        return true;
    }
}
