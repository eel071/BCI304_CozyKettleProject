using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Customer : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private TeaManager teaManager;
    [SerializeField] private LoadManager loadManager;  
    [SerializeField] private CustomerSpawner customerSpawner;

    void Awake()
    {
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        teaManager.SetCustomerOrder();       
    }    

    void OnMouseUp()
    {
        Debug.Log($"{teaManager.customerOrder} + {teaManager.sugarCubesOrder} sugar cubes + lemon = {teaManager.lemonOrder}");
        loadManager.LoadTeaStation();
    }

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup")
        {
            Destroy(draggable.transform.parent.gameObject);
            teaManager.ResetTea();   
            customerSpawner.isCustomer = false;
            Destroy(gameObject);
        }
    }
}
