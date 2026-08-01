using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class Customer : MonoBehaviour, IOnDropBaseCollision
{
    private TeaManager teaManager;
    private LoadManager loadManager;  
    private CustomerSpawner customerSpawner;
    private Dialogue dialogue;
    private BoxCollider2D col;

    //Customer Order
    [SerializeField] private bool randomiseOrder;
    [SerializeField] private TeaTypes teaOrder;
    [SerializeField] private bool lemonOrder;
    [SerializeField] private int sugarOrder;

    //Customer Sprites
    [SerializeField] private Sprite customerNeutralSprite;
    [SerializeField] private Sprite customerTalkingSprite;
    [SerializeField] private Sprite customerHappySprite;
    [SerializeField] private Sprite customerUpsetSprite;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        dialogue = FindAnyObjectByType(typeof(Dialogue)) as Dialogue;
        col = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (randomiseOrder)
        {
            teaManager.RandomiseCustomerOrder();
        }

        else
        {
            teaManager.SetCustomerOrder(teaOrder, lemonOrder, sugarOrder);
        }
        
    }    

    void OnMouseUp()
    {
        if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
        dialogue.OrderDialogue(); //create order dialogue        
        StartCoroutine(WaitBeforeLoad());        
    }

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup")
        {
            Destroy(draggable.transform.gameObject);

            if (teaManager.customerOrder != teaManager.tea)
            {
                if (customerUpsetSprite != null) spriteRenderer.sprite = customerUpsetSprite;
            }
            else
            {
                switch (teaManager.finalScore)
                {
                    case >= 75:
                        if (customerHappySprite != null) spriteRenderer.sprite = customerHappySprite;
                        break;
                    case >= 25:
                        if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
                        break;
                    case < 25:
                        if (customerUpsetSprite != null) spriteRenderer.sprite = customerUpsetSprite;
                        break;
                }

            }            

            dialogue.ScoreDialogue(); //create score dialogue (customer's response to the tea)
            teaManager.ResetTea();   
            col.enabled = false; //stop the player from clicking the customer again
            StartCoroutine(WaitBeforeDestroy());
        }
    }

    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(2);
        dialogue.HideDialogue();
        if (customerNeutralSprite != null) spriteRenderer.sprite = customerNeutralSprite;
        loadManager.LoadTeaStation();
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(2);
        dialogue.HideDialogue();
        customerSpawner.isCustomer = false;
        Destroy(gameObject);
    }
}
