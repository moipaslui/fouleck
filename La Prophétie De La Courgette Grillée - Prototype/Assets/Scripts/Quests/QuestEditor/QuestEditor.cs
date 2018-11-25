using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public enum TRIGGER_TYPES
{
    BUY = 0,
    CRAFT = 1,
    DIALOGUE = 2,
    ITEM_PICKUP = 3,
    ZONE = 4,
    START = 5,
    END = 6,
    AT_REMOVE_ITEM = 7,
    AT_REWARD = 8
};

public class QuestEditor : EditorWindow
{
    public static Quest selectedQuest;
    public static List<Node> nodes;
    public static int selectedNode;
    private int oldSelectedNode = -1;
    private static TriggerEditor triggerEditor;

    private bool newActive;
    private bool newDesactive;
    private bool destroy;

    [MenuItem("Quest Editor/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditor>().Show();

        nodes = new List<Node>();
        triggerEditor = new TriggerEditor();
        Node.guiStyle = new GUIStyle { fontSize = 15, alignment = TextAnchor.MiddleCenter };
        selectedNode = -1;
    }

    public void OnGUI()
    {
        // First launch
        if(nodes == null)
        {
            nodes = new List<Node>();
            triggerEditor = new TriggerEditor();
            Node.guiStyle = new GUIStyle { fontSize = 15, alignment = TextAnchor.MiddleCenter };
            selectedNode = -1;
        }


        if (selectedQuest != null)
        {
            if (nodes.Count == 0)
            {
                Node.LoadNodes(selectedQuest);
                foreach(Node node in nodes)
                {
                    node.CreateConnections();
                }
            }
            DisplayQuestEditor();
        }
        else
        {
            nodes.Clear();
            DisplayQuests();
        }
    }

    void DisplayQuestEditor()
    {
        BeginWindows();
        
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].rect = GUILayout.Window(nodes[i].id, nodes[i].rect, nodes[i].DisplayNode, nodes[i].trigger.gameObject.name);
        }
        foreach(Node node in nodes)
        {
            node.DrawConnections();
        }

        if (selectedNode != -1)
            triggerEditor.rect = GUILayout.Window(1, triggerEditor.rect, DisplayTriggerEditor, "Trigger Editor");

        EndWindows();

        DisplayToolbars();

