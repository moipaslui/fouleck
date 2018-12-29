public class EndQuestAT : ActionTrigger
{
    public Quest quest;

    public override void Trigger()
    {
        quest.isActive = false;
    }
}
