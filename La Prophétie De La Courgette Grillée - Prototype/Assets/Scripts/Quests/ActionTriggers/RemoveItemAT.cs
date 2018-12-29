public class RemoveItemAT : ActionTrigger
{
    public Item item;

    public override void Trigger()
    {
        GameManager.inventory.Remove(item, instanciate:false, isQuestItem:true);
    }
}
