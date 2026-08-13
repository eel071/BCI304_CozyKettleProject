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
    [SerializeField] Button nextDayButton;
    [SerializeField] CustomerSpawner customerSpawner;
    [SerializeField] ContainerManager containerManager;

    //[SerializeField] private TMP_Text clockText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Image radialClock;
    
    [SerializeField] private float elapsedTime;
    
    [SerializeField] private float averageTime = 60f;
    [SerializeField] private float timeInADay;

    [SerializeField] private float timeScale = 24f;
    //[SerializeField] private float timeInADay = 86400f; //24 hours in seconds 
    [SerializeField] private int dayCounter = 1;
    
    //private float startOfDay = 32400f; //9am
    //private float endOfDay = 57600f; //4pm

    private int dayPhase = 0;

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

    private void StartDay()
    {
       timeInADay = averageTime * customerSpawner.maxCustomers; 
       elapsedTime = 0;
    }

    void Start()
    {
        StartDay();
        //elapsedTime = startOfDay;
    }
        
    void Update()
    {
        if (dayPhase == 0)
        {
            elapsedTime += Time.deltaTime * timeScale;
            elapsedTime %= timeInADay;
            UpdateClockUI();
            CheckTime();
        }
        if (dayPhase == 1 && !customerSpawner.customerSpawned)
        {
            DayEnd();
        }        
    }

    void UpdateClockUI()
    {
        radialClock.fillAmount = ((timeInADay - elapsedTime) / timeInADay);

        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);        

        //string clockString = string.Format("{0:00}:{1:00}", hours, minutes);
        //clockText.text = clockString;
        string dayString = $"Day {dayCounter}";
        dayText.text = dayString;
    }
    
    public void EndDayEarly()
    {
        dayPhase = 1;
    }

    void CheckTime()
    {
        if (elapsedTime >= timeInADay && SceneManager.GetActiveScene().name != "TeaGarden")
        {
            dayPhase = 1;
            customerSpawner.canSpawn = false;
        }

        /*
        if (elapsedTime >= endOfDay && SceneManager.GetActiveScene().name != "TeaGarden")
        {
            dayPhase = 1;
            customerSpawner.canSpawn = false;
        }
        */
    }

    void DayEnd()
    {
        dayPhase = 2;
        loadManager.LoadTeaGarden();
        nextDayButton.gameObject.SetActive(true);
    }   

    public void NextDay()
    {
        foreach (var Plant in plants)
        {
            Plant.UpdateGrowth();
        }        
        dayCounter++;
        plantManager.daysSinceLastHarvest++;
        loadManager.LoadFrontCounter();
        plants.Clear();
        dayPhase = 0;
        StartDay();
        //elapsedTime = startOfDay;
        nextDayButton.gameObject.SetActive(false);
        customerSpawner.createCustomerList();
        customerSpawner.isCustomer = false;
        customerSpawner.canSpawn = true;
        containerManager.sugarCount = containerManager.sugarMax; //temporarily reset sugar count since we have no way to replenish it atm
    }
}
