using UnityEngine;

public class Tree : MonoBehaviour
{
    
    public GameObject lemonPrefab;
    //bool isLemon;
    public int lemonNumber;
    [SerializeField] ContainerManager containerManager;
    [SerializeField] PlantManager plantManager;
    [SerializeField] GameObject treeLemonR;
    [SerializeField] GameObject treeLemonT;
    [SerializeField] GameObject treeLemonL;

    private void Awake()
    {
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;        
        
    }   


    public void SpawnLemons()
    {
        if (plantManager.daysSinceLastHarvest >= 2 && lemonNumber < 3)
        {
            //Instantiate(lemonPrefab, new Vector3(-26.2f, 2.3f, 0f), Quaternion.identity);
            
            if (treeLemonL.activeSelf == false)
            {
                treeLemonL.SetActive(true);
            }
            else if (treeLemonR.activeSelf == false)
            {
                treeLemonR.SetActive(true);
            }
            else 
            {
                treeLemonT.SetActive(true);
            }           
            
            //isLemon = true;
            lemonNumber++;
        }
        
    }

    public void HarvestLemon()
    {
        containerManager.AddContainerCount("Lemon", 4);
        Debug.Log("lemon harvested");
        lemonNumber -= 1;
    }
}
