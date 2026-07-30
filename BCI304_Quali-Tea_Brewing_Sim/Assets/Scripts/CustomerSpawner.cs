using UnityEngine;
using System.Collections;


public class CustomerSpawner : MonoBehaviour
{
    private TeaManager teaManager;
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
        
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
    }

    public GameObject[] customerPrefabs;
    public bool isCustomer = true;
    public bool canSpawn = true;
    public bool customerSpawned = false;
    
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
            customerSpawned = false;
            if (canSpawn)
            {
                isCustomer = true;
                StartCoroutine(WaitBeforeSpawn());
            }
        }
    }

    IEnumerator WaitBeforeSpawn()
    {
        float seconds = Random.Range(0.5f, 5f);
        yield return new WaitForSeconds(seconds);
        SpawnCustomer();
    }

    private void SpawnCustomer()
    {
        Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Length)], new Vector3(0, 0, 0), Quaternion.identity);
        customerSpawned = true;
    }
}
