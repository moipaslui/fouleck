using UnityEngine;

public class QuestTrigger_Zone : QuestTrigger
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.Trigger();
    }
}
