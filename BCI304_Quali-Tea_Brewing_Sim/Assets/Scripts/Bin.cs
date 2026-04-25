using UnityEngine;


public class Bin : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private Teacup teacup;
    
    public void OnDrop(Draggable draggable)
    {
        switch (draggable.tag)
        {
            case "Teacup":
                draggable.transform.position = draggable.startPosition;
                teacup.EmptyCup();
                break;
            case "Addition":
            case "Tea":
                Destroy(draggable.gameObject);
                break;
            default:
                Debug.Log($"Tried to bin {draggable.tag}");
                draggable.transform.position = draggable.startPosition;
                break;
        }

    }
}
