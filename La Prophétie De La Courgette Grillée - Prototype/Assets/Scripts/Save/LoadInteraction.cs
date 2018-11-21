using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInteraction : Interactable
{
    public override bool Interact()
    {
        if (!base.Interact())
            return false;
        GameManager.saveManager.LoadData();
        return true;
    }
}
	