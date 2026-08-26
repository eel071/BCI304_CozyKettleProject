using UnityEngine;

public class Plot : MonoBehaviour
{
    public PlantManager plantManager;
    public enum PlotNumber { Plot1, Plot2, Plot3 };
    public PlotNumber plotNumber;

    public bool planted = false;

    public GameObject teaBushPrefab;
    [SerializeField] private GameObject spawnedPlant;
    private Plant plant;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;
    }

    private void Start()
    {
        //LoadPlot();
    }

    private void OnMouseDown()
    {
        if (!planted && plantManager.seeds > 0)
        {
            plantManager.seeds -= 1;
            SpawnPlant();
            UpdatePlanted();
        }
    }

    private void SpawnPlant()
    {
        spawnedPlant = Instantiate(teaBushPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);           
        plant = spawnedPlant.GetComponent<Plant>();
        plant.plotNumber = plotNumber;
        plant.plot = this;
        planted = true;
    }

    private void UpdatePlanted()
    {
        switch(plotNumber)
        { 
            case PlotNumber.Plot1:
                plantManager.plot1Planted = planted;
                break;
            case PlotNumber.Plot2:
                plantManager.plot2Planted = planted;
                break;
            case PlotNumber.Plot3:
                plantManager.plot3Planted = planted;
                break;
        }
    }

}
