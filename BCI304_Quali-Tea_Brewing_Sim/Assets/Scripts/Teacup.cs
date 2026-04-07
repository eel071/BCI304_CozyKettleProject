using UnityEngine;

public class Teacup : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private GameObject tea;
    private float teaMax = 0.25f; 
    private float fillLevel = 0f;
    private Vector3 teaFill;
    private Vector3 teaEmpty;
    private bool filled = false;

    void Start()
    {
        teaEmpty = tea.transform.position;
    }

    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Teapot")
        {
            Debug.Log($"Tea Filled");
            if (!filled)
            {
                FillCup();
            }
            draggable.transform.position = draggable.startPosition;
        }        
        else if(draggable.tag == "Addition")
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

    private void FillCup()
    { 
        filled = true;
        //will have to change this later so it can have varying heights
        //Mathf.Lerp(0, teaMax, fillLevel) 
        tea.transform.position += new Vector3(0f, teaMax, 0f); 
    }

    private void EmptyCup()
    {
        tea.transform.position = teaEmpty;
        filled = false;
    }
}
