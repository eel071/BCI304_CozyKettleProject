using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public int plot1GrowthStage, plot2GrowthStage, plot3GrowthStage;
    public int plot1DecayStage, plot2DecayStage, plot3DecayStage;
    public bool plot1Watered, plot2Watered, plot3Watered;

    public bool plot1Planted, plot2Planted, plot3Planted;

    public int daysSinceLastHarvest = 1;
    //public bool teaFinishedGrowing, lemonFinishedGrowing;

    private static PlantManager uniqueInstance;

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
    }

    
}
