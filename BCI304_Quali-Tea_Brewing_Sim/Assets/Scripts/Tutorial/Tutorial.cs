using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private ClockManager clockManager;
    private CustomerSpawner customerSpawner;
    [SerializeField] private TextBox tutText1;

    [SerializeField] private Container blackTeaContainer; 
    [SerializeField] private Container sugarContainer; 
    [SerializeField] private Container milkContainer; 

    [SerializeField] private Draggable teapotDraggable; 
    [SerializeField] private Draggable teacupDraggable; 
    
    //[SerializeField] private GameObject toAddArrow;

    public bool tutorialActive = true;
    private bool animating;

    public enum TutorialStep
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
        CompleteTutorial1
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
                teapotDraggable.SwitchDraggable(false);
                teacupDraggable.SwitchDraggable(false);
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
                teapotDraggable.SwitchDraggable(true);
                waitingForAction = true;
                break;
            case TutorialStep.RemoveTeaPot:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("Looks like the water is ready! \n Drag the teapot off.");
                teapotDraggable.SwitchDraggable(true);
                waitingForAction = true; 
                break;
            case TutorialStep.Ticket:
                tutText1.SetText("Lets check the customers order again, click on the ticket in the corner to check");
                waitingForAction = true;
                break;
            case TutorialStep.TeaLeaves:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("The customer wants a black tea. \n Drag the black tea leaves onto the teapot.");
                blackTeaContainer.SwitchInteractable(true);
                waitingForAction = true;
                break;
            case TutorialStep.FinishSteeping:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("The tea has finished steeping! \n Remove the tea leaves by clicking them.");
                waitingForAction = true;
                break;
            case TutorialStep.PourTea:
                tutText1.SetText("Now pour the tea!");
                teapotDraggable.SwitchDraggable(true);
                waitingForAction = true;
                break;
            case TutorialStep.ToAdd:
                tutText1.SetText("Perfect, let's go to the addition station!");
                waitingForAction = true;
                break;
            case TutorialStep.Additions:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("This is the addition station.");
                waitingForAction = false;
                break;
            case TutorialStep.Ingredients:
                tutText1.SetText("Add 2 sugar cubes and a dash of milk.");
                sugarContainer.SwitchInteractable(true);
                milkContainer.SwitchInteractable(true);
                waitingForAction = true;
                break;
            case TutorialStep.FinishTea:
                tutText1.SetText("The tea is ready! Drag the cup onto the serving area.");
                teacupDraggable.SwitchDraggable(true);
                waitingForAction = true;
                break;
            case TutorialStep.ServeTea:
                tutText1.gameObject.SetActive(true);
                tutText1.SetText("Drag the cup onto the customer to serve it.");
                waitingForAction = true;
                break;
            case TutorialStep.CompleteTutorial1:
                tutText1.SetText("Nice job! Looks like you got the hang of it now.");
                waitingForAction = false;
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
                case TutorialStep.Additions:
                    currentStep = TutorialStep.Ingredients;
                    ShowTutorialStep();
                    break;
                case TutorialStep.CompleteTutorial1:
                    tutText1.gameObject.SetActive(false);
                    tutorialActive = false;
                    customerSpawner.canSpawn = true;
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
            teapotDraggable.SwitchDraggable(false);
            tutText1.gameObject.SetActive(false);
        }
    }

    public void RemoveTeaPot()
    {
        if (currentStep == TutorialStep.RemoveTeaPot)
        {
            currentStep = TutorialStep.DropTeaPot;
        }
    }

    public void DropTeaPot()
    {
        if (currentStep == TutorialStep.DropTeaPot)
        {
            teapotDraggable.SwitchDraggable(false);
            currentStep = TutorialStep.Ticket;
            ShowTutorialStep();
        }
    }

    public void OpenTicket()
    {
        if (currentStep == TutorialStep.Ticket)
        {
            currentStep = TutorialStep.CloseTicket;
            tutText1.gameObject.SetActive(false);
        }
    }
    
    public void CloseTicket()
    {
        if (currentStep == TutorialStep.CloseTicket)
        {
            currentStep = TutorialStep.TeaLeaves;
            ShowTutorialStep();
        }
    }

    public void TeaLeaves()
    {
        if (currentStep == TutorialStep.TeaLeaves)
        {
            blackTeaContainer.SwitchInteractable(false);
            currentStep = TutorialStep.FinishSteeping;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void FinishSteeping()
    {
        if (currentStep == TutorialStep.FinishSteeping)
        {
            currentStep = TutorialStep.PourTea;
            ShowTutorialStep();
        }
    }

    public void PourTea()
    {
        if (currentStep == TutorialStep.PourTea)
        {
            currentStep = TutorialStep.ToAdd;
            ShowTutorialStep();
            //StartCoroutine(AnimateColourFlash(toAddArrow, true));
        }
    }

    public void ToAdd()
    {
        if (currentStep == TutorialStep.ToAdd)
        {
            currentStep = TutorialStep.Additions;
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
        if (currentStep == TutorialStep.Ingredients)
        {
            sugarContainer.SwitchInteractable(false);
            milkContainer.SwitchInteractable(false);
            currentStep = TutorialStep.FinishTea;
            ShowTutorialStep();
        }
    }

    public void FinishTea()
    {
        if (currentStep == TutorialStep.FinishTea)
        {
            currentStep = TutorialStep.ServeTea;
            tutText1.gameObject.SetActive(false);
        }
    }

    public void ServeTea()
    {
        if (currentStep == TutorialStep.ServeTea)
        {
            currentStep = TutorialStep.CompleteTutorial1;
            ShowTutorialStep();
            Debug.Log("Serve Tea");
        }
    }

}
