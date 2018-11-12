using UnityEngine;

//[ExecuteInEditMode]
public class ItemOnObject : MonoBehaviour
{
    public Item item;

    private SpriteRenderer sp;
    private Interactable inter;
    private IsometricObject iso;

    /* --- EXECUTED IN EDITOR --- */

    private void Update()
    {
        if (item != null && !Application.isPlaying)
        {
            Start();
            Refresh();
        }
    }

    /* -------------------------- */

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        inter = GetComponent<Interactable>();
        iso = GetComponent<IsometricObject>();
    }

    private void Refresh()
    {
        sp.sprite = item.icon;
        if(inter != null)
            inter.offsetIcon = item.offsetIcon;
        iso.RefreshSprite();
        iso.floorHeight = item.floorHeight;
        iso.PlaceInZ();
    }

    public void ChangeItem(Item item)
    {
        Start();
        this.item = item;
        Refresh();
    }
}
