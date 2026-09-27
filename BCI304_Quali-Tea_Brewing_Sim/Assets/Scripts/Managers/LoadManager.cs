using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private float sceneFadeDuration;
    private SceneFade sceneFade;
    [SerializeField] private GameObject mainCamera;
    private Vector3 frontCamPos = new Vector3(0, 0, -10);
    private Vector3 gardenCamPos = new Vector3 (-32, 0, -10);
    private Vector3 teaBrewCamPos = new Vector3(32, 0, -10);
    private Vector3 teaAddCamPos = new Vector3(50, 0, -10);

    [SerializeField] private GameObject toTeaAddButton;
    [SerializeField] private GameObject toTeaBrewButton;
    [SerializeField] private GameObject ticketButton;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private Tree tree;

    [SerializeField] GameObject teacup;  
    private Teacup teacupScript;
      
    private Tutorial tutorial;
    
    private static LoadManager uniqueInstance;
    private void Awake()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");

        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sceneFade = GetComponentInChildren<SceneFade>();
        sceneFade.gameObject.SetActive(false);
        teacupScript = FindAnyObjectByType(typeof(Teacup)) as Teacup;
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
    }

    private IEnumerator MoveCameraCoroutine(string screenName) //fade in and out when loading a scene
    {
        sceneFade.gameObject.SetActive(true);
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        mainCamera = GameObject.FindWithTag("MainCamera");
        
        //show the customer if entering the front counter
        if (screenName == "FrontCounter")
        {
            mainCamera.transform.position = frontCamPos;
            uiManager.GardenUI(false);
            ticketButton.SetActive(false);
            toTeaBrewButton.SetActive(false);
            dialogue.SetActive(true);
            uiManager.CloseDayOverscreen();  
        }

        if (screenName == "Garden")
        {
            mainCamera.transform.position = gardenCamPos;
            uiManager.GardenUI(true);
            uiManager.CloseDayOverscreen();
            tree.SpawnLemons();
        }
        
        if (screenName == "TeaBrew")
        {
            if (tutorial.tutorialActive) tutorial.ShowTutorialStep();

            mainCamera.transform.position = teaBrewCamPos;
            toTeaAddButton.SetActive(true);
            toTeaBrewButton.SetActive(false);
            ticketButton.SetActive(true);
            dialogue.SetActive(false);
            if (teacup == null) { teacup = GameObject.Find("Teacup"); }
            teacup.transform.position = new Vector3(38f, -2f, 0);
            teacupScript.NewStartPosition();
        }
        
        if (screenName == "TeaAdd")
        {
            mainCamera.transform.position = teaAddCamPos;
            toTeaAddButton.SetActive(false);
            toTeaBrewButton.SetActive(true);
            if (teacup == null) { teacup = GameObject.Find("Teacup"); }
            teacup.transform.position = new Vector3(49.85f, -2f, 0);
            teacupScript.NewStartPosition();
        }

        if (screenName == "DayEnd")
        {
            uiManager.DayOverScreen();
        }

        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);        
    }
    
    public void Load(string screenName)
    {
        StartCoroutine(MoveCameraCoroutine(screenName));
    }
}
