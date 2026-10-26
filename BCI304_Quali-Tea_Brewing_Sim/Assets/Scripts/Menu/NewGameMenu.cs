using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField shopNameInputField;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Toggle tutorialToggle;
    private string playerInput;
    private bool tutorial;

    [SerializeField] private AudioClip clickSfx;

    public void LogPlayerInput()
    {
        playerInput = shopNameInputField.text;
        tutorial = tutorialToggle.isOn;
        startGameButton.interactable = true;
    }

    public void StartGame()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        //create a new game which will initalize our data to a clean slate
        DataPersistenceManager.instance.NewGame(playerInput, tutorial);
        SceneManager.LoadSceneAsync("Game");
    }
}
