using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public int teaGrowthStage, teaDecayStage, lemonGrowthStage, lemonDecayStage;
    public bool teaWatered, lemonWatered;
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
