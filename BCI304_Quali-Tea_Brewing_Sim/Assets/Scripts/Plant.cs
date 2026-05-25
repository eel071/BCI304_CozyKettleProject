using UnityEngine;

public class Plant : MonoBehaviour, IOnDropBaseCollision
{
    // option 1: similar to teacup pour.
    // option 2: just a drop, sets plant to watered and plays a little watering animation

    [SerializeField] PlantManager plantManager;
    private enum Plants { TeaBush, LemonTree };
    [SerializeField] private Plants plantType;

    [SerializeField] int growthStage = 0;
    [SerializeField] int decayStage = 0;
    [SerializeField] bool watered = false;
    //[SerializeField] bool finishedGrowing = false;
    [SerializeField] private bool ready = false;

    [SerializeField] private Sprite[] growthSprites;
    [SerializeField] private Sprite readySprite;
    private SpriteRenderer spriteRenderer;

    [SerializeField] ContainerManager containerManager;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager; 
        ClockManager.uniqueInstance.plants.Add(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /* bool wateringBushes = false;
     private void OnTriggerEnter2D(Collider2D other)
     {

         Draggable drag = other.GetComponent<Draggable>(); //get a refence to the other objects Draggable script

         if (other.gameObject.CompareTag("WateringCan") && drag.dragging) //checks that the watering can is the object being dragged
         {
             wateringBushes = true;
             Debug.Log("watering bush");           
         }
     }*/
    private void Start()
    {
        LoadPlant();
    }


    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "WateringCan")
        {
            Debug.Log($"Watered {gameObject.name}");
            watered = true;
            UpdateSprite();
            UpdatePlantManager();
            //animation would go here
            draggable.transform.position = draggable.startPosition;
        }
    }

    private void LoadPlant()
    {
        if (plantManager != null)
        {
            switch(plantType)
            {
                case Plants.TeaBush:
                    growthStage = plantManager.teaGrowthStage;
                    watered = plantManager.teaWatered;
                    //finishedGrowing = plantManager.teaFinishedGrowing;
                    decayStage = plantManager.teaDecayStage;
                    break;
                case Plants.LemonTree:
                    growthStage = plantManager.lemonGrowthStage;
                    watered = plantManager.lemonWatered;
                    //finishedGrowing = plantManager.lemonFinishedGrowing;
                    decayStage = plantManager.lemonDecayStage;
                    break;
            }
            if (growthStage >= growthSprites.Length)
            {
                ready = true;
            }
            UpdateSprite();
        }
        else
        {
            Debug.Log("Cannot find plant manager");
        }
    }

    public void UpdateGrowth()
    {
        if (watered)
        {
            decayStage = 0;
            if (growthStage <= growthSprites.Length)
            {
                growthStage++;
                if (growthStage >= growthSprites.Length)
                {
                    ready = true;
                    //finishedGrowing = true;
                }
                watered = false;
            }
        }
        else
        {
            decayStage +=1;

            if (decayStage >= 3)
            {
                Destroy(gameObject);
            }

            /*
            if (growthStage > 0)
            {
                growthStage--; //rather than subtracting from growth stage should do some kind of decay stage instead?
            }
            else
            {
                Destroy(gameObject);
            }
            */
        }
        UpdatePlantManager();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        Debug.Log("updating sprite");
        if (watered)
        {
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
        else //not watered
        {
            spriteRenderer.color = new Color32(207, 207, 207, 255);
        }

        if (ready && readySprite != null)
        {
            spriteRenderer.sprite = readySprite;   
        }
        else if (growthSprites.Length != 0)
        {
            if (growthStage <= growthSprites.Length -1)
            {
                spriteRenderer.sprite = growthSprites[growthStage];
            }
            else
            {
                Debug.Log("error: growth stage out of range");
            }
        }
        else
        {
            Debug.Log("error: Missing sprites");
        }
    }

    private void UpdatePlantManager()
    {
        switch(plantType)
        {
            case Plants.TeaBush:
                plantManager.teaWatered = watered;
                plantManager.teaGrowthStage = growthStage;
                //plantManager.teaFinishedGrowing = finishedGrowing;
                plantManager.teaDecayStage = decayStage;
                break;
            case Plants.LemonTree:
                plantManager.lemonWatered = watered;
                plantManager.lemonGrowthStage = growthStage;
                //plantManager.lemonFinishedGrowing = finishedGrowing;
                plantManager.lemonDecayStage = decayStage;
                break;
        }
    }

    private void OnMouseDown() 
    {  
        if (ready) //harvest the plant
        {
            switch(plantType)
            { 
                case Plants.TeaBush:
                    containerManager.AddLeaves();
                    break;
                case Plants.LemonTree:
                    //add lemons to the container manager.
                    break;
            }
            Debug.Log("plant harvested");
            growthStage = growthSprites.Length -1;
            ready = false;
            UpdateSprite();
        }
    }
}
