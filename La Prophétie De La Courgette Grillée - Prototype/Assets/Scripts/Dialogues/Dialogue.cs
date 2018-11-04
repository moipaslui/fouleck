using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
    public string speaker;
}
