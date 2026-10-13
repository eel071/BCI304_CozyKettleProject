using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField shopNameInputField;
    [SerializeField] private Button startGameButton;
    private string playerInput;

    public void LogPlayerInput()
    {
        playerInput = shopNameInputField.text;
        startGameButton.interactable = true;
    }

    public void StartGame()
    {
        //create a new game which will initalize our data to a clean slate
        DataPersistenceManager.instance.NewGame(playerInput);

        SceneManager.LoadSceneAsync("Game");
    }
}
