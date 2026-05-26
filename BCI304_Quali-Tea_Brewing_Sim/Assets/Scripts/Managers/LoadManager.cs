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
    
    public void LoadTeaStation()
    {        
        customer = GameObject.Find("Customer(Clone)");                
        DontDestroyOnLoad(customer);        
        SceneManager.LoadScene("TeaStation");
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
        
        SceneManager.LoadScene("FrontCounter");
    }
    public void LoadTeaGarden()
    {        
        SceneManager.LoadScene("TeaGarden");
    }
}
