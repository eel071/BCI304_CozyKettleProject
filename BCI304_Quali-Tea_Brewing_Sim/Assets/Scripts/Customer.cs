using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class Customer : MonoBehaviour, IOnDropBaseCollision
{
    private TeaManager teaManager;
    private BankManager bankManager;
    private TipJar tipJar;
    [SerializeField] Teacup teacup;
    [SerializeField] Teapot teapot;
    
    private CustomerSpawner customerSpawner;
    private Dialogue dialogue;
    private BoxCollider2D col;

    [Header("Customer Order")]
    [SerializeField] private bool randomiseOrder;
    [SerializeField] private TeaTypes teaOrder;
    [SerializeField] private bool lemonOrder;
    [SerializeField] private int sugarOrder;
    [SerializeField] private bool honeyOrder;
    [SerializeField] private bool milkOrder;

    [Header("Dialogue")]
    public string orderD;
    [SerializeField] private string wrongTeaD;
    [SerializeField] private string perfectTeaD;
    [SerializeField] private string goodTeaD;
    [SerializeField] private string fineTeaD;
    [SerializeField] private string badTeaD;
    [SerializeField] private string terribleTeaD;
    public string rejectD;

    [Header("Sprites")]
    [SerializeField] private Sprite customerNeutralSprite;
    [SerializeField] private Sprite customerTalkingSprite;
    [SerializeField] private Sprite customerHappySprite;
    [SerializeField] private Sprite customerUpsetSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Audio")]
    [SerializeField] private AudioClip orderSound;
    [SerializeField] private AudioClip amazingSound; // 90-100
    [SerializeField] private AudioClip goodSound;     // 75-89
    [SerializeField] private AudioClip disappointedSound; // 25-74
    public AudioClip angrySound;   // 0-24

    private bool hasOrdered = false;
    public bool destroyAfterTalk = false;
    
    void Awake()
    {
        //get references
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
        tipJar = FindAnyObjectByType(typeof(TipJar)) as TipJar;
        dialogue = FindAnyObjectByType(typeof(Dialogue)) as Dialogue;
        teacup = FindAnyObjectByType(typeof(Teacup)) as Teacup;
        teapot = FindAnyObjectByType(typeof(Teapot)) as Teapot;
        col = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //set customer order in tea manager
        if (randomiseOrder) teaManager.RandomiseCustomerOrder();
        else teaManager.SetCustomerOrder(teaOrder, lemonOrder, sugarOrder, honeyOrder, milkOrder);

    }
    
    //taking customer order
    void OnMouseUp()
    {
        if (!hasOrdered) //if the player hasn't already clicked on the customer
        {
            CustomerTalk();
            OrderDialogue(); //set or generate order dialogue
            hasOrdered = true;
        }
    }

    public void CustomerTalk()
    {
        if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
    }

    //giving tea to customer
    public void OnDrop(Draggable draggable)  
    {
        if (draggable.tag == "Teacup") 
        {
            //Destroy(draggable.transform.gameObject); //destroy the teacup object
            ReactionDialogue();
            teacup.EmptyCup();
            teapot.ResetTeapot();                        
        }
    }

    #region dialogue generation
    private void OrderDialogue()
    {
        string customerOrder;
        
        if (orderD != "") customerOrder = orderD; //if the customer has custom dialogue then use this dialogue
        else //generate default customer order dialogue
        {       
            //sugar and lemon 
            if (teaManager.sugarCubesOrder > 0 && teaManager.lemonOrder) customerOrder = $"{teaManager.teaOrder} with {teaManager.sugarCubesOrder} sugar and lemon.";
            //just sugar
            else if (teaManager.sugarCubesOrder > 0) customerOrder = $"{teaManager.teaOrder} with {teaManager.sugarCubesOrder} sugar.";
            //just lemom
            else if (teaManager.lemonOrder) customerOrder = $"{teaManager.teaOrder} with lemon.";
            //no sugar or lemon
            else customerOrder = $"{teaManager.teaOrder}";
        }
        dialogue.SetCustomerText(customerOrder, orderSound, true);
    }

    private void ReactionDialogue()
    {
        float tips = 0f;
        string reactDialogue = "";
        AudioClip reactionSound = null;

        if (teaManager.customerOrder != teaManager.tea) //if the tea doesnt match the order
        {
            if (customerUpsetSprite != null) spriteRenderer.sprite = customerUpsetSprite;
            if (wrongTeaD != "") reactDialogue = wrongTeaD; else reactDialogue = "This isn't what I ordered!";
            reactionSound = angrySound;
            tips = 0f;
        }

        else //set dialogue, sprite, and audio clip depending on final score
        {
            switch (teaManager.finalScore)
            {
                case >= 90:
                    if (customerHappySprite != null) spriteRenderer.sprite = customerHappySprite;
                    if (perfectTeaD != "") reactDialogue = perfectTeaD; else reactDialogue = "This is Perfect!";
                    reactionSound = amazingSound;
                    tips = 10f;
                    break;
                case >= 75:
                    if (customerHappySprite != null) spriteRenderer.sprite = customerHappySprite;
                    if (goodTeaD != "") reactDialogue = goodTeaD; else reactDialogue = "Yum!";
                    reactionSound = goodSound;
                    tips = 5f;
                    break;
                case >= 50:
                    if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
                    if (fineTeaD != "") reactDialogue = fineTeaD; else reactDialogue = "This is okay";
                    reactionSound = disappointedSound;
                    tips = 2.5f;
                    break;
                case >= 25:
                    if (customerTalkingSprite != null) spriteRenderer.sprite = customerTalkingSprite;
                    if (badTeaD != "") reactDialogue = badTeaD; else reactDialogue = "I've had better tea.";
                    reactionSound = disappointedSound;
                    tips = 1f;
                    break;
                case < 25:
                    if (customerUpsetSprite != null) spriteRenderer.sprite = customerUpsetSprite;
                    if (terribleTeaD != "") reactDialogue = terribleTeaD; else reactDialogue = "Can you even call this tea?";
                    reactionSound = angrySound;
                    tips = 0f;
                    break;
            }
        }    
    
        dialogue.SetCustomerText(reactDialogue, reactionSound, false);

        bankManager.AddMoney(tips);
        tipJar.AddTips(tips);
        teaManager.ResetTea();
        col.enabled = false; //stop the player from clicking the customer again
        destroyAfterTalk = true;
    }
    #endregion

    public void StopTalking()
    {
        if (customerNeutralSprite != null) spriteRenderer.sprite = customerNeutralSprite; //set sprite
        if (destroyAfterTalk) StartCoroutine(WaitBeforeDestroy()); //destroy customer if destroyAfterTalk = true
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(1);
        dialogue.HideDialogue();
        customerSpawner.isCustomer = false;
        Destroy(gameObject);
    }
}