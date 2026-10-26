using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject saveSlotsMenu;
    [SerializeField] private AudioClip clickSfx;

    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    
    public void OpenSaveSlots()
    {
        saveSlotsMenu.SetActive(true);
        gameObject.SetActive(false);
        
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
    }

    public void QuitGame()
    {
        Debug.Log("exiting game");
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        Application.Quit(); 
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        UpdateAudioSliders();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
    }

    private void UpdateAudioSliders()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }


}
