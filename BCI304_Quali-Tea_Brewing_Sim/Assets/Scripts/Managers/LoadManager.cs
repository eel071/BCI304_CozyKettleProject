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

    //GameObject teaManager;
    GameObject customer;    
    GameObject teacup;  
      
    
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
            if (customer != null) customer.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }
        //hide the customer if entering the tea station
        else if (sceneName == "TeaStation")
        {
            customer.SetActive(false);
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
        
        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);        
    }

    

    public void LoadTeaStation()
    {        
        customer = GameObject.FindWithTag("Customer");       
        DontDestroyOnLoad(customer);   
        StartCoroutine(LoadSceneCoroutine("TeaStation"));
    }

    public void LoadFrontCounter()
    {
        if (SceneManager.GetActiveScene().name == "TeaStation")
        {
            teacup = GameObject.Find("Teacup");
            DontDestroyOnLoad(teacup.transform.gameObject);
            StartCoroutine(LoadSceneCoroutine("FrontCounter"));
        }
        else
        {
            StartCoroutine(MoveCameraCoroutine("FrontCounter"));
        }
        
    }
    
    public void LoadTeaGarden()
    {        
        StartCoroutine(MoveCameraCoroutine("Garden"));
    }
}
