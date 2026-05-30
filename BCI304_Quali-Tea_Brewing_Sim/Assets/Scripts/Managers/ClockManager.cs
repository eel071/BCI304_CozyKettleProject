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
    
    [SerializeField] private TMP_Text clockText;
    [SerializeField] private TMP_Text dayText;
    
    private float elapsedTime;
    [SerializeField] private float timeScale = 24f;
    [SerializeField] private float timeInADay = 86400f; //24 hours in seconds 
    [SerializeField] private int dayCounter = 1;
    
    private float startOfDay = 32400f; //9am
    private float endOfDay = 57600f; //4pm

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

    void Start()
    {
        elapsedTime = startOfDay;
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
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);        

        string clockString = string.Format("{0:00}:{1:00}", hours, minutes);
        clockText.text = clockString;
        string dayString = $"Day {dayCounter}";
        dayText.text = dayString;
    }
    
    void CheckTime()
    {
        if (elapsedTime >= endOfDay && SceneManager.GetActiveScene().name != "TeaGarden")
        {
            dayPhase = 1;
            customerSpawner.canSpawn = false;
        }
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
        elapsedTime = startOfDay;
        nextDayButton.gameObject.SetActive(false);
        customerSpawner.isCustomer = false;
        dayPhase = 0;
        customerSpawner.canSpawn = true;
    }
}
