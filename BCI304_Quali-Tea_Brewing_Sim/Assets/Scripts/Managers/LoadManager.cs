using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{
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

    //GameObject teaManager;
    //GameObject customer;    
    [SerializeField] GameObject teacup;  
      
    
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
    }

    private IEnumerator LoadSceneCoroutine(string sceneName) //fade in and out when loading a scene
    {
        sceneFade.gameObject.SetActive(true);
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        mainCamera = GameObject.FindWithTag("MainCamera");
        
        //show the customer if entering the front counter
        if (sceneName == "FrontCounter")
        {
            //if (customer != null) customer.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }
        //hide the customer if entering the tea station
        else if (sceneName == "TeaStation")
        {
            //customer.SetActive(false);
            SceneManager.LoadScene(sceneName);
        }

        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);        
    }

    private IEnumerator MoveCameraCoroutine(string sceneName) //fade in and out when loading a scene
    {
        sceneFade.gameObject.SetActive(true);
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        mainCamera = GameObject.FindWithTag("MainCamera");
        
        //show the customer if entering the front counter
        if (sceneName == "FrontCounter")
        {
            mainCamera.transform.position = frontCamPos;
        }

        if (sceneName == "Garden")
        {
            mainCamera.transform.position = gardenCamPos;
        }
        
        if (sceneName == "TeaBrew")
        {
            mainCamera.transform.position = teaBrewCamPos;
        }
        
        if (sceneName == "TeaAdd")
        {
            mainCamera.transform.position = teaAddCamPos;
        }

        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);        
    }

    

    public void LoadTeaStation()
    {
        /*customer = GameObject.FindWithTag("Customer");       
        DontDestroyOnLoad(customer);   
        StartCoroutine(LoadSceneCoroutine("TeaStation")); */

        StartCoroutine(MoveCameraCoroutine("TeaBrew"));        
        toTeaAddButton.SetActive(true);
        toTeaBrewButton.SetActive(false);
        ticketButton.SetActive(true);
        dialogue.SetActive(false);
        if (teacup == null) { teacup = GameObject.Find("Teacup"); }
        teacup.transform.position = new Vector3(38f, -2f, 0);
    }
    public void LoadTeaAdditions()
    {
        StartCoroutine(MoveCameraCoroutine("TeaAdd"));
        toTeaAddButton.SetActive(false);
        toTeaBrewButton.SetActive(true);
        if (teacup == null) { teacup = GameObject.Find("Teacup"); }
        teacup.transform.position = new Vector3(49.85f, -2f, 0);
    }

    public void LoadFrontCounter()
    {
        /* (SceneManager.GetActiveScene().name == "TeaStation")
        {
            teacup = GameObject.Find("Teacup");
            DontDestroyOnLoad(teacup.transform.gameObject);
            StartCoroutine(LoadSceneCoroutine("FrontCounter"));
        }
        else
        {
            StartCoroutine(MoveCameraCoroutine("FrontCounter"));
        }*/

        StartCoroutine(MoveCameraCoroutine("FrontCounter"));
        ticketButton.SetActive(false);
        toTeaBrewButton.SetActive(false);
        dialogue.SetActive(true);                
    }
    
    public void LoadTeaGarden()
    {        
        StartCoroutine(MoveCameraCoroutine("Garden"));
        tree.SpawnLemons();
    }
}
