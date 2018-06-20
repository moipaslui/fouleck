using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    
    public void TriggerDialogue()
    {
        DialogueManager DM = FindObjectOfType<DialogueManager>();

        if(!DM.isDialoguing)
        {
            if(DM.StartDialogue(dialogue))
            {
                DM.isDialoguing = false;
                NPC npc = GetComponent<NPC>();
                if (npc != null)
                {
                    npc.canMove = true;
                }
            }
            else
            {
                DM.isDialoguing = true;
                NPC npc = GetComponent<NPC>();
                if(npc != null)
                {
                    npc.canMove = false;
                }
            }
        }
        else
        {
            if(DM.DisplayNextSentence())
            {
                DM.isDialoguing = false;
                NPC npc = GetComponent<NPC>();
                if (npc != null)
                {
                    npc.canMove = true;
                }
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        TriggerDialogue();
    }
}
