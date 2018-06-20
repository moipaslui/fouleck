using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool isStarted;

    public virtual void StartQuest()
    {
        isStarted = true;
        // Add quest to questList
    }

    public virtual void RefreshQuest()
    {
        // Refresh datas
    }

    public virtual void EndQuest()
    {
        // Remove quest from questList
    }
}
