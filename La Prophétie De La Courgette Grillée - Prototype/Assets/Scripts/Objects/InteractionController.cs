using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactibleRadius = 3f;
    public LayerMask interactibleLayerMask;
    public GameObject ButtonIconInteractable;
    public GameObject NPCIconInteractable;

    [HideInInspector]
    public GameObject focus;

    private DialogueManager DM;

    private void Start()
    {
        DM = FindObjectOfType<DialogueManager>();
    }

    void Update ()
    {
        // Set focus
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, interactibleRadius, interactibleLayerMask);
        if (targets.Length > 0)
        {
            float minDistance = Vector2.Distance(transform.position, targets[0].transform.position);
            focus = targets[0].gameObject;
            foreach (Collider2D target in targets)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
                if (distanceToTarget < minDistance)
                {
                    minDistance = distanceToTarget;
                    focus = target.gameObject;
                }
            }
            ButtonIconInteractable.transform.position = new Vector3
                                                        ( focus.transform.position.x + focus.GetComponent<Interactable>().offsetIcon.x,
                                                          focus.transform.position.y + focus.GetComponent<Interactable>().offsetIcon.y,
                                                          focus.transform.position.z - 0.002f
                                                        );
            ButtonIconInteractable.SetActive(true);
            if(focus.tag == "NPC")
            {
                NPCIconInteractable.transform.position = new Vector3
                                                         ( ButtonIconInteractable.transform.position.x,
                                                           ButtonIconInteractable.transform.position.y,
                                                           ButtonIconInteractable.transform.position.z + 0.001f
                                                          );
                NPCIconInteractable.SetActive(true);
            }
            else
            {
                NPCIconInteractable.SetActive(false);
                if(DM.isDialoguing)
                    DM.EndDialogue();
            }
        }
        else
        {
            ButtonIconInteractable.SetActive(false);
            NPCIconInteractable.SetActive(false);
            if (DM.isDialoguing)
                DM.EndDialogue();
            focus = null;
        }


        // Interact
        if (Input.GetButtonDown("Interact") && focus != null)
        {
            focus.GetComponent<Interactable>().Interact();
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
