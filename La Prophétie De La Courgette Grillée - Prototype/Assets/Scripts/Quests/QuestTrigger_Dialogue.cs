public class QuestTrigger_Dialogue : QuestTrigger
{
    public QuestDialogue dialogue = new QuestDialogue();

    override public void Trigger()
    {
        if (GameManager.dialogueManager.ShowDialogue(dialogue, gameObject.name, trigger: this) == -1)
        {
            int index = -1;
            for (int i = 0; i < dialogue.triggers.Count; i++)
            {
                if (dialogue.triggers[i].line == GameManager.questManager.FindQuest(this).line)
                    index = i;
            }

            if (index != -1)
            {
                foreach (QuestTrigger trigger in dialogue.triggers[index].triggersToActiveToAdd)
                {
                    triggersToActive.Add(trigger);
                }

                foreach (QuestTrigger trigger in dialogue.triggers[index].triggersToDesactiveToAdd)
                {
                    triggersToDesactive.Add(trigger);
                }
            }

            base.Trigger();
        }
    }

    public override void EndOfInteraction()
    {
        base.EndOfInteraction();

        GameManager.dialogueManager.EndDialogue();
    }
}
