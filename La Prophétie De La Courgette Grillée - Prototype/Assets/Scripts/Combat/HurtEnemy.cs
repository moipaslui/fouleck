using UnityEngine;

public class HurtEnemy : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ennemi")
        {
            Debug.Log("Ennemi touché.");
        }
    }
}
