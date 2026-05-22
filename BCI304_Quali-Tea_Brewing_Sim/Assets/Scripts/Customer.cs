using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class Customer : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private TeaManager teaManager;
    [SerializeField] private LoadManager loadManager;  
    [SerializeField] private CustomerSpawner customerSpawner;
    private Dialogue dialogue;
    private BoxCollider2D col;

    void Awake()
    {
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        dialogue = FindAnyObjectByType(typeof(Dialogue)) as Dialogue;
        teaManager.SetCustomerOrder();       
        col = GetComponent<BoxCollider2D>();
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
            Destroy(draggable.transform.parent.gameObject);
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
