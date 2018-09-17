using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public float YOffset;

    void Update ()
	{
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + YOffset, player.transform.position.z - 10f);
	}
}
