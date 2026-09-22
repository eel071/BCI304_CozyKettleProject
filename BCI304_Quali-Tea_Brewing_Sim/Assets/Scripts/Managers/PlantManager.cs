using UnityEngine;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour, IDataPersistence
{
    //public int plot1GrowthStage, plot2GrowthStage, plot3GrowthStage;
    public Dictionary<string, int> plantGrowthStage = new Dictionary<string, int>()
    {
        {"Plot1", 0},
        {"Plot2", 0},
        {"Plot3", 0}
    };

    //public int plot1DecayStage, plot2DecayStage, plot3DecayStage;
    public Dictionary<string, int> plantDecayStage = new Dictionary<string, int>()
    {
        {"Plot1", 0},
        {"Plot2", 0},
        {"Plot3", 0}
    };

    //public bool plot1Watered, plot2Watered, plot3Watered;
    public Dictionary<string, bool> plotWatered = new Dictionary<string, bool>()
    {
        {"Plot1", false},
        {"Plot2", false},
        {"Plot3", false}
    };

    //public bool plot1Planted, plot2Planted, plot3Planted;
    public Dictionary<string, bool> plotPlanted = new Dictionary<string, bool>()
    {
        {"Plot1", false},
        {"Plot2", false},
        {"Plot3", false}
    };

    public int daysSinceLastHarvest;
    
    //public bool teaFinishedGrowing, lemonFinishedGrowing;

    public static PlantManager uniqueInstance;

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

    public void LoadData(GameData data)
    {
        LoadPlantData(plantGrowthStage, data.plantGrowth);
        LoadPlantData(plantDecayStage, data.plantDecay);
        LoadPlotData(plotPlanted, data.plotPlanted);
        LoadPlotData(plotWatered, data.plotWatered);

        this.daysSinceLastHarvest = data.daysSinceLastHarvest;
    }

    private void LoadPlantData(Dictionary<string, int> currentDictionary, Dictionary<string, int> savedDictionary)
    {
        foreach (string key in new List<string>(currentDictionary.Keys))
        {
            if (savedDictionary.TryGetValue(key, out int count))
            {
                currentDictionary[key] = count;
            }
            else
            {
                Debug.Log($"{key} was not found in the saved data. ");
            }
        }
    }

    private void LoadPlotData(Dictionary<string, bool> currentDictionary, Dictionary<string, bool> savedDictionary)
    {
        foreach (string key in new List<string>(currentDictionary.Keys))
        {
            if (savedDictionary.TryGetValue(key, out bool value))
            {
                currentDictionary[key] = value;
            }
            else
            {
                Debug.Log($"{key} was not found in the saved data. ");
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        SavePlantData(plantGrowthStage, data.plantGrowth);
        SavePlantData(plantDecayStage, data.plantDecay);
        SavePlotData(plotPlanted, data.plotPlanted);
        SavePlotData(plotWatered, data.plotWatered);

        data.daysSinceLastHarvest = this.daysSinceLastHarvest;
    }

    private void SavePlantData(Dictionary<string, int> currentDictionary, Dictionary<string, int> savedDictionary)
    {
        foreach (string key in currentDictionary.Keys)
        {
            savedDictionary[key] = currentDictionary[key];
        }
    }

    private void SavePlotData(Dictionary<string, bool> currentDictionary, Dictionary<string, bool> savedDictionary)
    {
        foreach (string key in currentDictionary.Keys)
        {
            savedDictionary[key] = currentDictionary[key];
        }
    }
}
