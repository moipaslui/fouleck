public class EndQuest_Dialogue : QuestTrigger_Dialogue
{
    public override void Trigger()
    {
        base.Trigger();

        if (!GameManager.dialogueManager.isDialoguing)
        {
            GameManager.questManager.FindQuest(this).EndQuest();
        }
    }
}
