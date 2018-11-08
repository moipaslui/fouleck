using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class QuestEditor : EditorWindow
{
    public Quest quest;
    public QuestTrigger startTrigger;
    public bool active;
    public bool interactable;
    public Vector2 offset;
    public List<QuestTrigger> triggersDesactive;
    public List<QuestTrigger> triggersActive;
    public QuestTrigger newTriggerDesactive;
    public QuestTrigger newTriggerActive;

    [MenuItem("Window/Quest Editor")]
    static void ShowWindow()
    {
        GetWindow<QuestEditor>();
    }

    void OnGUI()
    {
        quest = (Quest)EditorGUILayout.ObjectField("Quest", quest, typeof(Quest), true);
        if (quest)
        {
            if (quest.startQuestTrigger != null)
            {
                GrabData();
                ReferenceData();
                UpdateData();
            }
        }
    }

    void GrabData()
    {
        startTrigger = quest.startQuestTrigger;
        active = quest.startQuestTrigger.isActive;
        offset = quest.startQuestTrigger.offsetIcon;
        interactable = quest.startQuestTrigger.isInteractable;
        triggersDesactive = quest.startQuestTrigger.triggersToDesactive;
        triggersActive = quest.startQuestTrigger.triggersToActive;
    }

    void ReferenceData()
    {
        startTrigger = (QuestTrigger)EditorGUILayout.ObjectField("QuestTrigger", startTrigger, typeof(QuestTrigger), true);
        active = EditorGUILayout.Toggle("is Active", active);
        offset = EditorGUILayout.Vector2Field("offsetIcon", offset);
        interactable = EditorGUILayout.Toggle("is Interactable", interactable);
        TriggersDesactive();
        TriggersActive();
    }

    void UpdateData()
    {
        quest.startQuestTrigger = startTrigger;
        quest.startQuestTrigger.isActive = active;
        quest.startQuestTrigger.offsetIcon = offset;
        quest.startQuestTrigger.isInteractable = interactable;
        quest.startQuestTrigger.triggersToDesactive = triggersDesactive;
        quest.startQuestTrigger.triggersToActive = triggersActive;
    }

    void TriggersDesactive()
    {
        EditorGUILayout.LabelField("Triggers to desactive : " + triggersDesactive.Count);
        for (int i = 0; i < triggersDesactive.Count; i++)
        {
            triggersDesactive[i] = (QuestTrigger)EditorGUILayout.ObjectField(triggersDesactive[i], typeof(QuestTrigger), true);
            if (triggersDesactive[i] == null)
            {
                triggersDesactive.RemoveAt(i);
                i--;
            }
        }
        newTriggerDesactive = (QuestTrigger)EditorGUILayout.ObjectField(null, typeof(QuestTrigger), true);
        if (newTriggerDesactive)
        {
            quest.startQuestTrigger.triggersToDesactive.Add(newTriggerDesactive);
        }
    }

    void TriggersActive()
    {
        EditorGUILayout.LabelField("Triggers to active : " + triggersActive.Count);
        for (int i = 0; i < triggersActive.Count; i++)
        {
            triggersActive[i] = (QuestTrigger)EditorGUILayout.ObjectField(triggersActive[i], typeof(QuestTrigger), true);
            if (triggersActive[i] == null)
            {
                triggersActive.RemoveAt(i);
                i--;
            }
        }
        newTriggerActive = (QuestTrigger)EditorGUILayout.ObjectField(null, typeof(QuestTrigger), true);
        if (newTriggerActive)
        {
            quest.startQuestTrigger.triggersToActive.Add(newTriggerActive);
        }
    }
}