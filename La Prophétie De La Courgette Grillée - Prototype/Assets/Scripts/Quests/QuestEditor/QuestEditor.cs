using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class QuestEditor : EditorWindow
{
    public Quest selectedQuest;
    List<Node> nodes;
    List<Connector> connectors;

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditor>();
    }

    public void OnGUI()
    {
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



        ShowEverything();
    }

    private void ShowEverything()
    {
        BeginWindows();


        EndWindows();

        DisplayToolbars();
    }

    private void DisplayToolbars()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);


        GUILayout.Label("Quête : " + selectedQuest.questData.title);

        if(GUILayout.Button(" + ", EditorStyles.toolbarButton))
        {
            // When click Add
        }

        if(GUILayout.Button(" - ", EditorStyles.toolbarButton))
        {
            // When click Remove
        }

        GUILayout.FlexibleSpace();

        if(GUILayout.Button("Revenir aux quêtes", EditorStyles.toolbarButton))
        {
            selectedQuest = null;
        }


        GUILayout.EndHorizontal();
    }





    void DisplayQuests()
    {
        selectedQuest = (Quest)EditorGUILayout.ObjectField("Quest", selectedQuest, typeof(Quest), true);
    }

    /*public Quest selectedQuest;

    private Rect triggerEditorRect = new Rect(100, 100, 250, 250);

    [MenuItem("Window/Quest Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuestEditor>();
    }

    private void OnGUI()
    {
        if (selectedQuest != null)
        {
            DisplayQuestEditor();
        }
        else
        {
            DisplayQuests();
        }
    }

    #region Quests

    private void DisplayQuests()
    {
        selectedQuest = (Quest)EditorGUILayout.ObjectField("Quest", selectedQuest, typeof(Quest), true);
    }

    #endregion

    #region QuestEditor

    public static Node selectedNode;
    private List<Node> nodes = new List<Node>();
    private Vector2 nodePosition = new Vector2(100, 100);

    private void DisplayQuestEditor()
    {
        BeginWindows();

        // Display nodes
        foreach(Node node in nodes)
        {
            //node.Paint();
        }

        // Display trigger editor
        if (selectedNode != null)
        {
            triggerEditorRect = GUILayout.Window(1, triggerEditorRect, DisplayTriggerEditor, "Trigger Editor");
        }

        EndWindows();

        DisplayToolbars();

        // Temporary
        if(selectedNode != null)
            selectedNode.trigger = (QuestTrigger)EditorGUILayout.ObjectField("Quest Trigger", selectedNode.trigger, typeof(QuestTrigger), true);
        // --
    }

    private void DisplayToolbars()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);


        GUILayout.Label("Quête : " + selectedQuest.questData.title);

        if(GUILayout.Button(" + ", EditorStyles.toolbarButton))
        {
            nodes.Add(new Node(nodePosition, selectedQuest));
        }

        if(GUILayout.Button(" - ", EditorStyles.toolbarButton) && selectedNode != null && selectedNode.GetType() != typeof(StartQuest_Dialogue) && selectedNode.GetType() != typeof(EndQuest_Dialogue))
        {
            nodes.Remove(selectedNode);
            Destroy(selectedNode);
        }

        GUILayout.FlexibleSpace();

        if(GUILayout.Button("Revenir aux quêtes", EditorStyles.toolbarButton))
        {
            selectedQuest = null;
        }


        GUILayout.EndHorizontal();
    }

    #region TriggerEditor

    // Common objects
    public bool isActive;
    private GameObject gameObject;
    private bool isInteractable;
    private Vector2 offsetIcon;

    // Specific trigger
    private Dialogue dialogue;
    private Item itemToPickup;
    private List<Item> itemsToBuy;
    private List<Item> itemsToCraft;

    private void DisplayTriggerEditor(int id)
    {
        GUI.DragWindow();
        GetTriggerData();
        DisplayData();
        UpdateData();
    }

    void GetTriggerData()
    {
        // Common objects
        gameObject = selectedNode.trigger.gameObject;
        isActive = selectedNode.trigger.isActive;
        isInteractable = selectedNode.trigger.isInteractable;
        offsetIcon = selectedNode.trigger.offsetIcon;

        // Specific trigger
        if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Dialogue) || selectedNode.trigger.GetType() == typeof(StartQuest_Dialogue) || selectedNode.trigger.GetType() == typeof(EndQuest_Dialogue))
            dialogue = ((QuestTrigger_Dialogue)selectedNode.trigger).dialogue;
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_ItemPickup))
            itemToPickup = selectedNode.trigger.GetComponent<ItemOnObject>().item;
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Buy))
            itemsToBuy = ((QuestTrigger_Buy)selectedNode.trigger).itemsToBuy;
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Craft))
            itemsToCraft = ((QuestTrigger_Craft)selectedNode.trigger).itemsToCraft;
    }

    #region TriggerEditorDisplays

    void DisplayData()
    {
        // Common objects
        gameObject = (GameObject)EditorGUILayout.ObjectField("Porteur du trigger", gameObject, typeof(GameObject), true);
        isActive = EditorGUILayout.Toggle("is Active", isActive);
        isInteractable = EditorGUILayout.BeginToggleGroup("is Interactable", isInteractable);
        offsetIcon = EditorGUILayout.Vector2Field("offsetIcon", offsetIcon);
        EditorGUILayout.EndToggleGroup();

        // Specific trigger
        if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Dialogue) || selectedNode.trigger.GetType() == typeof(StartQuest_Dialogue) || selectedNode.trigger.GetType() == typeof(EndQuest_Dialogue))
            DisplayDialogue();
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_ItemPickup))
            itemToPickup = (Item)EditorGUILayout.ObjectField("Item to pickup", itemToPickup, typeof(Item), false);
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Buy))
            DisplayItemsToBuy();
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Craft))
            DisplayItemsToCraft();
    }

    void DisplayDialogue()
    {
        GUILayout.Label("Dialogue");
        for (int i = 0; i < dialogue.sentences.Count; i++)
        {
            dialogue.sentences[i] = EditorGUILayout.TextArea(dialogue.sentences[i]);
            if (dialogue.sentences[i] == "" && i == dialogue.sentences.Count - 1)
                dialogue.sentences.RemoveAt(i);
        }
        string newText = EditorGUILayout.TextArea("");
        if (newText != "")
            dialogue.sentences.Add(newText);
        dialogue.speaker = EditorGUILayout.TextField("Speaker", dialogue.speaker);
    }

    void DisplayItemsToBuy()
    {
        GUILayout.Label("Items to buy");
        for (int i = 0; i < itemsToBuy.Count; i++)
        {
            itemsToBuy[i] = (Item)EditorGUILayout.ObjectField(itemsToBuy[i], typeof(Item), false);
            if (itemsToBuy[i] == null)
                itemsToBuy.RemoveAt(i--);
        }
        Item newItem = (Item)EditorGUILayout.ObjectField(null, typeof(Item), false);
        if (newItem != null)
            itemsToBuy.Add(newItem);
    }

    void DisplayItemsToCraft()
    {
        GUILayout.Label("Items to craft");
        for (int i = 0; i < itemsToCraft.Count; i++)
        {
            itemsToCraft[i] = (Item)EditorGUILayout.ObjectField(itemsToCraft[i], typeof(Item), false);
            if (itemsToCraft[i] == null)
                itemsToCraft.RemoveAt(i--);
        }
        Item newItem = (Item)EditorGUILayout.ObjectField(null, typeof(Item), false);
        if (newItem != null)
            itemsToCraft.Add(newItem);
    }

    #endregion

    void UpdateData()
    {
        // Common objects
        if (selectedNode.trigger.gameObject != gameObject)
        {
            DestroyImmediate(selectedNode.trigger);
            selectedNode.trigger = (QuestTrigger)gameObject.AddComponent(selectedNode.trigger.GetType());
            selectedNode.ChangeScript(selectedNode.trigger, selectedQuest);
        }
        selectedNode.trigger.isActive = isActive;
        selectedNode.trigger.isInteractable = isInteractable;
        selectedNode.trigger.offsetIcon = offsetIcon;

        // Specific trigger
        if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Dialogue) || selectedNode.trigger.GetType() == typeof(StartQuest_Dialogue) || selectedNode.trigger.GetType() == typeof(EndQuest_Dialogue))
            ((QuestTrigger_Dialogue)selectedNode.trigger).dialogue = dialogue;
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_ItemPickup))
            selectedNode.trigger.GetComponent<ItemOnObject>().item = itemToPickup;
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Buy))
            ((QuestTrigger_Buy)selectedNode.trigger).itemsToBuy = itemsToBuy;
        else if (selectedNode.trigger.GetType() == typeof(QuestTrigger_Craft))
            ((QuestTrigger_Craft)selectedNode.trigger).itemsToCraft = itemsToCraft;
    }

    #endregion

    #endregion*/
}