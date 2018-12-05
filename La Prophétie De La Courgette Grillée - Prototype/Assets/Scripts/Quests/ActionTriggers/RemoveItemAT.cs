public class RemoveItemAT : ActionTrigger
{
    public Item item;

    public override void Trigger()
    {
        /// A adapter avec les objets de quêtes
        GameManager.inventory.Remove(item, instanciate:false, isQuestItem:true);
    }
}
