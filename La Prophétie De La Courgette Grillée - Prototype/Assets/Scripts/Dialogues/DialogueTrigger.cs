using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    public void TriggerDialogue()
    {
        DialogueManager DM = FindObjectOfType<DialogueManager>();

        if(!DM.isDialoguing)
        {
            if(DM.StartDialogue(dialogue))
            {
                DM.isDialoguing = false;
            }
            else
            {
                DM.isDialoguing = true;
            }
        }
        else
        {
            if(DM.DisplayNextSentence())
            {
                DM.isDialoguing = false;
            }
        }
    }
}
