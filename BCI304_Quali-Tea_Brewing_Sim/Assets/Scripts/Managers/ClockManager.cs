using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class ClockManager : MonoBehaviour
{
    [SerializeField] LoadManager loadManager;
    [SerializeField] PlantManager plantManager;
    public List<Plant> plants;
    [SerializeField] Button openShopButton;
    [SerializeField] CustomerSpawner customerSpawner;
    [SerializeField] ContainerManager containerManager;

    [SerializeField] private TMP_Text dayText;
    
    [SerializeField] private int dayCounter = 1;

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
    }

    void Start()
    {
        UpdateDayUI();
    }
    
    private void StartDay()
    {
        UpdateDayUI();
        if (dayCounter >= 3) //if garden is unlocked
        {
            loadManager.LoadTeaGarden();
            foreach (var p in plants)
            {
                p.LoadPlant();
            }
            openShopButton.gameObject.SetActive(true); 
        }
        else OpenShop();
        
    }

    public void OpenShop()
    {
        loadManager.LoadFrontCounter();
        openShopButton.gameObject.SetActive(false);
        customerSpawner.createCustomerList();
        customerSpawner.isCustomer = false;
        customerSpawner.canSpawn = true;
    }

    void UpdateDayUI()
    {
        string dayString = $"Day {dayCounter}";
        dayText.text = dayString;
    }

    public void EndDay()
    {
        foreach (var p in plants)
        {
            p.UpdateGrowth();            
        }        
        dayCounter++;
        plantManager.daysSinceLastHarvest++;
        containerManager.sugarCount = containerManager.sugarMax; //temporarily reset sugar count since we have no way to replenish it atm
        StartDay();
    }
}
