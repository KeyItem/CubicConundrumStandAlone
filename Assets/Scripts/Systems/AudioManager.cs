using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public static bool audioIsPaused;

    public static AudioSource bgmPlayer;

    public static List<AudioClip> bgmList;

	void Start ()
    {
        InstanceManagement();
    }
	
	void Update ()
    {
	
	}

    void InstanceManagement()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            audioManager = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void PlayBGM(int bgmIndex)
    {
        bgmPlayer.clip = bgmList[bgmIndex];
        bgmPlayer.Play();
    }

    public static void PauseBGM()
    {

    }

    public static void StopBGM()
    {
        bgmPlayer.Stop();
    }
}
