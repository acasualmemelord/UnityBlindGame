﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public GameObject player;
    public Slider brightnessSlider;
    public Slider musicSlider;
    public Slider soundFXSlider;
    public Button cancelButton;
    public Button saveButton;
    public Button backButton;

    public SoundManager soundManager;

    private float originalBrightness;
    private static float originalMusicVolume;
    private static float originalSoundFXVolume;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        // Loads saved settings or set default values
        originalBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        originalMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        originalSoundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume", 0.5f);

        // Sets initial slider values
        brightnessSlider.value = originalBrightness;
        musicSlider.value = originalMusicVolume;
        soundFXSlider.value = originalSoundFXVolume;

        ApplySettings();

        // Adds event listeners
        cancelButton.onClick.AddListener(CancelChanges);
        saveButton.onClick.AddListener(SaveChanges);
        brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
        musicSlider.onValueChanged.AddListener(AdjustMusicVolume);
        soundFXSlider.onValueChanged.AddListener(AdjustSoundFXVolume);

        if (soundManager == null) {
            soundManager = FindObjectOfType<SoundManager>();
        }
    }

    public void ApplySettings() {
        AdjustBrightness(brightnessSlider.value);
        AdjustMusicVolume(musicSlider.value);
        AdjustSoundFXVolume(soundFXSlider.value);
    }

    public void AdjustBrightness(float brightnessValue) {
        // Clamps the brightness value between 0 and 1
        brightnessValue = Mathf.Clamp01(brightnessValue);

        // Updates the brightness of the scene
        RenderSettings.ambientLight = new Color(brightnessValue, brightnessValue, brightnessValue);
    }

    public void AdjustMusicVolume(float volume) {
        SoundManager.Instance.MusicVolume(volume);
        PlayerPrefs.GetFloat("MusicVolume", volume);
    }

    public void AdjustSoundFXVolume(float volume) {
        SoundManager.Instance.SoundFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void CloseSettings() {
        // Load the main menu scene
        SceneManager.UnloadSceneAsync("SettingsScene");
    }

    public void CancelChanges() {
        // Reset slider values to the previously saved settings
        brightnessSlider.value = originalBrightness;
        musicSlider.value = originalMusicVolume;
        soundFXSlider.value = originalSoundFXVolume;

        // Apply settings after canceling changes
        ApplySettings();
    }

    public void SaveChanges() {
        // Save the current slider values
        originalBrightness = brightnessSlider.value;
        originalMusicVolume = musicSlider.value;
        originalSoundFXVolume = soundFXSlider.value;

        // Store the values using PlayerPrefs
        PlayerPrefs.SetFloat("Brightness", originalBrightness);
        PlayerPrefs.SetFloat("MusicVolume", originalMusicVolume);
        PlayerPrefs.SetFloat("SoundFXVolume", originalSoundFXVolume);
        PlayerPrefs.Save();
    }
}