using UnityEngine;
using TMPro;

public class Container : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField] private GameObject storedItem;
    [SerializeField] private int maxStorage, currentStorage;

    [SerializeField] private TextMeshProUGUI counterText;
    
    private enum Containers {GreenTea, BlackTea, WhiteTea, Lemon, Sugar, Honey, Milk, Seeds};
    [SerializeField] private Containers containerType;
    
    [Header("Sprites")]
    [SerializeField] private Sprite[] containerSprites;
    private SpriteRenderer spriteRenderer;

    [Header("Audio")]
    [SerializeField] private AudioClip wooshSound;

    [SerializeField] ContainerManager containerManager;
    [SerializeField] private UIManager uiManager;

    private Collider2D col;

    public bool itemSpawned = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
        uiManager = FindAnyObjectByType(typeof(UIManager)) as UIManager;
        col = GetComponent<Collider2D>();
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
        
        UpdateContainer();
    }

    private void OnMouseDown()
    {  
        if (currentStorage > 0 && !itemSpawned) //check the container isnt empty and hasn't already instantiated item
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
            UpdateContainer();
            
            
            AudioManager.instance.PlaySFXClip(wooshSound, transform, 1f);
            
        }
        else
        {
            Debug.Log($"cannot take {storedItem.name}");
        }
    }

    private void UpdateContainer()
    {
        containerManager.containerCount[containerType.ToString()] = currentStorage;
        
        //update counter
        counterText.text = $"{containerManager.containerCount[containerType.ToString()]}"; 

        //update sprite
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
        UpdateContainer();

    }

    public void SwitchInteractable(bool interactable)
    {
        if (col != null) col.enabled = interactable;  
        else Debug.Log($"no collider assigned on {gameObject}");
    }

    public int GetCurrentStorage()
    {
        return currentStorage;
    }
}
