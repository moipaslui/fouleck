using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;

    private Queue<DialogueSentence> sentences;

    [HideInInspector]
    public bool isDialoguing;

    void Start()
    {
        sentences = new Queue<DialogueSentence>();
    }

    public bool StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        isDialoguing = true;

        sentences.Clear();
        foreach(DialogueSentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        return DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }

        DialogueSentence sentence = sentences.Dequeue();
        nameText.text = sentence.speaker.name;
        dialogueText.text = sentence.text;
        return false;
    }

    public void EndDialogue()
    {
        sentences.Clear();
        dialoguePanel.SetActive(false);
        isDialoguing = false;
        Debug.Log("End of conversation");
    }
}
