using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;

    private int count;

    private Dialogue currentDialogue;

    [HideInInspector]
    public bool isDialoguing;

    public bool ShowDialogue(Dialogue dialogue)
    {
        if (!isDialoguing || currentDialogue != dialogue)
        {
            // On change la variable de dialogue
            currentDialogue = dialogue;

            // On affiche le nom de la personne qui parle
            nameText.text = dialogue.speaker;

            // On remet le compteur de phrase à 0
            count = 0;

            // On affiche le panel
            dialoguePanel.SetActive(true);

            // Cette variable montre qu'un dialogue est activé
            isDialoguing = true;
        }
        
        // On affiche la prochaine phrase
        return DisplayNextSentence();
    }

    private bool DisplayNextSentence()
    {
        // Si c'était la dernière phrase, on arrête tout
        if(count >= currentDialogue.sentences.Length)
        {
            EndDialogue();
            return true;
        }
        
        // Sinon on affiche la prochaine
        dialogueText.text = currentDialogue.sentences[count++];
        return false;
    }

    public void EndDialogue()
    {
        // On affiche plus le panel
        dialoguePanel.SetActive(false);

        // On est plus entrain de dialoguer
        isDialoguing = false;
    }
}
