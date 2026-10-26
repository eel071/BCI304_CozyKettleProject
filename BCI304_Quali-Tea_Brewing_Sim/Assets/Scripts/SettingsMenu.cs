using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject teaPage;
    [SerializeField] private GameObject teaButton;
    [SerializeField] private GameObject infoPage;
    [SerializeField] private GameObject infoButton;


    [Header("Audio")]
    [SerializeField] private AudioClip[] pageTurnsSFX;
    [SerializeField] private AudioClip[] clickSFX;

    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;


    private void OnEnable()
    {
        Debug.Log("Game paused");
        Time.timeScale = 0f;
        OpenSettings();
    }

    public void OpenSettings()
    {
        settingsPage.SetActive(true);
        settingsButton.SetActive(false);
        teaPage.SetActive(false);
        teaButton.SetActive(true);
        infoPage.SetActive(false);
        infoButton.SetActive(true);
        playRandomSFX(pageTurnsSFX);
        UpdateAudioSliders();
    }

    public void OpenTea()
    {
        teaPage.SetActive(true);
        teaButton.SetActive(false);
        infoPage.SetActive(false);
        infoButton.SetActive(true);
        settingsPage.SetActive(false);
        settingsButton.SetActive(true);
        playRandomSFX(pageTurnsSFX);
    }

    public void OpenInfo()
    {
        infoPage.SetActive(true);
        infoButton.SetActive(false);
        settingsPage.SetActive(false);
        settingsButton.SetActive(true);
        teaPage.SetActive(false);
        teaButton.SetActive(true);
        playRandomSFX(pageTurnsSFX);
    }

    public void QuitToTitle()
    {
        playRandomSFX(clickSFX);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        playRandomSFX(clickSFX);
        Time.timeScale = 1f;
        Debug.Log("exiting game");
        Application.Quit(); 
    }

    public void CloseSettings()
    {
        playRandomSFX(clickSFX);
        Time.timeScale = 1f;

        gameObject.SetActive(false);        
    }

    public void randomClickSFX()
    {
        playRandomSFX(clickSFX);
    }

    private void playRandomSFX(AudioClip[] randomSFX)
    {
        int r = Random.Range(0, randomSFX.Length -1); //choose a random sfx

        AudioManager.instance.PlaySFXClip(randomSFX[r], transform, 1f, 0f);
    }

    private void UpdateAudioSliders()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}
