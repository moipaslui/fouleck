using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    
    private GameObject focus;

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
    }

    public void RefreshFocus(GameObject foc)
    {
        focus = foc;
        if (focus == null)
        {
            // Le hint n'est pas visible si rien n'est focus
            spriteRenderer.enabled = false;
        }
        else
        {
            // On affiche le bon sprite
            if(focus.tag == "NPC")
            {
                anim.SetBool("NPC", true);
            }
            else
            {
                anim.SetBool("NPC", false);
            }
            
            // On actualise sa position une première fois pour éviter le "clignotement" (bug)
            transform.position = new Vector3(focus.transform.position.x + focus.GetComponent<Interactable>().offsetIcon.x,
                                               focus.transform.position.y + focus.GetComponent<Interactable>().offsetIcon.y,
                                               focus.transform.position.z - 0.002f);

            // On le rend visible
            spriteRenderer.enabled = true;
        }
    }

    private void Update()
    {
        if(spriteRenderer.enabled)
        {
            if (focus == null)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                // On actualise sa position
                transform.position = new Vector3(focus.transform.position.x + focus.GetComponent<Interactable>().offsetIcon.x,
                                                   focus.transform.position.y + focus.GetComponent<Interactable>().offsetIcon.y,
                                                   focus.transform.position.z - 0.002f);
            }
        }
    }
}
