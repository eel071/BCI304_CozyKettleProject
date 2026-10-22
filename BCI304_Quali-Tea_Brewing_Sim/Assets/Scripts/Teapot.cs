using System;
using UnityEngine;

public class Teapot : MonoBehaviour, IOnDropBaseCollision //IOnPickUpBaseCollision
{    

    public TeaTypes teapotTea;

    [SerializeField] private TeaManager teaManager;
    private Tutorial tutorial;
    private Animator anim;

    private float steepTimer = 0f;
    public float finalSteep = 0f;
    public bool teaSteeping = false;
    public bool teaSteeped = false;
    public bool waterHeating = false;
    public bool waterHeated = false;
    
    
    [SerializeField] private float maxSteepTime = 5f; //max time for steeping
    public float steepGoal = 3f; //this could be changed depending on the tea type

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameObject steepBar;
    [SerializeField] private HotPlate hotPlate;

    private Draggable teaDraggable;

    private Collider2D col;

    private string teaType;
    [SerializeField] private GameObject steepButton;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<Collider2D>();
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
    }

    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Tea" && waterHeating == false && waterHeated == true && teaSteeping == false && teaSteeped == false)
        {
            col.enabled = false;
            if (tutorial.tutorialActive)
            {
                teaDraggable = draggable;
                tutorial.TeaLeaves();
            }
            teaSteeping = true;
            Debug.Log($"Steeping {draggable.gameObject.name}");                      
            draggable.transform.position = transform.position + new Vector3(-0.19f, 1.25f, 0);

            ContainerItem containerItem = draggable.GetComponent<ContainerItem>();

            //set steep goal & teapotTea
            switch (containerItem.item)
            {
                case ContainerItems.BlackLeaves:
                    steepGoal = 4f;
                    teapotTea = TeaTypes.Black;
                    break;
                case ContainerItems.WhiteLeaves:
                    steepGoal = 3f;
                    teapotTea = TeaTypes.White;
                    break;
                case ContainerItems.GreenLeaves:
                    steepGoal = 2f;
                    teapotTea = TeaTypes.Green;
                    break;
            }
            
            //steeping progress bar
            steepBar.SetActive(true); 
            progressBar.SetBar(maxSteepTime, steepGoal);
            Destroy(draggable.gameObject);
            steepButton.SetActive(true);
        }        
        else
        {
            Debug.Log($"Tried to Add {draggable.tag} to teapot");
            draggable.ReturnItem();
            //draggable.transform.position = draggable.startPosition;
        }
        
    }

    public void StopSteeping()
    {
        col.enabled = true;
        if (tutorial.tutorialActive)
        {
            tutorial.FinishSteeping();
        }
        steepButton.SetActive(false);
        finalSteep = steepTimer;
        teaSteeping = false;
        steepTimer = 0f;
        Debug.Log($"Steep Final Time : {finalSteep}");
        teaSteeped = true;  
        steepBar.SetActive(false);
        teaManager.UpdateSteepValue(finalSteep, steepGoal); 
    }

    private void Update()
    {
        if (teaSteeping == true && steepTimer <= maxSteepTime)
        {
            steepTimer += Time.deltaTime;
            // Debug.Log($"Steep Timer : {steepTimer}");
            progressBar.SetProgress(steepTimer); //update progress bar

            if (tutorial.tutorialActive && steepTimer >= steepGoal)
            {
                teaSteeping = false;
                if (teaDraggable != null) teaDraggable.SwitchDraggable(true);
                tutorial.ShowTutorialStep();
            }
        }

        
    }
    public void ResetTeapot()
    {
        waterHeated = false;
        teaSteeped = false;
        hotPlate.finalTime = 0f;
        finalSteep = 0f;
        teaManager.ResetTea();     
        teapotTea = TeaTypes.Water;   
    }

    public void PourTea()
    {
        anim.SetBool("Pouring", true);
    }
    
    public void StopPouring()
    {
        if (tutorial.tutorialActive)
        {
            tutorial.PourTea();
        }
        anim.SetBool("Pouring", false);
    }
}
