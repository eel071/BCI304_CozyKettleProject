using System.Collections;
using UnityEngine;

public class Teacup : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private TeaManager teaManager;
    
    [SerializeField] private GameObject tea;
    [SerializeField] private Teapot teapotScript;

    private float teaMax = 0.4f; 
    public float fillLevel = 0f;
    [SerializeField] private float fillSpeed = 0.2f;
    private Vector3 teaEmpty;
    private bool fillingCup = false;    
    // private bool draggingCup = false; //not currently in use, just uncomment it when you need it
    public bool teaFilled = false;

    [SerializeField] private AudioClip teaPouring;
    [SerializeField] private AudioSource myAudioSource;


    void Start()
    {
        teaEmpty = tea.transform.position;        
    }

    public void OnDrop(Draggable draggable)
    {            
        if(draggable.tag == "Addition" && teaFilled == true)
        {
            Debug.Log($"Added {draggable.gameObject.name} to teacup");
            Destroy(draggable.gameObject);
        }
        else
        {
            Debug.Log($"Tried to Add {draggable.tag} to teacup");
            draggable.transform.position = draggable.startPosition;
        }
    }
    
    #region filling cup

    private void OnTriggerEnter2D(Collider2D other)
    {
        teaManager.UpdateTea();

        Draggable drag = other.GetComponent<Draggable>(); //get a refence to the other objects Draggable script
    
        if (other.gameObject.CompareTag("Teapot") && drag.dragging && teapotScript.teaSteeped == true && teaFilled == false) //checks that the teapot is the object being dragged
        {
            fillingCup = true; 
            StartCoroutine(FillCup());
            Debug.Log("filling cup");

            myAudioSource.clip = teaPouring;
            myAudioSource.loop = true;
            myAudioSource.Play();
            teapotScript.PourTea();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Teapot") && fillingCup == true) 
        {
            fillingCup = false;
            Debug.Log("stopped filling cup");
            teaFilled = true;

            if (myAudioSource != null)
            {
                myAudioSource.Stop(); // Stop sound when 100% full
            }
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

                if (myAudioSource != null)
                {
                    myAudioSource.Stop(); // Stop sound when 100% full
                }
                Debug.Log("stopped filling cup - FULL");
            }
        }
    }

    public void EmptyCup() //used when making a new tea or dump the current tea
    {
        tea.transform.position = teaEmpty;
        fillLevel = 0;
        teaFilled = false;
    }
    #endregion
}
