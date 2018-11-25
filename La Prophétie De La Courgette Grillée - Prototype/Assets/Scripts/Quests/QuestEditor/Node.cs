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

    public List<Connector> connectActive;
    public List<Connector> connectDesactive;

    public Node(QuestTrigger questTrigger)
    {
        trigger = questTrigger;
        ChangeTitle(questTrigger.GetType());
        id = lastId++;
        rect = new Rect(100, 100, 120, 50);
        QuestEditor.nodes.Add(this);
        QuestEditor.selectedNode = id;
        connectActive = new List<Connector>();
        connectDesactive = new List<Connector>();
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
        connectActive = new List<Connector>();
        connectDesactive = new List<Connector>();
    }

    public void DisplayNode(int id)
    {
        GUI.DragWindow();
        GUILayout.Label(title, guiStyle);
        if (Event.current.type == EventType.MouseUp)
        {
            QuestEditor.selectedNode = id;
        }
        ChangeTitle(trigger.GetType());
    }

    public void CreateConnections()
    {
        foreach (QuestTrigger trig in trigger.triggersToActive)
        {
            Node victim = FindNode(trig);
            if(victim != null)
                connectActive.Add(new Connector(this, victim, true));
        }

        foreach (QuestTrigger trig in trigger.triggersToDesactive)
        {
            Node victim = FindNode(trig);
            if (victim != null && victim != this)
                connectDesactive.Add(new Connector(this, victim, false));
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
        title = "";

        if (QuestEditor.selectedNode == id)
            title += "* ";

        if (type == QuestEditor.TranslateType(TRIGGER_TYPES.DIALOGUE))
            title += "Dialogue";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.ITEM_PICKUP))
            title += "Item Pickup";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.BUY))
            title += "Buy";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.CRAFT))
            title += "Craft";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.START))
            title += "Start Dialogue";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.END))
            title += "End Dialogue";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.AT_REMOVE_ITEM))
            title += "Remove Item";
        else if (type == QuestEditor.TranslateType(TRIGGER_TYPES.AT_REWARD))
            title += "Reward";
        else
            title = "Trigger";

        if (QuestEditor.selectedNode == id)
            title += " *";
    }

    public void DestroyNode()
    {
        Object.DestroyImmediate(trigger);
    }

    public Node FindNode(QuestTrigger trigger)
    {
        foreach(Node node in QuestEditor.nodes)
        {
            if (node.trigger == trigger)
                return node;
        }
        return null;
    }


    public void DrawConnections()
    {
        for (int i = 0; i < connectActive.Count; i++)
        {
            if (!connectActive[i].Draw(true))
            {
                connectActive.Remove(connectActive[i]);
                i--;
            }
        }

        for(int i = 0; i < connectDesactive.Count; i++)
        {
            if (!connectDesactive[i].Draw(false))
            {
                connectDesactive.Remove(connectDesactive[i]);
                i--;
            }
        }
    }

    public void DestroyConnector(int victim)
    {
        for(int i = 0; i < connectActive.Count; i++)
        {
            if (connectActive[i].nodeVictim == victim)
            {
                trigger.triggersToActive.Remove(NodeAtID(victim).trigger);
                connectActive.RemoveAt(i);
                return;
            }
        }

        for (int i = 0; i < connectDesactive.Count; i++)
        {
            if (connectDesactive[i].nodeVictim == victim)
            {
                trigger.triggersToDesactive.Remove(NodeAtID(victim).trigger);
                connectDesactive.RemoveAt(i);
                return;
            }
        }
    }

    #region static_functions

    public static Node NodeAtID(int id)
    {
        foreach (Node node in QuestEditor.nodes)
        {
            if (node.id == id)
                return node;
        }
        return null;
    }

    public static void LoadNodes(Quest quest)
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

    #endregion
}
