using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector2 mousePosition;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // populate ourRigidbody
        rb = GetComponent<Rigidbody2D>();
    }
    private Vector2 GetMousePos()
    {
        //capture mouse position & return WorldPoint
        return (Vector2)Camera.main.WorldToScreenPoint(transform.position);
    }
    private void OnMouseDown()
    {
        mousePosition = (Vector2)Input.mousePosition - GetMousePos();

    }
    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition - mousePosition);
    }
}
