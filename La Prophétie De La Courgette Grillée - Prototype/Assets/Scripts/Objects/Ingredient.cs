using UnityEngine;

public class Ingredient : MonoBehaviour
{
	public new string name;
	[TextArea] public string description;

	public int quality;
	public int cost;

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "player")
		{
			// Add ingredient to inventory
			Destroy(gameObject);
		}
	}
}
