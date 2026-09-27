using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class ClockManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] LoadManager loadManager;
    private Tutorial tutorial;
    [SerializeField] private UIManager uiManager;
    [SerializeField] PlantManager plantManager;
    public List<Plant> plants;

    [SerializeField] CustomerSpawner customerSpawner;
    [SerializeField] ContainerManager containerManager;
    
    [SerializeField] TipJar tipJar;

    [SerializeField] private TMP_Text dayText;
    
    [SerializeField] public int dayCounter;
    
    [SerializeField] private bool testingGarden;

    public static ClockManager uniqueInstance;

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
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
    }

    public void LoadData(GameData data)
    {
        this.dayCounter = data.dayCount;
    }

    public void SaveData(ref GameData data)
    {
        data.dayCount = this.dayCounter;
    }


    void Start()
    {
        if (dayCounter == 0 && tutorial.tutorialActive == false)
        {
            dayCounter = 1;
            customerSpawner.canSpawn = true;
        }
        UpdateDayUI();
    }
    
    private void StartDay()
    {
        UpdateDayUI();
        
        if (dayCounter >= 3 || testingGarden) //if garden is unlocked
        {
            loadManager.Load("Garden");
            foreach (var p in plants)
            {
                p.LoadPlant();
            }
        }
        else OpenShop();

   
    }

    public void OpenShop()
    {
        loadManager.Load("FrontCounter");
        customerSpawner.createCustomerList();
        customerSpawner.isCustomer = false;
        if (dayCounter != 0) customerSpawner.canSpawn = true;
        tipJar.ResetTipJar();
    }

    void UpdateDayUI()
    {
        string dayString = $"Day {dayCounter}";
        dayText.text = dayString;
    }

    
    public void EndDay()
    {
        loadManager.Load("DayEnd");
        
    }

    public void NextDay()
    {
        foreach (var p in plants)
        {
            p.UpdateGrowth();            
        }        
        dayCounter++;
        plantManager.daysSinceLastHarvest++;
        StartDay();
    }

}
