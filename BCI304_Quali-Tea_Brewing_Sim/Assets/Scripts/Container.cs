using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private GameObject storedItem;
    [SerializeField] private int maxStorage, currentStorage;
    [SerializeField] private Sprite[] containerSprites;
    private SpriteRenderer spriteRenderer;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnMouseDown()
    {  
        if (currentStorage > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = new Vector3(0f, 0f, 10f);
            Instantiate(storedItem, mousePos + offset, Quaternion.identity);
            currentStorage -= 1;
            UpdateSprite();
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
