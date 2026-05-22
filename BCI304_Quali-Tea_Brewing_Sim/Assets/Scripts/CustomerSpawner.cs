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
            isCustomer = true;
            StartCoroutine(WaitBeforeSpawn());
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
        Instantiate(customerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        isCustomer = true;
    }
}
