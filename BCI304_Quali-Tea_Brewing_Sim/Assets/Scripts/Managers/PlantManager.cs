using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public int teaGrowthStage, teaDecayStage;
    public bool teaWatered;
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
