using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    public Vector2 offsetIcon;

    private void Awake()
    {
        // Set to layer mask "Interactable"
        gameObject.layer = 11;
    }

    public virtual void Interact()
    {
        // This method is meant to be overwritten
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)offsetIcon, 0.03f);
    }
}
