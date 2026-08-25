using UnityEngine;

public class Plot : MonoBehaviour
{
    public PlantManager plantManager;
    public enum PlotNumber { Plot1, Plot2, Plot3 };
    public PlotNumber plotNumber;

    [SerializeField] private bool planted = false;

    public GameObject teaBushPrefab;
    private GameObject spawnedPlant;
    private Plant plant;

    [SerializeField] private bool plotLoaded = false;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;
        //plantManager = PlantManager.uniqueInstance;
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
        planted = true;
    }

    public void LoadPlot()
    {
        if (!plotLoaded)
        {
            if (plantManager == null)
            {
                Debug.LogError("plantManager null");
                return;
            }
            switch (plotNumber)
            {
                case PlotNumber.Plot1:
                    planted = plantManager.plot1Planted;
                    break;
                case PlotNumber.Plot2:
                    planted = plantManager.plot2Planted;
                    break;
                case PlotNumber.Plot3:
                    planted = plantManager.plot3Planted;
                    break;
            }
            
            if (planted)
            {
                Debug.Log("Spawning Plant");
                SpawnPlant();
            }

            plotLoaded = true;
        }
        
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
