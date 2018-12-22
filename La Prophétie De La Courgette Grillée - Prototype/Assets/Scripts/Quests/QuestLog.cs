using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public GameObject title;
    public GameObject panel;

    private QuestSlot[] questSlots;

    void Start()
    {
        title.SetActive(false);
        panel.SetActive(false);

        questSlots = panel.GetComponentsInChildren<QuestSlot>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Quest"))
        {
            if(panel.activeSelf)
            {
                panel.SetActive(false);
                title.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
                title.SetActive(true);
                UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        int i = 0;
        foreach (Quest quest in GameManager.questManager.quests)
        {
            if(quest.isActive)
            {
                if (i < 3)
                {
                    questSlots[i].gameObject.SetActive(true);
                    questSlots[i].quest = quest.questData;
                    questSlots[i].UpdateUI();
                    i++;
                }
            }
        }
        for(; i < 3; i++)
        {
            questSlots[i].gameObject.SetActive(false);
        }
    }
}
