using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Vector3 startPosition;
    private Collider2D col;
    public bool dragging = false;

    private HoneyAnimation honeyAnim;

    private ContainerItem containerItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startPosition = transform.position;
        col = GetComponent<Collider2D>();

        honeyAnim = GetComponent<HoneyAnimation>();

        containerItem = GetComponent<ContainerItem>();
    }

    void Update()
    {
        if (dragging)
        {
            if (!Input.GetMouseButton(0)) //not holding left click
            {
                dragging = false;
                DropObject();
                return;
            }
            transform.position = GetMousePosition(); 
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }

    private void OnMouseDown()
    {   
        DragObject();
    }

    private void OnMouseUp()
    {
        DropObject();
    }

    public void DragObject()
    {
        dragging = true;
        transform.position = GetMousePosition();

        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out IOnPickUpBaseCollision onPickUpBaseCollision))
        {
            onPickUpBaseCollision.OnPickUp(this);                      
        }      
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition();
    }    

    private void DropObject()
    {
        dragging = false;
        col.enabled = false;
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(transform.position);
        col.enabled = true;

        foreach (Collider2D hitCollider in hitColliders)
        {   
            if (hitCollider != null && hitCollider.TryGetComponent(out IOnDropBaseCollision onDropBaseCollision))
            {
                if (honeyAnim != null)
                {
                    honeyAnim.PlayHoneyAnimation();
                    return;
                }

                onDropBaseCollision.OnDrop(this);
                return;
            }
        }

        ReturnItem();
    }

    public void ReturnItem()
    {
        if (containerItem != null) containerItem.ReturnItem();
        else transform.position = startPosition;
    }
}
