using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class QuestEditor : EditorWindow
{
    public QuestTrigger newTrigger;
    TriggerEditor triggerEditor;

    private bool hasOpennedTriggerEditor;

    public static QuestEditor ShowWindow()
    {
        QuestEditor questEditor = GetWindow<QuestEditor>();
        return questEditor;
    }

    void OnGUI()
    {
        newTrigger = (QuestTrigger)EditorGUILayout.ObjectField("questTrigger", newTrigger, typeof(QuestTrigger), true);
        if (newTrigger != null && !hasOpennedTriggerEditor)
        {
            triggerEditor = TriggerEditor.ShowWindow(newTrigger);
            hasOpennedTriggerEditor = true;
        }
        else
        {
            if (triggerEditor != null)
                triggerEditor.Close();
            hasOpennedTriggerEditor = false;
        }
    }

    private void OnDestroy()
    {
        if(triggerEditor != null)
            triggerEditor.Close();
    }
}