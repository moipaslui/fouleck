using UnityEngine;

public class ActionTrigger : QuestTrigger
{
    public override void ActiveTrigger()
    {
        base.ActiveTrigger();

        Trigger();

        base.DesactiveTrigger();
    }
}
