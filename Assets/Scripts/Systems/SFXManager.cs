using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour
{
    public static SFXManager sfxManager;

    public AudioSource sfxPlayer;

    public AudioClip[] sfxList;

	void Start ()
    {
        InstanceManagement();

        sfxPlayer = GetComponent<AudioSource>();
    }
	
    void InstanceManagement()
    {
        if (sfxManager != null && sfxManager != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            sfxManager = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(int sfxIndex)
    {
        sfxPlayer.clip = sfxList[sfxIndex];
        sfxPlayer.Play();
    }

    public void StopSFX()
    {
        sfxPlayer.Stop();
    }
}
