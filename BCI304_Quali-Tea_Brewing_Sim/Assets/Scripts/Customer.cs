using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

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

    void Awake()
    {
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        dialogue = FindAnyObjectByType(typeof(Dialogue)) as Dialogue;
        col = GetComponent<BoxCollider2D>();

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
        dialogue.OrderDialogue(); //create order dialogue
        StartCoroutine(WaitBeforeLoad());
    }

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup")
        {
            Destroy(draggable.transform.gameObject);
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
