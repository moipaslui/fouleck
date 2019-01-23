using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;

	void Start()
    {
        source = GetComponent<AudioSource>();
	}

    public void AugmenterVolume()
    {
        source.volume += 0.02f;
    }

    public void BaisserVolume()
    {
        source.volume -= 0.02f;
    }
}
