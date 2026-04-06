using UnityEngine;

public class Teacup : MonoBehaviour, IOnDropBaseCollision
{
    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Teapot")
        {
            Debug.Log($"Tea Filled");
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
    
}
