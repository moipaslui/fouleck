using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    public bool isActive = true;
    public Vector2 offsetIcon;

    private NPC npc;

    private void Awake()
    {
        if (isActive)
            ChangeInteractable(true);

        npc = GetComponent<NPC>();
    }

    public virtual bool Interact()
    {
        if (!isActive)
            return false;

        if (npc != null)
        {
            npc.canMove = false;
        }

        return true;
    }

    public virtual void EndOfInteraction()
    {
        if (npc != null)
        {
            npc.canMove = true;
        }
    }

    protected void ChangeInteractable(bool isInteractable)
    {
        if(isInteractable)
        {
            // Set to layer mask "Interactable"
            gameObject.layer = 11;
        }
        else
        {
            gameObject.layer = 0;
        }
    }

    public bool IsInteractableLayer()
    {
        return gameObject.layer == 11;
    }

    public virtual void ChangeActivation(bool newIsActive)
    {
        ChangeInteractable(newIsActive);
        isActive = newIsActive;
        this.enabled = newIsActive;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)offsetIcon, 0.03f);
    }
}
