public class RemoveItemAT : ActionTrigger
{
    public Item item;

    int i = 0;

    public override void Trigger()
    {
        GameManager.inventory.Remove(item, instanciate:false, isQuestItem:true);
    }
}
