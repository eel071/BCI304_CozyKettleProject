using UnityEngine;

public class Plot : MonoBehaviour
{
    //[SerializeField] PlantManager plantManager;
    public enum PlotNumber { Plot1, Plot2, Plot3 };
    [SerializeField] public PlotNumber plotNumber;

    public GameObject teaBushPrefab;
    private GameObject spawnedPlant;
    private Plant plant;

    private void Start()
    {        

            SpawnPlant();
    }

    private void SpawnPlant()
    {
        spawnedPlant = Instantiate(teaBushPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);           
        plant = spawnedPlant.GetComponent<Plant>();
        plant.plotNumber = plotNumber;
    }

}
