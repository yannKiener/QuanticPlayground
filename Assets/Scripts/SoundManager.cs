using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public AudioClip gameOverSoundEffect;
    public AudioClip gameWonSoundEffect;
    public AudioClip bounceSoundEffect;
    public AudioClip breakSoundEffect;

    private static GameObject thisGameObject;
    private static SoundManager instance;
    private static float volume = 1;

    public void Start()
    {
        instance = this;
        thisGameObject = gameObject;
    }
    
    public static void PlaySound(AudioClip sound)
    {
        if(sound != null)
        {
            AudioSource audioSource = thisGameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;
            audioSource.PlayOneShot(sound, 1);
            audioSource.clip = sound;
            GameObject.Destroy(audioSource, sound.length);
        }
    }

    public static void PlaySoundsWithRandomChance(List<AudioClip> sounds, float percent)
    {
        if (sounds != null && Random.Range(0,101) <= percent)
        {
            PlaySound(GetRandomClip(sounds));
        }
    }

    public static void PlaySound(List<AudioClip> sounds)
    {
        if (sounds != null && sounds.Count > 0)
        {
            PlaySound(GetRandomClip(sounds));
        }
    }

    public static void SetVolume(float vol)
    {
        volume = vol;
    }

    public static float GetVolume()
    {
        return volume;
    }

    public static void StopAll()
    {
        foreach (AudioSource audio in thisGameObject.GetComponents<AudioSource>())
        {
            GameObject.Destroy(audio);
        }
    }

    public static void StopSound(AudioClip sound)
    {
        if(sound != null)
        {
            List<AudioSource> audioSources = thisGameObject.GetComponents<AudioSource>().ToList<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.clip != null && audioSource.clip == sound)
                {
                    audioSource.Stop();
                    GameObject.Destroy(audioSource);
                }
            }
        }
    }

    public static void PlayGameOverSound()
    {
        StopAll();
        PlaySound(instance.gameOverSoundEffect);
    }

    public static void PlayGameWonSound()
    {
        StopAll();
        PlaySound(instance.gameWonSoundEffect);
    }

    public static void PlayBreakSound()
    {
        PlaySound(instance.breakSoundEffect);
    }

    public static void PlayBounceSound()
    {
        PlaySound(instance.bounceSoundEffect);
    }

    private static AudioClip GetRandomClip(List<AudioClip> audioClips)
    {
        if(audioClips != null && audioClips.Count > 0)
        {
            return audioClips[Random.Range(0, audioClips.Count)];
        } else
        {
            return null;
        }
    }

}
