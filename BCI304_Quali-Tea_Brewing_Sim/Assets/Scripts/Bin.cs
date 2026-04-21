using UnityEngine;


public class Bin : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private Teacup teacup;
    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup")
        {
            draggable.transform.position = draggable.startPosition;
            teacup.EmptyCup();            
        }
        else
        {
            Debug.Log($"Tried to bin {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }

    }
}
