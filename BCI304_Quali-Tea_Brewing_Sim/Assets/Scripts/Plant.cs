using UnityEngine;

public class Plant : MonoBehaviour, IOnDropBaseCollision
{
    // option 1: similar to teacup pour.
    // option 2: just a drop, sets plant to watered and plays a little watering animation

    [SerializeField] PlantManager plantManager;
    private enum Plants { TeaBush, LemonTree };
    [SerializeField] private Plants plantType;

    [SerializeField] int growthStage = 1;
    [SerializeField] bool watered = false;
    [SerializeField] bool finishedGrowing = false;

    [SerializeField] private Sprite[] growthSprites;
    [SerializeField] private Sprite ready;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;        
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
        if (plantManager != null)
        {
            switch(plantType)
            {
                case Plants.TeaBush:
                    growthStage = plantManager.teaGrowthStage;
                    watered = plantManager.teaWatered;
                    finishedGrowing = plantManager.teaFinishedGrowing;
                    break;
                case Plants.LemonTree:
                    growthStage = plantManager.lemonGrowthStage;
                    watered = plantManager.lemonWatered;
                    finishedGrowing = plantManager.lemonFinishedGrowing;
                    break;
            }
            UpdateSprite();
        }
        else
        {
            Debug.Log("Cannot find plant manager");
        }
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

    public void UpdateGrowth()
    {
        if (watered || finishedGrowing)
        {
            if (growthStage < 2)
            {
                growthStage++;
                if (growthStage == 2)
                {
                    finishedGrowing = true;
                }
                watered = false;
            }

        }
        else
        {
            if (growthStage > 0)
            {
                growthStage--; //rather than subtracting from growth stage should do some kind of decay stage instead?
            }
            else
            {
                Destroy(gameObject);
            }
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

        if (growthSprites.Length != 0 && ready != null)
        {
            if (growthStage <= growthSprites.Length)
            {
                spriteRenderer.sprite = growthSprites[growthStage];
            }
            else if (growthStage == growthSprites.Length +1)
            {
                spriteRenderer.sprite = ready;
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
                plantManager.teaFinishedGrowing = finishedGrowing;
                break;
            case Plants.LemonTree:
                plantManager.lemonWatered = watered;
                plantManager.lemonGrowthStage = growthStage;
                plantManager.lemonFinishedGrowing = finishedGrowing;
                break;
        }
    }

}
