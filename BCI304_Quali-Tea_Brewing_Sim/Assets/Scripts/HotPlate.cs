using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision
{
    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teapot")
        {
            Debug.Log($"Water is Heating");
            draggable.transform.position = transform.position + new Vector3(0, 1, 0);
        }        
        else
        {
            Debug.Log($"Tried to Heat {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }                
    }   
    
}
