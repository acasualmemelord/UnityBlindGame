using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource musicAudioSource;
    private AudioSource soundFXAudioSource;

    private static SoundManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Find the AudioSource components for music and sound effects
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            musicAudioSource = audioSources[0];
            soundFXAudioSource = audioSources[1];
        }
        else
        {
            Debug.LogWarning("SoundManager requires at least two AudioSource components!");
        }
    }

    public void SoundFXVolume(float volume)
    {
        if (soundFXAudioSource != null)
        {
            soundFXAudioSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("SoundManager sound effects AudioSource component is missing!");
        }
    }

    public void MusicVolume(float volume)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("SoundManager music AudioSource component is missing!");
        }
    }

    public static SoundManager Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicAudioSource != null)
        {
            // Plays background music continuously
            musicAudioSource.clip = backgroundMusic;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip or AudioSource is not assigned.");
        }
    }
}