using UnityEngine;

public class Container : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField] private GameObject storedItem;
    [SerializeField] private int maxStorage, currentStorage;
    
    private enum Containers {GreenTea, BlackTea, WhiteTea, Lemon, Sugar};
    [SerializeField] private Containers containerType;
    
    [Header("Sprites")]
    [SerializeField] private Sprite[] containerSprites;
    private SpriteRenderer spriteRenderer;

    [Header("Audio")]
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private AudioSource myAudioSource;

    [SerializeField] ContainerManager containerManager;

    public bool itemSpawned = false;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (containerManager != null)
        {
            switch (containerType) //check the container type and assign the storage count
            {
                case Containers.GreenTea:
                    currentStorage = containerManager.greenTeaCount;
                    maxStorage = containerManager.teaMax;
                    break;
                case Containers.BlackTea:
                    currentStorage = containerManager.blackTeaCount;
                    maxStorage = containerManager.teaMax;
                    break;
                case Containers.WhiteTea:
                    currentStorage = containerManager.whiteTeaCount;
                    maxStorage = containerManager.teaMax;
                    break;
                case Containers.Lemon:
                    currentStorage = containerManager.lemonCount;
                    maxStorage = containerManager.lemonMax;
                    break;
                case Containers.Sugar:
                    currentStorage = containerManager.sugarCount;
                    maxStorage = containerManager.sugarMax;
                    break;
            }
        }
        else
        {
            Debug.Log("Cannot find container manager");
        }

        UpdateSprite();
    }

    private void OnMouseDown()
    {  
        if (currentStorage > 0 && !itemSpawned) //check the container isnt empty and havent already instantiated item type
        {
            itemSpawned = true;

            //get spawn position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Vector3 offset = new Vector3(0f, 0f, 10f); 

            //spawn stored item at spawn position
            GameObject newItem = Instantiate(storedItem, mousePos + offset, Quaternion.identity);
            
            //drag the item
            Draggable draggable = newItem.GetComponent<Draggable>();
            draggable.DragObject();

            ContainerItem containerItem = newItem.GetComponent<ContainerItem>();
            containerItem.container = this;
            
            //update storage and container sprite
            currentStorage -= 1;
            UpdateContainerManager();
            UpdateSprite();
            
            if (wooshSound != null && myAudioSource != null)
            {
                // PlayOneShot is great for clicks because it doesn't interrupt 
                // itself if the player clicks really fast!
                myAudioSource.PlayOneShot(wooshSound);
            }
        }
        else
        {
            Debug.Log($"cannot take {storedItem.name}");
        }
    }

    private void UpdateContainerManager()
    {
        switch (containerType) //check the container type and assign the item tag
        {
            case Containers.GreenTea:
                containerManager.greenTeaCount -= 1;
                break;
            case Containers.BlackTea:
                containerManager.blackTeaCount -= 1;
                break;
            case Containers.WhiteTea:
                containerManager.whiteTeaCount -=1;
                break;
            case Containers.Lemon:
                containerManager.lemonCount -= 1;
                break;
            case Containers.Sugar:
                containerManager.sugarCount -= 1;
                break;
        }
        
    }

    private void UpdateSprite()
    {
        if (containerSprites.Length > 0 && maxStorage > 0)
        {
            float currentPhase = (float)currentStorage / (float)maxStorage;
            int sprite = Mathf.FloorToInt((1-currentPhase) * (containerSprites.Length-1));
            spriteRenderer.sprite = containerSprites[sprite];
        }
    }
}
