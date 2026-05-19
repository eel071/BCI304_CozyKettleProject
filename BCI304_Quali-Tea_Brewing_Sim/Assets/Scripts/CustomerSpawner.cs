using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    private static CustomerSpawner uniqueInstance;
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
    public GameObject customerPrefab;
    public bool isCustomer = true;
    
    void Start()
    {
        if (GameObject.Find("Customer(Clone)") == null)
        {
            SpawnCustomer();
        }

    }
    private void Update()
    {
        if (isCustomer == false)
        {
            SpawnCustomer();
        }
    }


    private void SpawnCustomer()
    {
        Instantiate(customerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        isCustomer = true;
    }
}
