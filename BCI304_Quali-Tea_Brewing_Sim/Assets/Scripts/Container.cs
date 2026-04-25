using UnityEngine;

public class Container : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField] private GameObject storedItem;
    [SerializeField] private int maxStorage, currentStorage;
    
    private enum Containers {Tea, Sugar};
    [SerializeField] private Containers containerType;
    private string itemTag;
    
    [Header("Sprites")]
    [SerializeField] private Sprite[] containerSprites;
    private SpriteRenderer spriteRenderer;

    [Header("Audio")]
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private AudioSource myAudioSource;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (containerType) //check the container type and assign the item tag
        {
            case Containers.Tea:
                itemTag = "Tea";
                break;
            case Containers.Sugar:
                itemTag = "Addition";
                break;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnMouseDown()
    {  
        GameObject[] currentTeaLeaves = GameObject.FindGameObjectsWithTag(itemTag);

        if (currentStorage > 0 && currentTeaLeaves.Length < 1) //check the container isnt empty and havent already instantiated stored item
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = new Vector3(0f, 0f, 10f);
            GameObject newItem = Instantiate(storedItem, mousePos + offset, Quaternion.identity);
            Draggable draggable = newItem.GetComponent<Draggable>();
            draggable.DragObject();
            currentStorage -= 1;
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
            Debug.Log($"{gameObject.name} is empty!");
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
