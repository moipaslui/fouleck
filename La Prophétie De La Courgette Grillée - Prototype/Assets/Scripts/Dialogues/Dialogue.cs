using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public List<string> sentences = new List<string>();
    public string speaker;
}
