using System.Collections;
using UnityEngine;

public class Teacup : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private GameObject tea;
    [SerializeField] private GameObject teapot;
    private Teapot teapot_script;

    private float teaMax = 0.4f; 
    private float fillLevel = 0f;
    private float fillSpeed = 0.002f;
    private Vector3 teaEmpty;
    private bool fillingCup = false;    
    // private bool draggingCup = false; //not currently in use, just uncomment it when you need it
    private bool teaFilled = false;
    
    void Start()
    {
        teaEmpty = tea.transform.position;
        teapot_script = teapot.GetComponent<Teapot>();
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
        Draggable drag = other.GetComponent<Draggable>(); //get a refence to the other objects Draggable script

        if (other.gameObject.CompareTag("Teapot") && drag.dragging && teapot_script.teaSteeped == true) //checks that the teapot is the object being dragged
        {
            fillingCup = true; 
            StartCoroutine(FillCup());
            Debug.Log("filling cup");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Teapot") && fillingCup == true) 
        {
            fillingCup = false;
            Debug.Log("stopped filling cup");
            teaFilled = true;
        }
    }

    private IEnumerator FillCup()
    {
        while (fillingCup)
        {
            if (fillLevel < 1)
            {
                fillLevel += fillSpeed;
                fillLevel = Mathf.Clamp01(fillLevel);

                Vector3 pos = tea.transform.position;
                float teaY = Mathf.Lerp(0, teaMax, fillLevel);
                tea.transform.position = new Vector3(pos.x, teaEmpty.y + teaY, pos.z); 

                yield return null;
            }
            else
            {
                fillingCup = false;
            }
        }
    }

    public void EmptyCup() //used when making a new tea or dump the current tea
    {
        tea.transform.position = teaEmpty;
        fillLevel = 0;
    }
    #endregion
}
