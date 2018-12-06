using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;

	void Start()
    {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(source);
	}

    public void AugmenterVolume()
    {
        source.volume = source.volume + 0.02f;
    }

    public void BaisserVolume()
    {
        source.volume = source.volume - 0.02f;
    }
}
