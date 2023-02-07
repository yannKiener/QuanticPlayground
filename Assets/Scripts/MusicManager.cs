using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip basicMusic;
    public AudioClip quantumMusic;
    public AudioClip endMusic;

    private static MusicManager instance;
	private AudioSource audioSource;
    private float musicTimer = 0;

    public void Start()
    {
        musicTimer = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        instance = this;
        PlayBasicMusic();
    }

    private void Update()
    {
        musicTimer += Time.unscaledDeltaTime;
        if (musicTimer > basicMusic.length || musicTimer > quantumMusic.length)
        {
            musicTimer = 0;
        }
    }

    public static void PlayBasicMusic()
    {
        if (instance.basicMusic != instance.audioSource.clip)
        {
            instance.audioSource.clip = instance.basicMusic;
            instance.audioSource.time = instance.musicTimer;
            instance.audioSource.Play();
        }
    }

    public static void PlayQuantumMusic()
    {
        if (instance.quantumMusic != instance.audioSource.clip)
        {
            instance.audioSource.clip = instance.quantumMusic;
            instance.audioSource.time = instance.musicTimer;
            instance.audioSource.Play();
        }
    }

    public static void PlayEndMusic()
    {
        if (instance.endMusic != instance.audioSource.clip)
        {
            instance.audioSource.clip = instance.endMusic;
            instance.audioSource.time = 0f;
            instance.audioSource.Play();
        }
    }

    public static void SwitchMusic(bool isOn)
    {
        if (isOn)
        {
            instance.audioSource.volume = 1;
        } else
        {
            instance.audioSource.volume = 0;
        }
    }
}
