using UnityEngine;

[System.Serializable]
public class DialogueSentence
{
    [TextArea(3, 10)]
    public string text;
    public GameObject speaker;
}