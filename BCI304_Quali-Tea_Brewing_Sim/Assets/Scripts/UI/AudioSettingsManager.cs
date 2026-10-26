using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        LoadVolumeData();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolumeData()
    {
        if(PlayerPrefs.HasKey("MasterVolume") == true) SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        else SetMasterVolume(1f);
        
        if(PlayerPrefs.HasKey("MusicVolume") == true) SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        else SetMusicVolume(0.5f);

        if(PlayerPrefs.HasKey("SFXVolume") == true) SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        else SetSFXVolume(1f);
    }
}
