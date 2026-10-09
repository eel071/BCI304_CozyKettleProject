using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class ClockManager : MonoBehaviour, IDataPersistence
{
    private Tutorial tutorial;
    [SerializeField] LoadManager loadManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] PlantManager plantManager;
    private BankManager bankManager;
    public List<Plant> plants;

    [SerializeField] CustomerSpawner customerSpawner;
    [SerializeField] ContainerManager containerManager;
    
    [SerializeField] TipJar tipJar;

    [SerializeField] private TMP_Text dayText;
    
    [SerializeField] public int dayCounter;
    
    [SerializeField] private bool testingGarden;

    public static ClockManager uniqueInstance;

    //bool gardenUnlocked = false;

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
        bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
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
        StartDay();       
    }
    
    private void StartDay()
    {
        DataPersistenceManager.instance.SaveGame();
        UpdateDayUI();

        if (dayCounter == 0 && tutorial.tutorialActive == true)
        {
            customerSpawner.canSpawn = false;
            containerManager.ToggleContainersInteractable(false);
        }
        else if (dayCounter == 0 && tutorial.tutorialActive == false)
        {
            dayCounter = 1;
            customerSpawner.canSpawn = true;
            containerManager.ToggleContainersInteractable(true);
        }
        else customerSpawner.canSpawn = true;

        if (dayCounter >= 1|| testingGarden) //if garden is unlocked
        {
            if (tutorial.playingTutorial)
            {
                tutorial.tutorial = 2;
                tutorial.tutorialActive = true;
                tutorial.ShowTutorialStep();
            }
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
        customerSpawner.CreateCustomerList();
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
