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
    }

    private IEnumerator LoadSceneCoroutine(string sceneName) //fade in and out when loading a scene
    {
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        SceneManager.LoadScene(sceneName);
        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);
    }
    

    public void LoadTeaStation()
    {        
        customer = GameObject.Find("Customer(Clone)");                
        DontDestroyOnLoad(customer);   
        StartCoroutine(LoadSceneCoroutine("TeaStation"));
        customer.SetActive(false);
    }
    public void LoadFrontCounter()
    {
        if (SceneManager.GetActiveScene().name == "TeaStation")
        {
            teacup = GameObject.Find("Teacup");
            DontDestroyOnLoad(teacup.transform.gameObject);
            customer.SetActive(true);
        }
        StartCoroutine(LoadSceneCoroutine("FrontCounter"));
    }
    public void LoadTeaGarden()
    {        
        StartCoroutine(LoadSceneCoroutine("TeaGarden"));
    }
}
