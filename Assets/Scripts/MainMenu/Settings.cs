using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider brightnessSlider;
    public Slider musicSlider;
    public Slider soundFXSlider;
    public SoundManager soundManager;
    private float originalBrightness;
    private float originalMusicVolume;
    private float originalSoundFXVolume;

    public void AdjustBrightness(float brightnessValue)
    {
        // Clamps the brightness value between 0 and 1
        brightnessValue = Mathf.Clamp01(brightnessValue);

        // Updates the brightness of the scene
        RenderSettings.ambientLight = new Color(brightnessValue, brightnessValue, brightnessValue);
    }

    public void AdjustMusicVolume(float volume)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            // Adjusts the music volume based on the slider value
            audioManager.MusicVolume(volume);
        }
        else
        {
            Debug.LogWarning("AudioManager not found.");
        }
    }

    public void AdjustSoundFXVolume(float volume)
    {
        if (soundManager != null)
        {
            // Adjusts the sound effects volume based on the slider value
            soundManager.SoundFXVolume(volume);
        }
        else
        {
            Debug.LogWarning("SoundManager reference is missing!");
        }
    }

    public void BackToMainMenu()
    {
        // Loads the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void CancelChanges()
    {
        // Resets the values of the sliders to their original values
        brightnessSlider.value = originalBrightness;
        musicSlider.value = originalMusicVolume;
        soundFXSlider.value = originalSoundFXVolume;
    }

    public void SaveChanges()
    {
        // Saves the current values of the sliders
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SoundFXVolume", soundFXSlider.value);
        PlayerPrefs.Save();
    }
}