        // Si tu cliques, aucun node n'est séléctionné
        if (Event.current.type == EventType.MouseDown)
        {
            selectedNode = -1;
        }
    }

    private void DisplayToolbars()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);


        GUILayout.Label("Quête : " + selectedQuest.questData.title);

        if(GUILayout.Button(" + ", EditorStyles.toolbarButton))
        {
            PopupWindow.Show(new Rect(0, 15, 100, 0), new PopupCreateTrigger());
        }

        if(GUILayout.Button(" - ", EditorStyles.toolbarButton))
        {
            if (selectedNode != -1)
            {
                if(Node.NodeAtID(selectedNode).trigger.GetType() != TranslateType(TRIGGER_TYPES.START) && Node.NodeAtID(selectedNode).trigger.GetType() != TranslateType(TRIGGER_TYPES.END))
                { 
                    Node nodeToDestroy = Node.NodeAtID(selectedNode);
                    selectedQuest.questTriggers.Remove(nodeToDestroy.trigger);
                    nodes.Remove(nodeToDestroy);
                    foreach(Node node in nodes)
                    {
                        node.trigger.triggersToActive.Remove(nodeToDestroy.trigger);
                        node.trigger.triggersToDesactive.Remove(nodeToDestroy.trigger);
                    }
                    nodeToDestroy.DestroyNode();
                    selectedNode = -1;
                }
                else
                {
                    Debug.Log("Vous ne pouvrez pas supprimer un start ou end trigger.");
                }
            }
            else
            {
                Debug.Log("Aucun trigger n'est selectionné. Ne vous fiez pas à l'apparence des blocs et cliquez droit et gauche en même temps pour en selectionner un.");
            }
        }

        GUILayout.Space(10);

        if(newActive = GUILayout.Toggle(newActive, "Active", EditorStyles.toolbarButton))
        {
            newDesactive = false;
            destroy = false;

            if(selectedNode == -1)
            {
                newActive = false;
                oldSelectedNode = -1;
            }
            else
            {
                if (oldSelectedNode == -1)
                    oldSelectedNode = selectedNode;

                if(oldSelectedNode != selectedNode)
                {
                    Node.NodeAtID(oldSelectedNode).trigger.triggersToActive.Add(Node.NodeAtID(selectedNode).trigger);
                    Node.NodeAtID(oldSelectedNode).connectActive.Add(new Connector(Node.NodeAtID(oldSelectedNode), Node.NodeAtID(selectedNode), true));

                    newActive = false;
                    oldSelectedNode = -1;
                    selectedNode = -1;
                }
            }
        }

        if (newDesactive = GUILayout.Toggle(newDesactive, "Desactive", EditorStyles.toolbarButton))
        {
            newActive = false;
            destroy = false;

            if (selectedNode == -1)
            {
                newDesactive = false;
                oldSelectedNode = -1;
            }
            else
            {
                if (oldSelectedNode == -1)
                    oldSelectedNode = selectedNode;

                if (oldSelectedNode != selectedNode)
                {
                    Node.NodeAtID(oldSelectedNode).trigger.triggersToDesactive.Add(Node.NodeAtID(selectedNode).trigger);
                    Node.NodeAtID(oldSelectedNode).connectDesactive.Add(new Connector(Node.NodeAtID(oldSelectedNode), Node.NodeAtID(selectedNode), false));

                    newDesactive = false;
                    oldSelectedNode = -1;
                    selectedNode = -1;
                }
            }
        }

        if (destroy = GUILayout.Toggle(destroy, "Destroy", EditorStyles.toolbarButton))
        {
            newActive = false;
            newDesactive = false;

            if (selectedNode == -1)
            {
                destroy = false;
                oldSelectedNode = -1;
            }
            else
            {
                if (oldSelectedNode == -1)
                    oldSelectedNode = selectedNode;

                if (oldSelectedNode != selectedNode)
                {
                    Node.NodeAtID(oldSelectedNode).DestroyConnector(selectedNode);

                    destroy = false;
                    oldSelectedNode = -1;
                    selectedNode = -1;
                }

            }
        }

        GUILayout.FlexibleSpace();

        if(GUILayout.Button("Revenir aux quêtes", EditorStyles.toolbarButton))
        {
            selectedQuest = null;
        }


        GUILayout.EndHorizontal();
    }

    public void DisplayTriggerEditor(int id)
    {
        triggerEditor.Display(Node.NodeAtID(selectedNode));
        GUI.DragWindow();
    }

    public static System.Type TranslateType(TRIGGER_TYPES type)
    {
        switch(type)
        {
            case TRIGGER_TYPES.BUY:
                return typeof(QuestTrigger_Buy);

            case TRIGGER_TYPES.CRAFT:
                return typeof(QuestTrigger_Craft);

            case TRIGGER_TYPES.ITEM_PICKUP:
                return typeof(QuestTrigger_ItemPickup);

            case TRIGGER_TYPES.DIALOGUE:
                return typeof(QuestTrigger_Dialogue);

            case TRIGGER_TYPES.ZONE:
                return typeof(QuestTrigger_Zone);
                
            case TRIGGER_TYPES.START:
                return typeof(StartQuest_Dialogue);

            case TRIGGER_TYPES.END:
                return typeof(EndQuest_Dialogue);
                
            case TRIGGER_TYPES.AT_REMOVE_ITEM:
                return typeof(RemoveItemAT);

            case TRIGGER_TYPES.AT_REWARD:
                return typeof(RewardTriggerAT);
        }

        return null;
    }

    void DisplayQuests()
    {
        selectedQuest = (Quest)EditorGUILayout.ObjectField("Quest", selectedQuest, typeof(Quest), true);
    }
}