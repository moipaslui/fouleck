using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public float YOffset;

    private PlayerHealthManager health;

    private void Start()
    {
        health = FindObjectOfType<PlayerHealthManager>();
    }

    void Update ()
	{
        if (health.playerAlive)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, player.transform.position.z - 10f);
        }
	}
}
