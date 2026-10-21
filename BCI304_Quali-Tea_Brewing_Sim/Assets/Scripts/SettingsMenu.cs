using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject teaPage;
    [SerializeField] private GameObject teaButton;
    [SerializeField] private GameObject infoPage;
    [SerializeField] private GameObject infoButton;

    private void OnEnable()
    {
        Debug.Log("Game paused");
        Time.timeScale = 0f;
    }

    public void OpenSettings()
    {
        settingsPage.SetActive(true);
        settingsButton.SetActive(false);
        teaPage.SetActive(false);
        teaButton.SetActive(true);
        infoPage.SetActive(false);
        infoButton.SetActive(true);
    }

    public void OpenTea()
    {
        teaPage.SetActive(true);
        teaButton.SetActive(false);
        infoPage.SetActive(false);
        infoButton.SetActive(true);
        settingsPage.SetActive(false);
        settingsButton.SetActive(true);
    }

    public void OpenInfo()
    {
        infoPage.SetActive(true);
        infoButton.SetActive(false);
        settingsPage.SetActive(false);
        settingsButton.SetActive(true);
        teaPage.SetActive(false);
        teaButton.SetActive(true);
    }

    public void QuitToTitle()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("exiting game");
        Application.Quit(); 
    }

    public void CloseSettings()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
