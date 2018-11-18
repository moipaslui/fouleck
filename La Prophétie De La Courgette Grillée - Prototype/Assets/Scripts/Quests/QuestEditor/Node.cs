using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Node
{
    public QuestTrigger trigger;
    public string title = "";
    public int id;
    public Rect rect;
    public bool desactiveSelf;
    private static int lastId = 2;
    public static GUIStyle guiStyle;

    public Node(QuestTrigger questTrigger)
    {
        trigger = questTrigger;
        ChangeTitle(questTrigger.GetType());
        id = lastId++;
        rect = new Rect(100, 100, 120, 50);
        QuestEditor.nodes.Add(this);
        QuestEditor.selectedNode = id;
    }

    public Node(GameObject gameObject, System.Type type)
    {
        trigger = (QuestTrigger)gameObject.AddComponent(type);
        ChangeTitle(trigger.GetType());
        id = lastId++;
        rect = new Rect(100, 100, 120, 50);
        QuestEditor.nodes.Add(this);
        QuestEditor.selectedQuest.questTriggers.Add(trigger);
        QuestEditor.selectedNode = id;
    }
    
    public void DisplayNode(int id)
    {
        GUI.DragWindow();
        GUILayout.Label(title, guiStyle);
        if (Event.current.type == EventType.MouseUp)
        {
            QuestEditor.selectedNode = id;
        }
    }

    public void ChangeScript(System.Type type)
    {
        if (type != QuestEditor.TranslateType(TRIGGER_TYPES.START) && type != QuestEditor.TranslateType(TRIGGER_TYPES.END) &&
            trigger.GetType() != QuestEditor.TranslateType(TRIGGER_TYPES.START) && trigger.GetType() != QuestEditor.TranslateType(TRIGGER_TYPES.END))
        {
            QuestEditor.selectedQuest.questTriggers.Remove(trigger);
            QuestTrigger temp = (QuestTrigger)trigger.gameObject.AddComponent(type);
            Object.DestroyImmediate(trigger);
            trigger = temp;
            ChangeTitle(type);
            QuestEditor.selectedQuest.questTriggers.Add(trigger);
        }
    }

    public void ChangeGameObject(GameObject gameObject)
    {
        QuestEditor.selectedQuest.questTriggers.Remove(trigger);
        QuestTrigger temp = (QuestTrigger)gameObject.AddComponent(trigger.GetType());
        Object.DestroyImmediate(trigger);
        trigger = temp;
        QuestEditor.selectedQuest.questTriggers.Add(trigger);
    }

    private void ChangeTitle(System.Type type)
    {
        if (type == QuestEditor.TranslateType(TRIGGER_TYPES.DIALOGUE))
            title = "Dialogue";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.ITEM_PICKUP))
            title = "Item Pickup";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.BUY))
            title = "Buy";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.CRAFT))
            title = "Craft";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.START))
            title = "Start Dialogue";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.END))
            title = "End Dialogue";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.AT_REMOVE_ITEM))
            title = "Remove Item";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.AT_REWARD))
            title = "Reward";
        else
            title = "Trigger";
    }

    public void DestroyNode()
    {
        Object.DestroyImmediate(trigger);
    }

    

    public static Node NodeAtID(List<Node> nodes, int id)
    {
        foreach (Node node in nodes)
        {
            if (node.id == id)
                return node;
        }
        return null;
    }

    public static void LoadNodes(List<Node> nodes, Quest quest)
    {
        bool hasStartTrigger = false;
        bool hasEndTrigger = false;

        foreach (QuestTrigger trigger in quest.questTriggers)
        {
            new Node(trigger);
            
            if (trigger.GetType() == QuestEditor.TranslateType(TRIGGER_TYPES.START))
                hasStartTrigger = true;
            if (trigger.GetType() == QuestEditor.TranslateType(TRIGGER_TYPES.END))
                hasEndTrigger = true;
        }

        if(!hasStartTrigger)
        {
            Debug.Log("no start trigger");
            new Node(quest.gameObject, QuestEditor.TranslateType(TRIGGER_TYPES.START));
        }
        if (!hasEndTrigger)
        {
            Debug.Log("no end trigger");
            new Node(quest.gameObject, QuestEditor.TranslateType(TRIGGER_TYPES.END));
        }
    }
}
