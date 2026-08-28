using UnityEngine;

public class Plant : Plot, IOnDropBaseCollision
{
    //private PlantManager plantManager;
    private enum Plants { TeaBush };
    [SerializeField] private Plants plantType;    
    private int growthStage = 0;
    private int decayStage = 0;
    private bool watered = false;
    //[SerializeField] bool finishedGrowing = false;
    [SerializeField] private bool ready = false;

    [SerializeField] private Sprite[] growthSprites;
    [SerializeField] private Sprite[] decaySprites;
    [SerializeField] private Sprite readySprite;
    private SpriteRenderer spriteRenderer;

    [SerializeField] ContainerManager containerManager;

    public Plot plot;

    private void Awake()
    {
        plantManager = FindAnyObjectByType(typeof(PlantManager)) as PlantManager;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager; 
        ClockManager.uniqueInstance.plants.Add(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
        
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
            //insert wateringcan animation
            draggable.transform.position = draggable.startPosition;
        }
    }

    public void LoadPlant()
    {
        if (plantManager != null)
        {
            switch (plotNumber)
            {
                case PlotNumber.Plot1:
                    growthStage = plantManager.plot1GrowthStage;
                    watered = plantManager.plot1Watered;
                    decayStage = plantManager.plot1DecayStage;
                    break;
                case PlotNumber.Plot2:
                    watered = plantManager.plot2Watered;
                    growthStage = plantManager.plot2GrowthStage;
                    decayStage = plantManager.plot2DecayStage;
                    break;
                case PlotNumber.Plot3:
                    watered = plantManager.plot3Watered;
                    growthStage = plantManager.plot3GrowthStage;
                    decayStage = plantManager.plot3DecayStage;
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
                }
                watered = false;
            }
        }
        else
        {
            decayStage +=1;
            
        }
        UpdatePlantManager();
    }

    private void UpdateSprite()
    {
        Debug.Log("updating sprite");
        if (watered)
        {
            spriteRenderer.color = new Color32(207, 207, 207, 255);
        }
        else //not watered
        {
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }

        if (ready && readySprite != null)
        {
            spriteRenderer.sprite = readySprite;   
        }
        else if (decayStage >= 3)
        {
            if (growthStage <= decaySprites.Length)
            {
                spriteRenderer.sprite = decaySprites[growthStage -1];
            }
            else Debug.Log("error: growth stage out of decay sprite range");
        }

        else if (growthSprites.Length != 0)
        {
            if (growthStage <= growthSprites.Length -1)
            {
                spriteRenderer.sprite = growthSprites[growthStage];
            }
            else Debug.Log("error: growth stage out of growth sprite range");
            
        }
        else
        {
            Debug.Log("error: Missing sprites");
        }
    }

    private void UpdatePlantManager()
    {

        switch(plotNumber)
        {
            case PlotNumber.Plot1:
                plantManager.plot1Watered = watered;
                plantManager.plot1GrowthStage = growthStage;
                plantManager.plot1DecayStage = decayStage;
                break;
            case PlotNumber.Plot2:
                plantManager.plot2Watered = watered;
                plantManager.plot2GrowthStage = growthStage;
                plantManager.plot2DecayStage = decayStage;
                break;
            case PlotNumber.Plot3:
                plantManager.plot3Watered = watered;
                plantManager.plot3GrowthStage = growthStage;
                plantManager.plot3DecayStage = decayStage;
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
            }
            Debug.Log("plant harvested");
            growthStage = growthSprites.Length -1;
            ready = false;
            UpdateSprite();
        }
        
        if (decayStage >= 3)
        {
            KillPlant();
            ClockManager.uniqueInstance.plants.Remove(this);
            Destroy(gameObject);  
        }
    }

    private void KillPlant()
    {
        Debug.Log("Killing plant");
        
        plot.planted = false;
        switch (plotNumber)
        {
            case PlotNumber.Plot1:
                plantManager.plot1Planted = false;
                plantManager.plot1GrowthStage = 0;
                plantManager.plot1DecayStage = 0;
                break;
            case PlotNumber.Plot2:
                plantManager.plot2Planted = false;
                plantManager.plot2GrowthStage = 0;
                plantManager.plot2DecayStage = 0;
                break;
            case PlotNumber.Plot3:
                plantManager.plot3Planted = false;
                plantManager.plot3GrowthStage = 0;
                plantManager.plot3DecayStage = 0;
                break;
        }
    }

}
