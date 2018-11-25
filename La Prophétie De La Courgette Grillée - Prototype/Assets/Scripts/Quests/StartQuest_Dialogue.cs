using UnityEngine;

public class StartQuest_Dialogue : QuestTrigger_Dialogue
{
    public override void Trigger()
    {
        GameManager.questManager.FindQuest(this).StartQuest();

        base.Trigger();
    }
}
