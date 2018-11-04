using UnityEngine;

public class StartQuest_Dialogue : QuestTrigger_Dialogue
{
    public Quest quest;

    public override void Trigger()
    {
        quest.StartQuest();

        base.Trigger();
    }
}
