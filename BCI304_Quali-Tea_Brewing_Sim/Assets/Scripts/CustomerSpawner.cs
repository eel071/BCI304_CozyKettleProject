using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class CustomerSpawner : MonoBehaviour
{
    private TeaManager teaManager;
    private static CustomerSpawner uniqueInstance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bellAudio;

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
    [SerializeField] private List<GameObject> customers = new List<GameObject>();
    public bool isCustomer = true;
    public bool canSpawn = true;
    public bool customerSpawned = false;
    
    void Start()
    {
        if (GameObject.FindWithTag("Customer") == null)
        {
            createCustomerList();
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

    private void createCustomerList()
    {
        customers = customerPrefabs.ToList();
    }


    private void SpawnCustomer()
    {
        if (customers.Count <= 0) createCustomerList(); // **TEMPORARY** will eventually end the day instead of regenerating the customer list.

        if (audioSource != null) audioSource.PlayOneShot(bellAudio);
        int randomCustomer = Random.Range(0, customers.Count); //choose a random customer
        Instantiate(customers[randomCustomer], new Vector3(0, 0, 0), Quaternion.identity); //instantiate the random customer
        customers.RemoveAt(randomCustomer); //remove the random customer from the list.
        customerSpawned = true;
    }
}
