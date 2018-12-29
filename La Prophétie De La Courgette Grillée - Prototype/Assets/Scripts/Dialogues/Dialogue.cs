using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestDialogue : Dialogue
{
    public List<QuestDialogueData> triggers = new List<QuestDialogueData>();
}

[Serializable]
public class QuestDialogueData
{
    public List<QuestTrigger> triggersToActiveToAdd = new List<QuestTrigger>();
    public List<QuestTrigger> triggersToDesactiveToAdd = new List<QuestTrigger>();
    public int line;

    public QuestDialogueData(QuestTrigger triggerA, QuestTrigger triggerD, int line)
    {
        if(triggerA != null)
            triggersToActiveToAdd.Add(triggerA);
        if(triggerD != null)
            triggersToDesactiveToAdd.Add(triggerD);
        this.line = line;
    }
}

[Serializable]
public class Dialogue
{
    public List<Sentence> sentences = new List<Sentence>();
}

[Serializable]
public class Sentence
{
    [TextArea]
    public string text;
    public List<Response> responses = new List<Response>();
    public int line;

    public Sentence(string text = "", int line = 0)
    {
        this.text = text;
        this.line = line;
    }
}

[Serializable]
public class Response
{
    public string text;
    public int line;

    public Response(string text, int line)
    {
        this.text = text;
        this.line = line;
    }
}