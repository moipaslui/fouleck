using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    public Quest FindQuest(QuestTrigger trigger)
    {
        foreach(Quest quest in quests)
        {
            if (quest.questTriggers.Contains(trigger))
                return quest;
        }
        return null;
    }
}
