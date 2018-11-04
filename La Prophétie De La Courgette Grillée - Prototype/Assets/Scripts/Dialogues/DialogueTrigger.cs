public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;

    public override bool Interact()
    {
        if (!base.Interact())
            return false;

        if(GameManager.dialogueManager.ShowDialogue(dialogue))
        {
            EndOfInteraction();
        }

        return true;
    }

    public override void EndOfInteraction()
    {
        base.EndOfInteraction();

        GameManager.dialogueManager.EndDialogue();
    }
}
