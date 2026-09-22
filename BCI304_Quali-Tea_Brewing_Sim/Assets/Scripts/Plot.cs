using UnityEngine;

public class Plot : MonoBehaviour, IOnDropBaseCollision
{
    public PlantManager plantManager;
    public ContainerManager containerManager;
    public enum PlotNumber { Plot1, Plot2, Plot3 };
    public PlotNumber plotNumber;

    public bool planted = false;

    //public GameObject teaBushPrefab;
    [SerializeField] private GameObject teaBush;
    [SerializeField] private Plant plantScript;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
    }

    public void OnDrop(Draggable draggable)
    {
        Debug.Log("onDrop plot");
        if (draggable.tag == "Seed" && !planted)
        {
            SpawnPlant();
            UpdatePlanted();
            Destroy(draggable.gameObject);
        }
        else if (draggable.tag == "WateringCan" && planted)
        {
            if (plantScript == null) plantScript = GetComponentInChildren<Plant>();
            plantScript.WaterPlant(draggable);
        }        
        else draggable.ReturnItem();
    }

    private void SpawnPlant()
    {
        teaBush.SetActive(true);
        plantScript.LoadPlant();
        //teaBush = Instantiate(teaBushPrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);           
        //plant = teaBush.GetComponent<Plant>();
        plantScript.plotNumber = plotNumber;
        //plant.plot = this;
        planted = true;
    }

    private void UpdatePlanted()
    {
        string plot = plotNumber.ToString();

        plantManager.plotPlanted[plot] = planted;
        
        /*switch(plotNumber)
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
        */
    }

}
