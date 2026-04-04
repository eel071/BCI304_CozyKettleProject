using UnityEngine;

public class TeaPot : MonoBehaviour, IOnDropBaseCollision
{
    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Tea")
        {
            Debug.Log($"Added {draggable.gameObject.name}");
            Destroy(draggable.gameObject);
        }        
        else
        {
            Debug.Log($"Tried to Add {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }
    }

}
