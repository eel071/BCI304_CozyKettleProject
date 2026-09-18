using UnityEngine;

public class Container : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField] private GameObject storedItem;
    [SerializeField] private int maxStorage, currentStorage;
    
    private enum Containers {GreenTea, BlackTea, WhiteTea, Lemon, Sugar, Honey, Milk, Seeds};
    [SerializeField] private Containers containerType;
    
    [Header("Sprites")]
    [SerializeField] private Sprite[] containerSprites;
    private SpriteRenderer spriteRenderer;

    [Header("Audio")]
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private AudioSource myAudioSource;

    [SerializeField] ContainerManager containerManager;
    [SerializeField] private UIManager uiManager;

    public bool itemSpawned = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
        uiManager = FindAnyObjectByType(typeof(UIManager)) as UIManager;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateStorage();
    }

    public void UpdateStorage()
    {
        if (containerManager != null)
        {
            if (containerManager.containerCount.TryGetValue(containerType.ToString(), out int count))
            {
                currentStorage = count;
            }
            else Debug.Log($"{containerType} container not found in containerCount dictionary");

            if (containerManager.containerMax.TryGetValue(containerType.ToString(), out int storage))
            {
                maxStorage = storage;
            }
            else Debug.Log($"{containerType} container not found in containerMax dictionary");
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
        containerManager.containerCount[containerType.ToString()] = currentStorage;
        if (containerType == Containers.Seeds) uiManager.UpdateSeedCounter(containerManager.containerCount["Seeds"]);
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

    public void ReturnItem()
    {
        currentStorage += 1;
        UpdateContainerManager();
        UpdateSprite();
    }
}
