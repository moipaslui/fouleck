using UnityEngine;
using UnityEditor;
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
    AT_REWARD = 8,
    AT_END_QUEST = 9,
    AT_INSTANTIATE = 10,
    AT_ACTIVE_ENNEMI = 11
};

public class QuestEditor : EditorWindow
{
    public static Quest selectedQuest;
    public static List<Node> nodes;
    public static int selectedNode;
    private int oldSelectedNode = -1;
    private static TriggerEditor triggerEditor;
    public static int line = 0;

    private bool newActive;
    private bool newDesactive;
    private bool destroy;
    private bool newActiveLine;
    private bool newDesactiveLine;
    private bool destroyLine;

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
                Node nodeToDestroy = Node.NodeAtID(selectedNode);
                selectedQuest.questTriggers.Remove(nodeToDestroy.trigger);
                nodes.Remove(nodeToDestroy);
                foreach(Node node in nodes)
                {
                    node.trigger.triggersToActive.Remove(nodeToDestroy.trigger);
                    node.trigger.triggersToDesactive.Remove(nodeToDestroy.trigger);
                    if(node.trigger.GetType() == typeof(QuestTrigger_Dialogue) || node.trigger.GetType() == typeof(StartQuest_Dialogue) || node.trigger.GetType() == typeof(EndQuest_Dialogue))
                    {
                        foreach(QuestDialogueData triggers in ((QuestTrigger_Dialogue)node.trigger).dialogue.triggers)
                        {
                            triggers.triggersToActiveToAdd.Remove(nodeToDestroy.trigger);
                            triggers.triggersToDesactiveToAdd.Remove(nodeToDestroy.trigger);

                            if (triggers.triggersToDesactiveToAdd.Count == 0 && triggers.triggersToActiveToAdd.Count == 0)
                                ((QuestTrigger_Dialogue)node.trigger).dialogue.triggers.Remove(triggers);
                        }
                    }
                }
                nodeToDestroy.DestroyNode();
                selectedNode = -1;
            }
            else
            {
                Debug.Log("Aucun trigger n'est selectionné. Il faut cliquer sur droite et gauche en même temps pour séléctionner un trigger.");
            }
        }

        GUILayout.Space(10);

        if(newActive = GUILayout.Toggle(newActive, "Active", EditorStyles.toolbarButton))
        {
            newDesactive = false;
            destroy = false;
            newActiveLine = false;
            newDesactiveLine = false;

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
            newActiveLine = false;
            newDesactiveLine = false;

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
            newActiveLine = false;
            newDesactiveLine = false;

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

        GUILayout.Space(20);

        if (GUILayout.Button("-", EditorStyles.toolbarButton))
        {
            line--;
        }

        GUILayout.Label("" + line);

        if(GUILayout.Button("+", EditorStyles.toolbarButton))
        {
            line++;
        }

        if (newActiveLine = GUILayout.Toggle(newActiveLine, "Active (Line)", EditorStyles.toolbarButton))
        {
            newActive = false;
            newDesactive = false;
            destroy = false;
            newDesactiveLine = false;

            if (selectedNode == -1)
            {
                newActiveLine = false;
                oldSelectedNode = -1;
            }
            else
            {
                if (oldSelectedNode == -1)
                    oldSelectedNode = selectedNode;
                else if(Node.NodeAtID(oldSelectedNode).trigger.GetType() != typeof(QuestTrigger_Dialogue) && Node.NodeAtID(oldSelectedNode).trigger.GetType() != typeof(StartQuest_Dialogue) && Node.NodeAtID(oldSelectedNode).trigger.GetType() != typeof(EndQuest_Dialogue))
                {
                    newActiveLine = false;
                    oldSelectedNode = -1;
                }
                else if (oldSelectedNode != selectedNode)
                {
                    bool hasBeenAdded = false;
                    foreach (QuestDialogueData qdd in ((QuestTrigger_Dialogue)Node.NodeAtID(oldSelectedNode).trigger).dialogue.triggers)
                    {
                        if (qdd.line == line)
                        {
                            qdd.triggersToActiveToAdd.Add(Node.NodeAtID(selectedNode).trigger);
                            hasBeenAdded = true;
                        }
                    }
                    if (!hasBeenAdded)
                        ((QuestTrigger_Dialogue)Node.NodeAtID(oldSelectedNode).trigger).dialogue.triggers.Add(new QuestDialogueData(Node.NodeAtID(selectedNode).trigger, null, line));
                    
                    Node.NodeAtID(oldSelectedNode).connectActive.Add(new Connector(Node.NodeAtID(oldSelectedNode), Node.NodeAtID(selectedNode), true, line));

                    newActiveLine = false;
                    oldSelectedNode = -1;
                    selectedNode = -1;
                }
            }
        }

        if (newDesactiveLine = GUILayout.Toggle(newDesactiveLine, "Desactive (Line)", EditorStyles.toolbarButton))
        {
            newActive = false;
            newDesactive = false;
            destroy = false;
            newActiveLine = false;

            if (selectedNode == -1)
            {
                newDesactiveLine = false;
                oldSelectedNode = -1;
            }
            else
            {
                if (oldSelectedNode == -1)
                    oldSelectedNode = selectedNode;
                else if (Node.NodeAtID(oldSelectedNode).trigger.GetType() != typeof(QuestTrigger_Dialogue) && Node.NodeAtID(oldSelectedNode).trigger.GetType() != typeof(StartQuest_Dialogue) && Node.NodeAtID(oldSelectedNode).trigger.GetType() != typeof(EndQuest_Dialogue))
                {
                    newActiveLine = false;
                    oldSelectedNode = -1;
                }
                else if (oldSelectedNode != selectedNode)
                {
                    bool hasBeenAdded = false;
                    foreach (QuestDialogueData qdd in ((QuestTrigger_Dialogue)Node.NodeAtID(oldSelectedNode).trigger).dialogue.triggers)
                    {
                        if (qdd.line == line)
                        {
                            qdd.triggersToDesactiveToAdd.Add(Node.NodeAtID(selectedNode).trigger);
                            hasBeenAdded = true;
                        }
                    }
                    if (!hasBeenAdded)
                        ((QuestTrigger_Dialogue)Node.NodeAtID(oldSelectedNode).trigger).dialogue.triggers.Add(new QuestDialogueData(null, Node.NodeAtID(selectedNode).trigger, line));

                    Node.NodeAtID(oldSelectedNode).connectDesactive.Add(new Connector(Node.NodeAtID(oldSelectedNode), Node.NodeAtID(selectedNode), true, line));

                    newDesactiveLine = false;
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
                
            case TRIGGER_TYPES.AT_END_QUEST:
                return typeof(EndQuestAT);
                
            case TRIGGER_TYPES.AT_INSTANTIATE:
                return typeof(InstantiateAT);

            case TRIGGER_TYPES.AT_ACTIVE_ENNEMI:
                return typeof(ActiveEnnemiAT);
        }

        return null;
    }

    void DisplayQuests()
    {
        selectedQuest = (Quest)EditorGUILayout.ObjectField("Quest", selectedQuest, typeof(Quest), true);
    }
}