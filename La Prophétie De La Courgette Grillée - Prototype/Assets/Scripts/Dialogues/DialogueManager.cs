using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;
    public GameObject response1Button;
    public TextMeshProUGUI response1;
    public GameObject response2Button;
    public TextMeshProUGUI response2;

    private Dialogue currentDialogue;
    private QuestTrigger_Dialogue currentTrigger;
    private int count;
    private int currentLine;

    [HideInInspector]
    public bool isDialoguing;

    public int ShowDialogue(Dialogue dialogue, string name, QuestTrigger_Dialogue trigger = null)
    {
        currentTrigger = trigger;
        if (!isDialoguing)
        {
            // On change la variable de dialogue
            currentDialogue = dialogue;
            count = 0;

            // On affiche les textes
            nameText.text = name;
            DisplaySentence();

            // On affiche le panel
            dialoguePanel.SetActive(true);

            // Cette variable montre qu'un dialogue est activé
            isDialoguing = true;

            return 0;
        }
        else
        {
            // On affiche la prochaine phrase
            return DisplayNextSentence(response: 0);
        }
    }

    private int DisplayNextSentence(int response = 0)
    {
        if (response != 0 || currentDialogue.sentences[count].responses.Count < 2)
        {
            if (response != 0)
            {
                currentLine = currentDialogue.sentences[count].responses[response-1].line;
            }

            int oldCount = count;
            
            for (int i = count+1; i < currentDialogue.sentences.Count; i++)
            {
                if(currentDialogue.sentences[i].line == currentLine || currentDialogue.sentences[i].line == 0)
                {
                    count = i;
                    break;
                }
            }

            if (oldCount == count)
            {
                EndDialogue();
                return -1;
            }
            else
                DisplaySentence();
        }
        return response;
    }

    private void DisplaySentence()
    {
        dialogueText.text = currentDialogue.sentences[count].text;

        response1Button.SetActive(false);
        response2Button.SetActive(false);

        if (currentDialogue.sentences[count].responses.Count > 0)
        {
            response1Button.SetActive(true);
            response1.text = currentDialogue.sentences[count].responses[0].text;
            if (currentDialogue.sentences[count].responses.Count > 1)
            {
                response2Button.SetActive(true);
                response2.text = currentDialogue.sentences[count].responses[1].text;
            }
        }
    }

    public void Response1()
    {
        DisplayNextSentence(response: 1);
    }

    public void Response2()
    {
        DisplayNextSentence(response: 2);
    }

    public void EndDialogue()
    {
        // On affiche plus le panel
        dialoguePanel.SetActive(false);

        // On est plus entrain de dialoguer
        isDialoguing = false;

        if(currentTrigger != null)
        {
            GetComponent<QuestManager>().FindQuest(currentTrigger).line = currentLine;
            currentTrigger = null;
        }
    }
}
