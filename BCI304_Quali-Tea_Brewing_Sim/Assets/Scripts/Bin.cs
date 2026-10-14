using UnityEngine;


public class Bin : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private Teacup teacup;
    [SerializeField] private Teapot teapot;

    public void OnDrop(Draggable draggable)
    {
        switch (draggable.tag)
        {
            case "Teacup":
                draggable.transform.position = draggable.startPosition;
                teacup.EmptyCup();
                break;
            case "Teapot":
                draggable.transform.position = draggable.startPosition;
                teapot.ResetTeapot();
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
