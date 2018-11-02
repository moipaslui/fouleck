using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    public Vector2 offsetIcon;


    private NPC npc;

    private void Awake()
    {
        // Set to layer mask "Interactable"
        gameObject.layer = 11;

        npc = GetComponent<NPC>();
    }

    public virtual void Interact()
    {
        if(npc != null)
        {
            npc.canMove = false;
        }
    }

    public virtual void EndOfInteraction()
    {
        if (npc != null)
        {
            npc.canMove = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)offsetIcon, 0.03f);
    }
}
