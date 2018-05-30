using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    public float timeBeforeDestroy;

    private float counter;

	void Update ()
    {
        counter += Time.deltaTime;
        if(counter >= timeBeforeDestroy)
        {
            Destroy(gameObject);
        }
	}
}
