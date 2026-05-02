using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.Find("Customer(Clone)") == null)
        {
            SpawnCustomer();
        }

    }
    

    private void SpawnCustomer()
    {
        Instantiate(customer, new Vector3(0, 0, 0), Quaternion.identity);        
    }
}
