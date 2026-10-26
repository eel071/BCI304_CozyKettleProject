using System.Collections;
using UnityEngine;

public class Teacup : MonoBehaviour, IOnDropBaseCollision
{
    public TeaTypes cupTea;

    [SerializeField] private TeaManager teaManager;
    private Tutorial tutorial;
    [SerializeField] private GameObject tea;
    [SerializeField] private GameObject saucer;
    [SerializeField] private Teapot teapotScript;
    private Draggable thisDraggable;

    public bool canPour;

    private float teaMax = 0.4f; 
    public float fillLevel = 0f;
    [SerializeField] private float fillSpeed = 0.2f;
    private Vector3 teaEmpty;
    private bool fillingCup = false;    
    // private bool draggingCup = false; //not currently in use, just uncomment it when you need it
    public bool teaFilled = false;
    private bool teaFull = false;

    [SerializeField] private AudioClip teaPouring;
    [SerializeField] private AudioClip additionSFX;
    private AudioSource pouringSFX;
    

    void Start()
    {        
        teaEmpty = tea.transform.position;     
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        thisDraggable = GetComponent<Draggable>();
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
    }

    public void OnDrop(Draggable draggable)
    {            
        ContainerItem containerItem = draggable.GetComponent<ContainerItem>();

        if (containerItem != null)
        {
            if(draggable.tag == "Addition" && teaFilled == true)
            {
                Debug.Log($"Added {draggable.gameObject.name} to teacup");
                
                Destroy(draggable.gameObject);
                //making it milky         
                AudioManager.instance.PlaySFXClip(additionSFX, transform, 1f, 0.15f);

                teaManager.Additions(containerItem.item);

                if (containerItem.item == ContainerItems.Milk && cupTea != TeaTypes.Milk)
                {
                    teaManager.tColor = teaManager.tColor * 2;
                    teaManager.UpdateTea();
                    AudioManager.instance.PlaySFXClip(teaPouring, transform, 1f, 0f);
                }

                if (tutorial.tutorialActive) teaManager.CheckTea();
            }
            else if (containerItem.item == ContainerItems.Milk && teaFilled == false)
            {
                Destroy(draggable.gameObject);
                cupTea = TeaTypes.Milk;
                teaManager.SetTea();
                teaManager.UpdateTea();
                teaFilled = true;
                Vector3 pos = tea.transform.position;
                float teaY = Mathf.Lerp(0, teaMax, 0.75f);

                tea.transform.position = new Vector3(pos.x, pos.y + teaY , pos.z); 

            }
            else
            {
                Debug.Log($"Tried to Add {draggable.tag} to teacup");
                draggable.ReturnItem();
            }
        }
        else
        {
            Debug.Log($"Tried to Add {draggable.tag} to teacup");
            draggable.ReturnItem();
        }

        
    }
    
    #region filling cup

    private void OnTriggerEnter2D(Collider2D other)
    {
        Draggable drag = other.GetComponent<Draggable>(); //get a refence to the other objects Draggable script
    
        if (other.gameObject.CompareTag("Teapot") && teapotScript.waterHeated == true && drag.dragging && teaFull == false && canPour) //checks that the teapot is the object being dragged
        {
            if (teaFilled && cupTea != teapotScript.teapotTea)
            {
                Debug.Log("tea in teapot does not match tea in cup");
            }
            else
            {
                cupTea = teapotScript.teapotTea;
                teaManager.SetTea();
                teaManager.UpdateTea();
                fillingCup = true; 
                StartCoroutine(FillCup());
                Debug.Log("filling cup");

                pouringSFX = AudioManager.instance.PlayLoopingSFX(teaPouring, transform, 1f);
                teapotScript.PourTea();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Teapot") && fillingCup == true) 
        {
            fillingCup = false;
            Debug.Log("stopped filling cup");
            teaFilled = true;

            if (pouringSFX != null) AudioManager.instance.StopLoopingSFX(pouringSFX);
            teapotScript.StopPouring();
        }
    }

    private IEnumerator FillCup()
    {
        while (fillingCup)
        {
            if (fillLevel < 1)
            {
                fillLevel += fillSpeed * Time.deltaTime;
                fillLevel = Mathf.Clamp01(fillLevel);

                Vector3 pos = tea.transform.position;
                float teaY = Mathf.Lerp(0, teaMax, fillLevel);
                tea.transform.position = new Vector3(pos.x, teaEmpty.y + teaY, pos.z); 

                yield return null;
            }
            else
            {
                fillingCup = false;
                teaFilled = true;
                teaFull = true;

                if (pouringSFX != null) AudioManager.instance.StopLoopingSFX(pouringSFX);
                Debug.Log("stopped filling cup - FULL");
                teapotScript.StopPouring();
            }
        }
    }

    public void EmptyCup() //used when making a new tea or dump the current tea
    {
        transform.position = new Vector3(38f, -2f, 0);
        NewStartPosition();
        toggleSaucer(false);
        tea.transform.position = teaEmpty;        
        fillLevel = 0;
        teaFilled = false;
    }
    #endregion

    public void NewStartPosition()
    {
        thisDraggable.startPosition = transform.position;
    }

    public void toggleSaucer(bool active)
    {
        saucer.SetActive(active);
    }

    public void toggleDraggable(bool canDrag)
    {
        thisDraggable.SwitchDraggable(canDrag);
    }



}
