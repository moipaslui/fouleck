using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : Interactable
{
    [Header("Quest Trigger")]
    public bool isInteractable = true;
    public List<QuestTrigger> triggersToDesactive = new List<QuestTrigger>();
    public List<QuestTrigger> triggersToActive = new List<QuestTrigger>();

    private Interactable oldActive;

    private void Start()
    {
        if(!isInteractable && !IsInteractableLayer())
            ChangeInteractable(false);
    }

    public override bool Interact()
    {
        // Si l'objet est interactible, on déclenche le trigger
        if (isInteractable)
        {
            if (!base.Interact())
                return false;

            Trigger();

            return true;
        }

        return false;
    }

    public override void ChangeActivation(bool newIsActive)
    {
        if (isInteractable)
            base.ChangeActivation(newIsActive);
        else
        {
            isActive = newIsActive;
            this.enabled = newIsActive;
        }
    }
    
    public virtual void Trigger()
    {
        // On désactive les triggers à désactiver
        foreach (QuestTrigger questTrigger in triggersToDesactive)
        {
            questTrigger.DesactiveTrigger();
        }

        // On active les triggers suivants
        foreach (QuestTrigger questTrigger in triggersToActive)
        {
            questTrigger.ActiveTrigger();
        }
    }

    public virtual void ActiveTrigger()
    {
        // On cherche l'interactable qui est activé
        foreach (Interactable inter in GetComponents<Interactable>())
        {
            if (inter.isActive)
            {
                // On le desactive
                inter.ChangeActivation(false);
                oldActive = inter;
            }
        }
        
        // On active le trigger
        ChangeActivation(true);
    }

    public virtual void DesactiveTrigger()
    {
        // On désactive le trigger
        ChangeActivation(false);
        
        EndOfInteraction();

        // On active celui qui l'était avant
        if (oldActive != null)
            oldActive.ChangeActivation(true);
    }
}
