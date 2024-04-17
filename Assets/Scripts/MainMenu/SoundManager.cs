using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Fields
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    private static SoundManager _instance;
    private AudioSource musicAudioSource;

    private void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    public void SoundFXVolume(float volume)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("AudioManager AudioSource component is missing!");
        }
    }

    public static SoundManager Instance
    {
        // Accessess instance object
        get { return _instance; }
    }

    private void Start()
    {
        // Retrieves audio and calls the method to play it
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            // Plays background music continuously
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip is not assigned.");
        }
    }
}