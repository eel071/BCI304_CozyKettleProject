using UnityEngine;

public class Teacup : MonoBehaviour, IOnDropBaseCollision
{
    public void OnDrop(Draggable draggable)
    {
        Debug.Log("Tea Filled");
        draggable.transform.position = draggable.startPosition;
    }
}
