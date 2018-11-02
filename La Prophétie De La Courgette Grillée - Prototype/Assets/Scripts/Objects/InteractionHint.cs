using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private bool isActive;
    private GameObject focus;

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
        isActive = false;
    }

    public void RefreshFocus(GameObject foc)
    {
        focus = foc;
        if (focus == null)
        {
            // Le hint n'est pas visible si rien n'est focus
            spriteRenderer.enabled = false;
            isActive = false;
        }
        else
        {
            // On refresh en boucle la position du hint
            isActive = true;

            // On affiche le bon sprite
            if(focus.tag == "NPC")
            {
                anim.SetBool("NPC", true);
            }
            else
            {
                anim.SetBool("NPC", false);
            }

            // On le rend visible
            spriteRenderer.enabled = true;
        }
    }

    private void Update()
    {
        if(isActive)
        {
            // On actualise sa position
            transform.position = new Vector3(focus.transform.position.x + focus.GetComponent<Interactable>().offsetIcon.x,
                                               focus.transform.position.y + focus.GetComponent<Interactable>().offsetIcon.y,
                                               focus.transform.position.z - 0.002f);
        }
    }
}
