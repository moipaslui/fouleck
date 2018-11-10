using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerEditor : EditorWindow
{
    // Common objects
    public QuestTrigger trigger;
    public bool isActive;
    private GameObject gameObject;
    private bool isInteractable;
    private Vector2 offsetIcon;

    // Specific trigger
    private Dialogue dialogue;
    private Item itemToPickup;
    private List<Item> itemsToBuy;
    private List<Item> itemsToCraft;

    public static TriggerEditor ShowWindow(QuestTrigger questTrigger)
    {
        TriggerEditor triggerEditor = GetWindow<TriggerEditor>();
        triggerEditor.trigger = questTrigger;
        return triggerEditor;
    }

    void OnGUI()
    {
        if(trigger != null)
        {
            GetTriggerData();
            DisplayData();
            UpdateData();
        }
    }

    #region Data

    void GetTriggerData()
    {
        // Common objects
        gameObject = trigger.gameObject;
        isActive = trigger.isActive;
        isInteractable = trigger.isInteractable;
        offsetIcon = trigger.offsetIcon;

        // Specific trigger
        if (trigger.GetType() == typeof(QuestTrigger_Dialogue) || trigger.GetType() == typeof(StartQuest_Dialogue) || trigger.GetType() == typeof(EndQuest_Dialogue))
            dialogue = ((QuestTrigger_Dialogue)trigger).dialogue;
        else if (trigger.GetType() == typeof(QuestTrigger_ItemPickup))
            itemToPickup = trigger.GetComponent<ItemOnObject>().item;
        else if (trigger.GetType() == typeof(QuestTrigger_Buy))
            itemsToBuy = ((QuestTrigger_Buy)trigger).itemsToBuy;
        else if (trigger.GetType() == typeof(QuestTrigger_Craft))
            itemsToCraft = ((QuestTrigger_Craft)trigger).itemsToCraft;
    }

    #region Displays

    void DisplayData()
    {
        // Common objects
        gameObject = (GameObject)EditorGUILayout.ObjectField("Porteur du trigger", gameObject, typeof(GameObject), true);
        isActive = EditorGUILayout.Toggle("is Active", isActive);
        isInteractable = EditorGUILayout.BeginToggleGroup("is Interactable", isInteractable);
            offsetIcon = EditorGUILayout.Vector2Field("offsetIcon", offsetIcon);
        EditorGUILayout.EndToggleGroup();

        // Specific trigger
        if (trigger.GetType() == typeof(QuestTrigger_Dialogue) || trigger.GetType() == typeof(StartQuest_Dialogue) || trigger.GetType() == typeof(EndQuest_Dialogue))
            DisplayDialogue();
        else if (trigger.GetType() == typeof(QuestTrigger_ItemPickup))
            itemToPickup = (Item)EditorGUILayout.ObjectField("Item to pickup", itemToPickup, typeof(Item), false);
        else if (trigger.GetType() == typeof(QuestTrigger_Buy))
            DisplayItemsToBuy();
        else if (trigger.GetType() == typeof(QuestTrigger_Craft))
            DisplayItemsToCraft();
    }

    void DisplayDialogue()
    {
        GUILayout.Label("Dialogue");
        for(int i = 0; i < dialogue.sentences.Count; i++)
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
        if(trigger.gameObject != gameObject)
        {
            DestroyImmediate(trigger);
            trigger = (QuestTrigger)gameObject.AddComponent(trigger.GetType());
        }
        trigger.isActive = isActive;
        trigger.isInteractable = isInteractable;
        trigger.offsetIcon = offsetIcon;

        // Specific trigger
        if (trigger.GetType() == typeof(QuestTrigger_Dialogue) || trigger.GetType() == typeof(StartQuest_Dialogue) || trigger.GetType() == typeof(EndQuest_Dialogue))
            ((QuestTrigger_Dialogue)trigger).dialogue = dialogue;
        else if (trigger.GetType() == typeof(QuestTrigger_ItemPickup))
            trigger.GetComponent<ItemOnObject>().item = itemToPickup;
        else if (trigger.GetType() == typeof(QuestTrigger_Buy))
            ((QuestTrigger_Buy)trigger).itemsToBuy = itemsToBuy;
        else if (trigger.GetType() == typeof(QuestTrigger_Craft))
            ((QuestTrigger_Craft)trigger).itemsToCraft = itemsToCraft;
    }

    #endregion
}
