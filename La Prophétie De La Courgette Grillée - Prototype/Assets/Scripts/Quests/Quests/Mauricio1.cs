using UnityEngine;

public class Mauricio1 : Quest
{
    public ItemPickup BoiteAOutil;

    public Dialogue startDialogue;
    public Dialogue endDialogue;

    public void Start()
    {
        BoiteAOutil.gameObject.layer = 0;
    }

    public override void StartQuest()
    {
        base.StartQuest();
        BoiteAOutil.gameObject.layer = 11;
        StartDialogue(startDialogue);
    }

    public override void EndQuest()
    {
        Inventory invent = FindObjectOfType<Inventory>();
        if (invent.items.Contains(BoiteAOutil.item))
        {
            base.EndQuest();
            invent.Remove(BoiteAOutil.item);
            StartDialogue(endDialogue);
        }
    }

    private void StartDialogue(Dialogue dialogue)
    {
        DialogueManager DM = FindObjectOfType<DialogueManager>();
        if (!DM.isDialoguing)
        {
            if (DM.StartDialogue(dialogue))
            {
                DM.isDialoguing = false;
            }
            else
            {
                DM.isDialoguing = true;
            }
        }
        else
        {
            if (DM.DisplayNextSentence())
            {
                DM.isDialoguing = false;
            }
        }
    }
}
