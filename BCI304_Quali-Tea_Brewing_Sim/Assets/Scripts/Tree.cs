using UnityEngine;

public class Tree : MonoBehaviour
{
    
    public GameObject lemonPrefab;
    bool isLemon;
    [SerializeField] ContainerManager containerManager;
    [SerializeField] PlantManager plantManager;

    private void Awake()
    {
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;        
    }

    private void Update()
    {
        if (!isLemon)
        { 
            SpawnLemons(); 
        }
    }


    private void SpawnLemons()
    {
        if (plantManager.daysSinceLastHarvest >= 2)
        {
            Instantiate(lemonPrefab, new Vector3(-26.2f, 2.3f, 0f), Quaternion.identity);
            isLemon = true;
        }
        
    }

    private void OnMouseDown()
    {
        if (isLemon == true) //harvest the plant
        {
            containerManager.AddLemons();
            Debug.Log("lemon harvested");
            isLemon = false;
            plantManager.daysSinceLastHarvest = 0;
            GameObject lemon = GameObject.Find("Lemon(Clone)");
            Destroy(lemon);
        }
    }
}
