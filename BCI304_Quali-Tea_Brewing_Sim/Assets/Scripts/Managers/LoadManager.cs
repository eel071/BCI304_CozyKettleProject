using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
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
    }
    /*private void Start()
    {
        teaManager = GameObject.Find("TeaManager");
    }*/
    public void LoadTeaStation()
    {        
        customer = GameObject.Find("Customer(Clone)");
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(teaManager);
        DontDestroyOnLoad(customer);        
        SceneManager.LoadScene("TeaStation");
        customer.SetActive(false);
    }
    public void LoadFrontCounter()
    {        
        teacup = GameObject.Find("Teacup");
        DontDestroyOnLoad(teacup.transform.parent.gameObject);        
        SceneManager.LoadScene("FrontCounter");
        customer.SetActive(true);
    }
}
