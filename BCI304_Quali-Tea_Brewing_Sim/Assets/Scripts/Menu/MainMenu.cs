using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject saveSlotsMenu;
    
    public void OpenSaveSlots()
    {
        saveSlotsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("exiting game");
        Application.Quit(); 
    }


}
