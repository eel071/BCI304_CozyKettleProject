using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Vector3 startPosition;
    private Collider2D col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        col = GetComponent<Collider2D>();
    }
    private Vector3 GetMousePosition()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
    private void OnMouseDown()
    {        
        transform.position = GetMousePosition();

        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IOnPickUpBaseCollision onPickUpBaseCollision))
        {
            Debug.Log("Collision Found"); 
            onPickUpBaseCollision.OnPickUp(this);                       
        }
        else
        {
            Debug.Log("No Collision Found");
            transform.position = startPosition;
        }
    }
    private void OnMouseDrag()
    {
        transform.position = GetMousePosition();
    }
    private void OnMouseUp()
    {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IOnDropBaseCollision onDropBaseCollision))
        {
            //Debug.Log("Collision Found"); 
            onDropBaseCollision.OnDrop(this);                       
        }
        else
        {
            //Debug.Log("No Collision Found");
            transform.position = startPosition;
        }
        
    }
}
