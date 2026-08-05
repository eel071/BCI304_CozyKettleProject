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

    [Header("Customer Order")]
    [SerializeField] private bool randomiseOrder;
    [SerializeField] private TeaTypes teaOrder;
    [SerializeField] private bool lemonOrder;
    [SerializeField] private int sugarOrder;

    [Header("Dialogue")]
    [SerializeField] private string orderD;
    [SerializeField] private string wrongTeaD;
    [SerializeField] private string perfectTeaD;
    [SerializeField] private string goodTeaD;
    [SerializeField] private string fineTeaD;
    [SerializeField] private string badTeaD;
    [SerializeField] private string terribleTeaD;
    [SerializeField] private string outOfTeaD;


    [Header("Sprites")]
    [SerializeField] private Sprite customerNeutralSprite;
    [SerializeField] private Sprite customerTalkingSprite;
    [SerializeField] private Sprite customerHappySprite;
    [SerializeField] private Sprite customerUpsetSprite;
    private SpriteRenderer spriteRenderer;


    private bool hasOrdered = false;
    private bool destroyAfterTalk = false;

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
        if (!hasOrdered)
        {
            if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
            if (orderD != "") dialogue.OrderDialogue(orderD); //set custom order dialogue
            else dialogue.GenerateOrderDialogue(); //generate default order dialogue
            hasOrdered = true;      
        }
    }

    public void StopTalking()
    {
        if (customerNeutralSprite != null) spriteRenderer.sprite = customerNeutralSprite;

        if (destroyAfterTalk) StartCoroutine(WaitBeforeDestroy());
        else StartCoroutine(WaitBeforeLoad());
    }

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup")
        {
            Destroy(draggable.transform.gameObject);

            if (teaManager.customerOrder != teaManager.tea)
            {
                if (customerUpsetSprite != null) spriteRenderer.sprite = customerUpsetSprite; //set sprite
                dialogue.ScoreDialogue(wrongTeaD);
            }
            else
            {
                switch (teaManager.finalScore)
                {
                    case >= 90:
                        if (customerHappySprite != null) spriteRenderer.sprite = customerHappySprite;
                        dialogue.ScoreDialogue(perfectTeaD);
                        break;
                    case >= 75:
                        if (customerHappySprite != null) spriteRenderer.sprite = customerHappySprite;
                        dialogue.ScoreDialogue(goodTeaD);
                        break;
                    case >= 50:
                        if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
                        dialogue.ScoreDialogue(fineTeaD);
                        break;
                    case >= 25:
                        if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
                        dialogue.ScoreDialogue(badTeaD);
                        break;
                    case < 25:
                        if (customerUpsetSprite != null) spriteRenderer.sprite = customerUpsetSprite;
                        dialogue.ScoreDialogue(terribleTeaD);
                        break;
                }

            }            
            
            teaManager.ResetTea();   
            col.enabled = false; //stop the player from clicking the customer again

            destroyAfterTalk = true;
        }
    }

    
    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(1f);
        dialogue.HideDialogue();
        //if (customerNeutralSprite != null) spriteRenderer.sprite = customerNeutralSprite;
        loadManager.LoadTeaStation();
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(1);
        dialogue.HideDialogue();
        customerSpawner.isCustomer = false;
        Destroy(gameObject);
    }
}
