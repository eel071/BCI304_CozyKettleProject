using UnityEngine;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{
    private ClockManager clockManager;
    private CustomerSpawner customerSpawner;
    [SerializeField] private TextBox tutText1;

    public bool tutorialActive = true;

    public enum TutorialStep
    {
        Welcome,
        ClickCustomer,
        AcceptOrder,
        BrewingStation,
        HotPlate,
        RemoveTeaPot
    }

    private TutorialStep currentStep;

    private bool waitingForAction = false;

    void Awake()
    {
        clockManager = FindAnyObjectByType(typeof(ClockManager)) as ClockManager;
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (clockManager != null && clockManager.dayCounter == 0)
        {
            if (tutorialActive)
            {
                currentStep = TutorialStep.Welcome;
                ShowTutorialStep();
            }
        }
    }

    public void ShowTutorialStep()
    {

        switch (currentStep)
        {
            case TutorialStep.Welcome:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("Welcome to QualiTea Brewing Sim!");
                waitingForAction = false;
                break;
            case TutorialStep.ClickCustomer:
                tutText1.SetText("Here comes your first customer! Click on them to take their order.");
                customerSpawner.SpawnFirstCustomer();
                waitingForAction = true;
                break;
            case TutorialStep.AcceptOrder:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("Awesome! Press 'Accept' to start making their tea.");
                waitingForAction = true;
                break;
            case TutorialStep.BrewingStation:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("This is the brewing station!");
                waitingForAction = false;
                break;
            case TutorialStep.HotPlate:
                tutText1.SetText("Let's boil the water! \n Drag the teapot onto the hotplate.");
                waitingForAction = true;
                break;
            case TutorialStep.RemoveTeaPot:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("Looks like it's ready! \n Drag the teapot off.");
                waitingForAction = true; 
                break;
        }   
    }



    public void FinishedText()
    {
        if (!waitingForAction)
        {
            switch (currentStep)
            {
                case TutorialStep.Welcome:
                    currentStep = TutorialStep.ClickCustomer;
                    ShowTutorialStep();
                    break;
                case TutorialStep.BrewingStation:
                    currentStep = TutorialStep.HotPlate;
                    ShowTutorialStep();
                    break;
            }
            
        }
    }

    public void ClickCustomer()
    {
        if (currentStep == TutorialStep.ClickCustomer)
        {
            currentStep = TutorialStep.AcceptOrder;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void AcceptOrder()
    {
        if (currentStep == TutorialStep.AcceptOrder)
        {
            currentStep = TutorialStep.BrewingStation;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void HotPlate()
    {
        if (currentStep == TutorialStep.HotPlate)
        {
            currentStep = TutorialStep.RemoveTeaPot;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void RemoveTeaPot()
    {
        if (currentStep == TutorialStep.RemoveTeaPot)
        {
            //move to the next step
            tutorialActive = false; //temporary
            tutText1.gameObject.SetActive(false); //temporary
        }
    }
}
