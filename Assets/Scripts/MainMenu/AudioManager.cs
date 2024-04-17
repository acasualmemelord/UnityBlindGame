using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Fields
    private AudioSource soundFXAudioSource;

    private void Awake()
    {
        soundFXAudioSource = GetComponent<AudioSource>();
    }

    public void MusicVolume(float volume)
    {
        if (soundFXAudioSource != null)
        {
            soundFXAudioSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("SoundManager AudioSource component is missing!");
        }
    }
}