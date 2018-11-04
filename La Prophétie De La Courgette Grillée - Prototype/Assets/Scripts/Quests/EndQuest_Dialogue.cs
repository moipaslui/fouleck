public class EndQuest_Dialogue : QuestTrigger_Dialogue
{
    public Quest quest;

    public override void Trigger()
    {
        base.Trigger();

        if (!GameManager.dialogueManager.isDialoguing)
        {
            quest.EndQuest();
        }
    }
}
