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

    public void LoadQuests(List<bool> activeQuests, List<List<bool>> triggers)
    {
        for(int i = 0; i < quests.Count; i++)
        {
            quests[i].isActive = activeQuests[i];
            
            for(int y = 0; y < quests[i].questTriggers.Count; y++)
            {
                if (quests[i].questTriggers[y] != null)
                {
                    if (triggers[i][y] == true)
                        quests[i].questTriggers[y].ActiveTrigger();
                    else
                        quests[i].questTriggers[y].DesactiveTrigger();
                }
            }
        }
    }
}
