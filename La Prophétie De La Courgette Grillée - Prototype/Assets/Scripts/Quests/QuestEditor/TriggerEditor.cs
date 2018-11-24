using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerEditor
{
    Node node;
    public Rect rect = new Rect(150, 100, 240, 0);
    TRIGGER_TYPES triggerType;

    private bool desactiveSelf;

    public void Display(Node node)
    {
        this.node = node;
        desactiveSelf = node.trigger.triggersToDesactive.Contains(node.trigger);
        DisplayData();
    }
    
    void DisplayData()
    {
        // Common objects
        if(triggerType != (triggerType = (TRIGGER_TYPES)EditorGUILayout.EnumPopup("Type du trigger :", triggerType)))
        {
            node.ChangeScript(QuestEditor.TranslateType(triggerType));
        }
        if (GUILayout.Button("Changer GameObject"))
        {
            node.ChangeGameObject(Selection.activeGameObject);
        }

        node.trigger.isActive = EditorGUILayout.Toggle("is Active", node.trigger.isActive);
        node.trigger.enabled = node.trigger.isActive;

        desactiveSelf = EditorGUILayout.Toggle("Desactive Self", desactiveSelf);

        if(desactiveSelf && !node.trigger.triggersToDesactive.Contains(node.trigger))
        {
            node.trigger.triggersToDesactive.Add(node.trigger);
        }
        else if(!desactiveSelf)
        {
            node.trigger.triggersToDesactive.Remove(node.trigger);
        }

        node.trigger.isInteractable = EditorGUILayout.BeginToggleGroup("is Interactable", node.trigger.isInteractable);
        node.trigger.offsetIcon = EditorGUILayout.Vector2Field("offsetIcon", node.trigger.offsetIcon);
        EditorGUILayout.EndToggleGroup();

        if (node.trigger.tag == "NPC")
            node.trigger.offsetIcon = node.trigger.GetComponent<NPC>().offsetIcon;

        // Specific node.trigger
        if (node.trigger.GetType() == typeof(QuestTrigger_Dialogue) || node.trigger.GetType() == typeof(StartQuest_Dialogue) || node.trigger.GetType() == typeof(EndQuest_Dialogue))
            DisplayDialogue();
        else if (node.trigger.GetType() == typeof(QuestTrigger_ItemPickup))
        {
            node.trigger.GetComponent<ItemOnObject>().item = (Item)EditorGUILayout.ObjectField("Item to pickup", node.trigger.GetComponent<ItemOnObject>().item, typeof(Item), false);
            node.trigger.offsetIcon = node.trigger.GetComponent<ItemOnObject>().item.offsetIcon;
            /*node.trigger.GetComponent<SpriteRenderer>().sprite = node.trigger.GetComponent<ItemOnObject>().item.icon;
            if (node.trigger.GetComponent<ItemPickup>() != null)
                Object.DestroyImmediate(node.trigger.GetComponent<ItemPickup>());*/
        }
        else if (node.trigger.GetType() == typeof(QuestTrigger_Buy))
            DisplayItemsToBuy();
        else if (node.trigger.GetType() == typeof(QuestTrigger_Craft))
            DisplayItemsToCraft();
        else if (node.trigger.GetType() == typeof(RemoveItemAT))
            ((RemoveItemAT)node.trigger).item = (Item)EditorGUILayout.ObjectField("Item to remove", ((RemoveItemAT)node.trigger).item, typeof(Item), false);
        else if (node.trigger.GetType() == typeof(RewardTriggerAT))
            DisplayRewards();
    }

    #region TriggerEditorDisplays

    void DisplayDialogue()
    {
        QuestTrigger_Dialogue triggerD = (QuestTrigger_Dialogue)node.trigger;

        GUILayout.Label("Dialogue");
        for (int i = 0; i < triggerD.dialogue.sentences.Count; i++)
        {
            triggerD.dialogue.sentences[i] = EditorGUILayout.TextArea(triggerD.dialogue.sentences[i]);
            if (triggerD.dialogue.sentences[i] == "" && i == triggerD.dialogue.sentences.Count - 1)
                triggerD.dialogue.sentences.RemoveAt(i);
        }
        string newText = EditorGUILayout.TextArea("");
        if (newText != "")
            triggerD.dialogue.sentences.Add(newText);
        triggerD.dialogue.speaker = EditorGUILayout.TextField("Speaker", triggerD.dialogue.speaker);
    }

    void DisplayItemsToBuy()
    {
        QuestTrigger_Buy triggerB = (QuestTrigger_Buy)node.trigger;

        GUILayout.Label("Items to buy");
        for (int i = 0; i < triggerB.itemsToBuy.Count; i++)
        {
            triggerB.itemsToBuy[i] = (Item)EditorGUILayout.ObjectField(triggerB.itemsToBuy[i], typeof(Item), false);
            if (triggerB.itemsToBuy[i] == null)
                triggerB.itemsToBuy.RemoveAt(i--);
        }
        Item newItem = (Item)EditorGUILayout.ObjectField(null, typeof(Item), false);
        if (newItem != null)
            triggerB.itemsToBuy.Add(newItem);
    }

    void DisplayItemsToCraft()
    {
        QuestTrigger_Craft triggerC = (QuestTrigger_Craft)node.trigger;

        GUILayout.Label("Items to craft");
        for (int i = 0; i < triggerC.itemsToCraft.Count; i++)
        {
            triggerC.itemsToCraft[i] = (Item)EditorGUILayout.ObjectField(triggerC.itemsToCraft[i], typeof(Item), false);
            if (triggerC.itemsToCraft[i] == null)
                triggerC.itemsToCraft.RemoveAt(i--);
        }
        Item newItem = (Item)EditorGUILayout.ObjectField(null, typeof(Item), false);
        if (newItem != null)
            triggerC.itemsToCraft.Add(newItem);
    }

    void DisplayRewards()
    {
        RewardTriggerAT triggerR = ((RewardTriggerAT)node.trigger);

        GUILayout.Label("Money Reward");
        ((RewardTriggerAT)node.trigger).money = EditorGUILayout.IntField(((RewardTriggerAT)node.trigger).money);
        
        GUILayout.Label("Exp Reward");
        ((RewardTriggerAT)node.trigger).exp = EditorGUILayout.IntField(((RewardTriggerAT)node.trigger).exp);

        GUILayout.Label("Items Reward");
        for (int i = 0; i < triggerR.items.Count; i++)
        {
            triggerR.items[i] = (Item)EditorGUILayout.ObjectField(triggerR.items[i], typeof(Item), false);
            if (triggerR.items[i] == null)
                triggerR.items.RemoveAt(i--);
        }
        Item newItem = (Item)EditorGUILayout.ObjectField(null, typeof(Item), false);
        if (newItem != null)
            triggerR.items.Add(newItem);
    }

    #endregion
}
