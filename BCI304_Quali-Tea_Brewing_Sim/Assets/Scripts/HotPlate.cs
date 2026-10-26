using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    private float waterTimer = 0f;
    public float finalTime = 0f;
    private float heatSpeed = 1f;

    [SerializeField] private TeaManager teaManager;
    [SerializeField] private Teapot teapot;
    private UpgradeManager upgradeManager;
    
    private Tutorial tutorial;

    [SerializeField] private float maxHeatTime = 10f;
    public float tempGoal = 8f; //will be changed depending on tea type

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameObject heatBar;

    [SerializeField] private AudioClip boilingSound;
    [SerializeField] private AudioSource boilingSFX;

    [SerializeField] private Sprite plateOffSprite;
    [SerializeField] private Sprite plateOnSprite;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Animator steamAnimator;

    private Draggable teapotDraggable;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
        upgradeManager = FindAnyObjectByType(typeof(UpgradeManager)) as UpgradeManager;
    }

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teapot" && teapot.waterHeated == false && teapot.waterHeating == false)
        {
            heatSpeed = 1f + (upgradeManager.upgrade["Hotplate"] * 0.5f);
            Debug.Log($"heat speed = {heatSpeed}");
            if (tutorial.tutorialActive) tutorial.HotPlate();


            Debug.Log($"Water is Heating");
            draggable.transform.position = transform.position + new Vector3(0.2f, 1.25f, 0);
            teapot.waterHeating = true;

            if (steamAnimator != null)
            {
                steamAnimator.SetTrigger("Steam");
            }
            else Debug.Log("steam animator not assigned to hotplate");

            if (plateOnSprite != null) spriteRenderer.sprite = plateOnSprite;

            //set temperature goal
            switch (teaManager.customerOrder)
            {
                case TeaTypes.Black:
                    tempGoal = 8f;
                    break;
                case TeaTypes.White:
                    tempGoal = 7f;
                    break;
                case TeaTypes.Green:
                    tempGoal = 5f;
                    break;
            }

            //heating progress bar
            heatBar.SetActive(true);
            progressBar.SetBar(maxHeatTime, tempGoal);


            boilingSFX = AudioManager.instance.PlayLoopingSFX(boilingSound, transform, 1f);
        }
        else
        {
            Debug.Log($"Tried to Heat {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }

    }

    public void OnPickUp(Draggable draggable)
    {
        if (draggable.tag == "Teapot")
        {
            FinishedHeating();        
        }
    }

    private void FinishedHeating()
    {
        Debug.Log("FinishedHeating");
        teapot.waterHeating = false;

        if (tutorial.tutorialActive) tutorial.RemoveTeaPot();
        else Debug.Log("tutorial not active?");

        if (!teapot.waterHeated)
        {
            teapot.waterHeated = true;
            Debug.Log($"Heating has stopped");            
            finalTime = waterTimer;
            waterTimer = 0f;
        }

        Debug.Log($"Heat Final Time : {finalTime}");

        if (plateOffSprite != null) spriteRenderer.sprite = plateOffSprite;

        if (boilingSFX != null) AudioManager.instance.StopLoopingSFX(boilingSFX);
    
        //if (tutorial.tutorialActive) tutorial.DropTeaPot();

        if (steamAnimator != null)
        {
            steamAnimator.SetTrigger("EndSteam");
        }
        
        heatBar.SetActive(false); //hide heating progress bar

    }

    private void Update()
    {
        if (teapot.waterHeating && !teapot.waterHeated)
        {
            waterTimer += (Time.deltaTime * heatSpeed);
            progressBar.SetProgress(waterTimer); //updates heating progress bar
            // Debug.Log($"Heat Timer : {waterTimer}");

            if (tutorial.tutorialActive && waterTimer >= tempGoal)
            {
                teapot.waterHeating = false;
                tutorial.ShowTutorialStep();
            }
        }
        else if (teapot.waterHeating && teapot.waterHeated)
        {
            teapot.waterHeating = false;
            FinishedHeating();
        }

        if (teapot.waterHeated && boilingSFX != null) AudioManager.instance.StopLoopingSFX(boilingSFX);
    }
}
