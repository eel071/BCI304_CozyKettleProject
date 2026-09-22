using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int dayCount;
    public float money;
    public int daysSinceLastHarvest;

    public SerializableDictionary<string, int> containerItems;
    
    public SerializableDictionary<string, int> upgrades;

    //plant Manager
    public SerializableDictionary<string, int> plantGrowth;
    public SerializableDictionary<string, int> plantDecay;
    public SerializableDictionary<string, bool> plotWatered;
    public SerializableDictionary<string, bool> plotPlanted;

    public int lemonNumber;
    public SerializableDictionary<string, bool> activeLemons;

    //default values when the game starts when there is no data to load
    public GameData()
    {
        this.dayCount = 0;
        this.money = 0f;
        daysSinceLastHarvest = 0;
        lemonNumber = 0;

        activeLemons = new SerializableDictionary<string, bool>();


        containerItems = new SerializableDictionary<string, int>()
        {
            {"GreenTea", 15},
            {"BlackTea", 15},
            {"WhiteTea", 15},
            {"Lemon", 6},
            {"Sugar", 15},
            {"Milk", 5},
            {"Honey", 5},
            {"Seeds", 0}
        };

        upgrades = new SerializableDictionary<string, int>()
        {
            {"Customer", 0}
        };

        plantGrowth = new SerializableDictionary<string, int>()
        {
            {"Plot1", 0},
            {"Plot2", 0},
            {"Plot3", 0}
        };

        plantDecay = new SerializableDictionary<string, int>()
        {
            {"Plot1", 0},
            {"Plot2", 0},
            {"Plot3", 0}
        };

        plotWatered = new SerializableDictionary<string, bool>()
        {
            {"Plot1", false},
            {"Plot2", false},
            {"Plot3", false} 
        };

        plotPlanted = new SerializableDictionary<string, bool>()
        {
            {"Plot1", false},
            {"Plot2", false},
            {"Plot3", false} 
        };
    }
}
