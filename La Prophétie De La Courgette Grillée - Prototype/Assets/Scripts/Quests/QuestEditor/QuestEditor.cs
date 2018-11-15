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
    END = 6
};

public class QuestEditor : EditorWindow
{
    public Quest selectedQuest;
    static List<Node> nodes;
    static List<Connector> connectors;
    public static int selectedNode;
    private bool isAnyNodeSelected;
    private TriggerEditor triggerEditor;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditor>();
    }

    public void OnGUI()
    {
        // First launch
        if(nodes == null)
        {
            nodes = new List<Node>();
            connectors = new List<Connector>();
            triggerEditor = new TriggerEditor();
            Node.guiStyle = new GUIStyle { fontSize = 15, alignment = TextAnchor.MiddleCenter };
            selectedNode = -1;
        }


        if (selectedQuest != null)
        {
            DisplayQuestEditor();
        }
        else
        {
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
            PopupWindow.Show(new Rect(0, 15, 100, 0), new Popup());
        }

        if(GUILayout.Button(" - ", EditorStyles.toolbarButton))
        {
            if (selectedNode != -1)
            {
                Node nodeToDestroy = Node.NodeAtID(nodes, selectedNode);
                nodeToDestroy.DestroyNode();
                nodes.Remove(nodeToDestroy);
                selectedNode = -1;
            }
            else
            {
                Debug.Log("Aucun trigger n'est selectionné. Ne vous fiez pas à l'apparence des blocs et cliquez droit et gauche en même temps pour en selectionner un.");
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
        triggerEditor.Display(Node.NodeAtID(nodes, selectedNode));
        GUI.DragWindow();
    }

    public static void CreateNode(GameObject gameObject, TRIGGER_TYPES triggerType)
    {
        Node nodeToAdd = new Node(gameObject, TranslateType(triggerType));
        nodes.Add(nodeToAdd);
        selectedNode = nodeToAdd.id;
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
        }

        return null;
    }

    void DisplayQuests()
    {
        selectedQuest = (Quest)EditorGUILayout.ObjectField("Quest", selectedQuest, typeof(Quest), true);
    }
}