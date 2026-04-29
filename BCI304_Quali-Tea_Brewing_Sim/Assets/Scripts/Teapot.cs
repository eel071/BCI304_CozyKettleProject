using System;
using UnityEngine;

public class Teapot : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{    
    [SerializeField] private TeaManager teaManager;
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

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Tea" && waterHeating == false && waterHeated == true && teaSteeping == false && teaSteeped == false)
        {
            teaSteeping = true;
            Debug.Log($"Steeping {draggable.gameObject.name}");                      
            draggable.transform.position = transform.position + new Vector3(-0.19f, 1.25f, 0);

            //steeping progress bar
            steepBar.SetActive(true); 
            progressBar.SetBar(maxSteepTime, steepGoal);
        }        
        else
        {
            Debug.Log($"Tried to Add {draggable.tag} to teapot");
            draggable.transform.position = draggable.startPosition;
        }
        
    }

    public void OnPickUp(Draggable draggable)
    {
        Debug.Log("on pick up activated");

        if (draggable.tag == "Tea") 
        {            
            Destroy(draggable.gameObject);
            Debug.Log($"Steeping has stopped");
            finalSteep = steepTimer;
            teaSteeping = false;
            steepTimer = 0f;
            // Debug.Log($"Steep Timer : {steepTimer}");
            Debug.Log($"Steep Final Time : {finalSteep}");
            teaSteeped = true;  
            steepBar.SetActive(false);
            
            string teaType = draggable.name;
            teaManager.SetTea(teaType, finalSteep, steepGoal); 
        }
    }

    private void Update()
    {
        if (teaSteeping == true && steepTimer <= maxSteepTime)
        {
            steepTimer += Time.deltaTime;
            // Debug.Log($"Steep Timer : {steepTimer}");
            progressBar.SetProgress(steepTimer); //update progress bar
        }
    }
    public void ResetTeapot()
    {
        waterHeated = false;
        teaSteeped = false;
        hotPlate.finalTime = 0f;
        finalSteep = 0f;
        teaManager.ResetTea();        
    }

    public void PourTea()
    {
        anim.SetBool("Pouring", true);
    }
    
    public void StopPouring()
    {
        anim.SetBool("Pouring", false);
    }
}
