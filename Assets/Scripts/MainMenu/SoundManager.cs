using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip backgroundMusic;
    private AudioSource musicAudioSource;
    private AudioSource soundFXAudioSource;

    private static SoundManager _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }

        // Find the AudioSource components for music and sound effects
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2) {
            musicAudioSource = audioSources[0];
            soundFXAudioSource = audioSources[1];
        } else {
            // Create the necessary AudioSource components dynamically
            musicAudioSource = gameObject.AddComponent<AudioSource>();
            soundFXAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void SoundFXVolume(float volume) {
        if (soundFXAudioSource != null) {
            soundFXAudioSource.volume = volume;
        } else {
            Debug.LogWarning("SoundManager sound effects AudioSource component is missing!");
        }
    }

    public void MusicVolume(float volume) {
        if (musicAudioSource != null) {
            musicAudioSource.volume = volume;
        } else {
            Debug.LogWarning("SoundManager music AudioSource component is missing!");
        }
    }

    public static SoundManager Instance {
        get { return _instance; }
    }

    private void Start() {
        AudioListener.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic() {
        if (backgroundMusic != null && musicAudioSource != null) {
            // Plays background music continuously
            musicAudioSource.clip = backgroundMusic;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
        else {
            Debug.LogWarning("Background music clip or AudioSource is not assigned.");
        }
    }
}