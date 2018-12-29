using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactibleRadius = 3f;
    public LayerMask interactibleLayerMask;
    public InteractionHint interactionHint;

    [HideInInspector]
    public GameObject focus;
    private GameObject oldFocus;
    private GameObject tempFocus;
    
    void Update()
    {
        // On regarde s'il y a des interactible autour du player
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, interactibleRadius, interactibleLayerMask);

        // On cherche le plus proche et on le focus
        tempFocus = null;
        float minDistance = interactibleRadius;
        foreach (Collider2D target in targets)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= minDistance)
            {
                minDistance = distanceToTarget;
                tempFocus = target.gameObject;
            }
        }

        // Si le focus a changé...
        if (tempFocus != focus)
        {
            // On met à jour les variables
            oldFocus = focus;
            focus = tempFocus;

            // On place le hint sur le nouveau focus
            interactionHint.RefreshFocus(focus);

            // On termine l'interaction de l'ancien focus
            if (oldFocus != null)
                oldFocus.GetComponent<Interactable>().EndOfInteraction();
        }

        // Interact
        if (Input.GetButtonDown("Interact") && focus != null)
        {
            Interactable[] interactables = focus.GetComponents<Interactable>();
            foreach(Interactable interactable in interactables)
            {
                if (interactable.isActive)
                {
                    interactable.Interact();
                    break;
                }
            }

            // Si jamais l'objet bouge est supprimé par exemple, on refresh le hint
            interactionHint.RefreshFocus(focus);
        }
    }


    // Interaction Sphere
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactibleRadius);

        Gizmos.color = Color.magenta;
    }
}
