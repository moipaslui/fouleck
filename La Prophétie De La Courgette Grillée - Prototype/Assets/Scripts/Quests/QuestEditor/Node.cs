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
        rect = new Rect(100, 100, 150, 100);
    }

    public Node(GameObject gameObject, System.Type type)
    {
        trigger = (QuestTrigger)gameObject.AddComponent(type);
        ChangeTitle(trigger.GetType());
        id = lastId++;
        rect = new Rect(100, 100, 150, 60);
    }


    public static Node NodeAtID(List<Node> nodes, int id)
    {
        foreach(Node node in nodes)
        {
            if(node.id == id)
                return node;
        }
        return null;
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
        QuestTrigger temp = (QuestTrigger)trigger.gameObject.AddComponent(type);
        Object.DestroyImmediate(trigger);
        trigger = temp;
        ChangeTitle(type);
    }

    public void ChangeGameObject(GameObject gameObject)
    {
        QuestTrigger temp = (QuestTrigger)gameObject.AddComponent(trigger.GetType());
        Object.DestroyImmediate(trigger);
        trigger = temp;
    }

    private void ChangeTitle(System.Type type)
    {
        if (type == typeof(QuestTrigger_Dialogue))
            title = "Dialogue";
        else if (type == typeof(QuestTrigger_ItemPickup))
            title = "Item Pickup";
        else if (type == typeof(QuestTrigger_Buy))
            title = "Buy";
        else if (type == typeof(QuestTrigger_Craft))
            title = "Craft";
        else
            title = "Trigger";
    }

    public void DestroyNode()
    {
        Object.DestroyImmediate(trigger);
    }
}
