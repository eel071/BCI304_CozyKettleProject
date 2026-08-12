using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private float sceneFadeDuration;
    private SceneFade sceneFade;
    //GameObject teaManager;
    GameObject customer;    
    GameObject teacup;    
    
    private static LoadManager uniqueInstance;
    private void Awake()
    {
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
        SceneManager.LoadScene(sceneName);
        
        //show the customer if entering the front counter
        if (sceneName == "FrontCounter")
        {
            if (customer != null) customer.SetActive(true);
        }

        //hide the customer if entering the tea station
        else if (sceneName == "TeaStation")
        {
            customer.SetActive(false);
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
        }
        StartCoroutine(LoadSceneCoroutine("FrontCounter"));
    }
    
    public void LoadTeaGarden()
    {        
        StartCoroutine(LoadSceneCoroutine("TeaGarden"));
    }
}
