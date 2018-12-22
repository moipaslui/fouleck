using UnityEngine;
using TMPro;

public class QuestSlot : MonoBehaviour
{
    public QuestData quest;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public void UpdateUI()
    {
        title.text = quest.title;
        description.text = quest.description;
    }
}
