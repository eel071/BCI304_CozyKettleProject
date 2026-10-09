using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private ClockManager clockManager;
    private CustomerSpawner customerSpawner;
    [SerializeField] private TextBox tutText1;
    [SerializeField] private TextBox tutText2;

    private ContainerManager containerManager;

    [SerializeField] private Container blackTeaContainer; 
    [SerializeField] private Container sugarContainer; 
    [SerializeField] private Container milkContainer; 

    [SerializeField] private Draggable teapotDraggable; 
    [SerializeField] private Draggable teacupDraggable; 
    
    //[SerializeField] private GameObject toAddArrow;

    public bool tutorialActive = true;
    public bool playingTutorial = true;

    private int tutorial = 1;
    //Tutorial 1: Tea shop on day 0
    public enum Tutorial1Step
    {
        Welcome,
        ClickCustomer,
        AcceptOrder,
        BrewingStation,
        HotPlate,
        RemoveTeaPot,
        DropTeaPot,
        Ticket,
        CloseTicket,
        TeaLeaves,
        FinishSteeping,
        PourTea,
        ToAdd,
        Additions,
        Ingredients,
        FinishTea,
        ServeTea,
        CompleteTutorial1,
        Finished
    }
    private Tutorial1Step currentStep1;
    

    //Tutorial 2: The Garden
    public enum Tutorial2Step
    {
        Garden,
        Lemon,
        Seed,
        Water,
        FinishedWatering,
        StartDay  
    }
    
    private Tutorial2Step currentStep2;

    private bool waitingForAction = false;

    void Awake()
    {
        clockManager = FindAnyObjectByType(typeof(ClockManager)) as ClockManager;
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (clockManager != null && clockManager.dayCounter == 0)
        {
            if (tutorialActive)
            {
                teapotDraggable.SwitchDraggable(false);
                teacupDraggable.SwitchDraggable(false);
                currentStep1 = Tutorial1Step.Welcome;
                ShowTutorialStep();
            }
        }
    }

    public void ShowTutorialStep()
    {
        if (tutorial == 1)
        {
            switch (currentStep1)
            {
                case Tutorial1Step.Welcome:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("Welcome to QualiTea Brewing Sim!");
                    waitingForAction = false;
                    break;
                case Tutorial1Step.ClickCustomer:
                    tutText1.SetText("Here comes your first customer! Click on them to take their order.");
                    customerSpawner.SpawnFirstCustomer();
                    waitingForAction = true;
                    break;
                case Tutorial1Step.AcceptOrder:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("Awesome! Press 'Accept' to start making their tea.");
                    waitingForAction = true;
                    break;
                case Tutorial1Step.BrewingStation:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("This is the brewing station!");
                    waitingForAction = false;
                    break;
                case Tutorial1Step.HotPlate:
                    tutText1.SetText("Let's boil the water! \n Drag the teapot onto the hotplate.");
                    teapotDraggable.SwitchDraggable(true);
                    waitingForAction = true;
                    break;
                case Tutorial1Step.RemoveTeaPot:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("Looks like the water is ready! \n Drag the teapot off.");
                    teapotDraggable.SwitchDraggable(true);
                    waitingForAction = true; 
                    break;
                case Tutorial1Step.Ticket:
                    tutText1.SetText("Lets check the customers order again, click on the ticket in the corner to check");
                    waitingForAction = true;
                    break;
                case Tutorial1Step.TeaLeaves:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("The customer wants a black tea. \n Drag the black tea leaves onto the teapot.");
                    blackTeaContainer.SwitchInteractable(true);
                    waitingForAction = true;
                    break;
                case Tutorial1Step.FinishSteeping:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("The tea has finished steeping! \n Remove the tea leaves by clicking them.");
                    waitingForAction = true;
                    break;
                case Tutorial1Step.PourTea:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("Now pour the tea!");
                    teapotDraggable.SwitchDraggable(true);
                    waitingForAction = true;
                    break;
                case Tutorial1Step.ToAdd:
                    tutText1.SetText("Perfect, let's go to the addition station!");
                    waitingForAction = true;
                    break;
                case Tutorial1Step.Additions:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("This is the addition station.");
                    waitingForAction = false;
                    break;
                case Tutorial1Step.Ingredients:
                    tutText1.SetText("Add 2 sugar cubes and a dash of milk.");
                    sugarContainer.SwitchInteractable(true);
                    milkContainer.SwitchInteractable(true);
                    waitingForAction = true;
                    break;
                case Tutorial1Step.FinishTea:
                    tutText1.gameObject.SetActive(false);
                    tutText2.gameObject.SetActive(true);
                    tutText2.SetText("The tea is ready! Drag the cup onto the serving area.");
                    teacupDraggable.SwitchDraggable(true);
                    waitingForAction = true;
                    break;
                case Tutorial1Step.ServeTea:
                    tutText2.gameObject.SetActive(true);
                    tutText2.SetText("Drag the cup onto the customer to serve it.");
                    waitingForAction = true;
                    break;
                case Tutorial1Step.CompleteTutorial1:
                    tutText2.gameObject.SetActive(false);
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("Nice job! Try serving this next customer on your own.");
                    waitingForAction = false;
                    break;
            }
        }
        else if (tutorial == 2)
        {
            switch (currentStep2)
            {
                case Tutorial2Step.Garden:
                    tutText1.gameObject.SetActive(true);
                    tutText1.SetText("Welcome to the garden! Here you can grow ingredients for your shop.");
                    waitingForAction = false;
                    break;
                case Tutorial2Step.Lemon:
                    tutText1.SetText("Looks like a lemon has already grown! \n Click on the lemon to pick it.");
                    waitingForAction = true;
                    break;              
                case Tutorial2Step.Seed:
                    tutText1.SetText("You can also grow your very own tea leaves. \n Plant a tea bush by dragging a seed onto one of the plots.");
                    waitingForAction = true;
                    break;
                case Tutorial2Step.Water:
                    tutText1.SetText("Now drag the watering can over the plot to water it.");
                    waitingForAction = true;
                    break;
                case Tutorial2Step.FinishedWatering:
                    tutText1.SetText("Nice job! You'll be able to harvest some tea leaves in a few days as long as you remember to water the plant each morning.");
                    waitingForAction = false;
                    break;
                case Tutorial2Step.StartDay:
                    tutText1.SetText("Now you can start the day.");
                    waitingForAction = true;
                    break;
                    
            }
        }
    }

    public void FinishedText()
    {
        if (!waitingForAction)
        {
            if (tutorial == 1)
            {
                switch (currentStep1)
                {
                    case Tutorial1Step.Welcome:
                        currentStep1 = Tutorial1Step.ClickCustomer;
                        ShowTutorialStep();
                        break;
                    case Tutorial1Step.BrewingStation:
                        currentStep1 = Tutorial1Step.HotPlate;
                        ShowTutorialStep();
                        break;
                    case Tutorial1Step.Additions:
                        currentStep1 = Tutorial1Step.Ingredients;
                        ShowTutorialStep();
                        break;
                    case Tutorial1Step.CompleteTutorial1:
                        currentStep1 = Tutorial1Step.Finished;
                        tutText1.gameObject.SetActive(false);
                        customerSpawner.canSpawn = true;
                        tutorialActive = false;
                        tutorial = 2;
                        containerManager.ToggleContainersInteractable(true);
                        break;
                }
            }
            else if (tutorial == 2)
            {
                switch (currentStep2)
                {
                    case Tutorial2Step.Garden:
                        currentStep2 = Tutorial2Step.Lemon;
                        ShowTutorialStep();
                        break;
                    case Tutorial2Step.FinishedWatering:
                        currentStep2 = Tutorial2Step.StartDay;
                        ShowTutorialStep();
                        break;
                }
            }
        }
        
    }

    #region Tutorial1

    public void ClickCustomer()
    {
        if (currentStep1 == Tutorial1Step.ClickCustomer)
        {
            currentStep1 = Tutorial1Step.AcceptOrder;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void AcceptOrder()
    {
        if (currentStep1 == Tutorial1Step.AcceptOrder)
        {
            currentStep1 = Tutorial1Step.BrewingStation;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void HotPlate()
    {
        if (currentStep1 == Tutorial1Step.HotPlate)
        {
            currentStep1 = Tutorial1Step.RemoveTeaPot;
            teapotDraggable.SwitchDraggable(false);
            tutText1.gameObject.SetActive(false);
        }
    }

    public void RemoveTeaPot()
    {
        if (currentStep1 == Tutorial1Step.RemoveTeaPot)
        {
            currentStep1 = Tutorial1Step.DropTeaPot;
        }
    }

    public void DropTeaPot()
    {
        if (currentStep1 == Tutorial1Step.DropTeaPot)
        {
            teapotDraggable.SwitchDraggable(false);
            currentStep1 = Tutorial1Step.Ticket;
            ShowTutorialStep();
        }
    }

    public void OpenTicket()
    {
        if (currentStep1 == Tutorial1Step.Ticket)
        {
            currentStep1 = Tutorial1Step.CloseTicket;
            tutText1.gameObject.SetActive(false);
        }
    }
    
    public void CloseTicket()
    {
        if (currentStep1 == Tutorial1Step.CloseTicket)
        {
            currentStep1 = Tutorial1Step.TeaLeaves;
            ShowTutorialStep();
        }
    }

    public void TeaLeaves()
    {
        if (currentStep1 == Tutorial1Step.TeaLeaves)
        {
            blackTeaContainer.SwitchInteractable(false);
            currentStep1 = Tutorial1Step.FinishSteeping;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void FinishSteeping()
    {
        if (currentStep1 == Tutorial1Step.FinishSteeping)
        {
            currentStep1 = Tutorial1Step.PourTea;
            ShowTutorialStep();
        }
    }

    public void PourTea()
    {
        if (currentStep1 == Tutorial1Step.PourTea)
        {
            currentStep1 = Tutorial1Step.ToAdd;
            ShowTutorialStep();
            //StartCoroutine(AnimateColourFlash(toAddArrow, true));
        }
    }

    public void ToAdd()
    {
        if (currentStep1 == Tutorial1Step.ToAdd)
        {
            currentStep1 = Tutorial1Step.Additions;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void SugarCubes()
    {
        sugarContainer.SwitchInteractable(false);
    }

    public void Milk()
    {
        milkContainer.SwitchInteractable(false);
    }

    public void Ingredients()
    {
        if (currentStep1 == Tutorial1Step.Ingredients)
        {
            sugarContainer.SwitchInteractable(false);
            milkContainer.SwitchInteractable(false);
            currentStep1 = Tutorial1Step.FinishTea;
            ShowTutorialStep();
        }
    }

    public void FinishTea()
    {
        if (currentStep1 == Tutorial1Step.FinishTea)
        {
            currentStep1 = Tutorial1Step.ServeTea;
            tutText2.gameObject.SetActive(false);
        }
    }

    public void ServeTea()
    {
        if (currentStep1 == Tutorial1Step.ServeTea)
        {
            currentStep1 = Tutorial1Step.CompleteTutorial1;
            ShowTutorialStep();
            Debug.Log("Serve Tea");
        }
    }

    #endregion

    #region Tutorial2
    public void Lemon()
    {
        if (currentStep2 == Tutorial2Step.Lemon)
        {
            currentStep2 = Tutorial2Step.Seed;
            ShowTutorialStep();
        }
    }

    public void Seed()
    {
        if (currentStep2 == Tutorial2Step.Seed)
        {
            currentStep2 = Tutorial2Step.Water;
            ShowTutorialStep();
        }
    }

    public void Water()
    {
        if (currentStep2 == Tutorial2Step.Water)
        {
            currentStep2 = Tutorial2Step.FinishedWatering;
            ShowTutorialStep();
        }
    }

    public void StartDay()
    {
        if (currentStep2 == Tutorial2Step.StartDay)
        {
            tutText1.gameObject.SetActive(false);
            tutorialActive = false;            
        }
    }

    #endregion

}
