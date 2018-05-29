using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class NPC : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
