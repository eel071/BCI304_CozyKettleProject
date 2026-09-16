using UnityEngine;

public class Plot : MonoBehaviour, IOnDropBaseCollision
{
    public PlantManager plantManager;
    private ContainerManager containerManager;
    public enum PlotNumber { Plot1, Plot2, Plot3 };
    public PlotNumber plotNumber;

    public bool planted = false;

    public GameObject teaBushPrefab;
    [SerializeField] private GameObject spawnedPlant;
    private Plant plant;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
    }

    /*
    private void OnMouseDown()
    {
        if (!planted && containerManager.seedCount > 0)
        {
            containerManager.seedCount -= 1;
            SpawnPlant();
            UpdatePlanted();
        }
    }
    */

    public void OnDrop(Draggable draggable)
    {
        if(draggable.tag == "Seed" && !planted)
        {
            SpawnPlant();
            UpdatePlanted();
            Destroy(draggable.gameObject);
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
