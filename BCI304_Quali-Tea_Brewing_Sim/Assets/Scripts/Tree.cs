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

        if (plantManager.daysSinceLastHarvest >= 2)
        {
            SpawnLemons();
        }
    }

    private void SpawnLemons()
    {
        Instantiate(lemonPrefab, new Vector3(5.5f, 3f, 0f), Quaternion.identity);
        isLemon = true;
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
