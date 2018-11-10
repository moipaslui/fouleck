using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuestsWindow : EditorWindow
{
    public Quest quest;

    public QuestEditor questEditor;

    private bool hasOpennedQuestEditor;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestsWindow>();
    }

    private void OnGUI()
    {
        quest = (Quest)EditorGUILayout.ObjectField("Quest", quest, typeof(Quest), true);
        if (quest != null && !hasOpennedQuestEditor)
        {
            questEditor = QuestEditor.ShowWindow();
            hasOpennedQuestEditor = true;
        }
        else
        {
            if(questEditor != null)
                questEditor.Close();
            hasOpennedQuestEditor = false;
        }
    }
}
