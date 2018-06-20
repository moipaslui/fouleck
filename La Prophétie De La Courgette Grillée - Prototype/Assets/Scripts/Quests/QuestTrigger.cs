public class QuestTrigger : Interactable
{
    public Quest quest;

    private void TriggerQuest()
    {
        if (!quest.isStarted)
            quest.StartQuest();
        else
            quest.EndQuest();
    }
	
	public override void Interact()
    {
        base.Interact();
        TriggerQuest();
    }
}
