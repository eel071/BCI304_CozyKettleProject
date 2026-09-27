using UnityEngine;

public class FridgeSpriteSwap : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    private SpriteRenderer fridgeRenderer;
    private Collider2D fridgeCollider;
    private bool isOpen = false;

    void Awake()
    {
        fridgeRenderer = GetComponent<SpriteRenderer>();
        fridgeCollider = GetComponent<Collider2D>();

        //Debug.Log("FridgeSpriteSwap loaded");
    }

    void Update()
    {
        if (fridgeRenderer == null || fridgeCollider == null)
            return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition = new Vector2(mouseWorld.x, mouseWorld.y);

        bool mouseInside = fridgeCollider.OverlapPoint(mousePosition);

        if (mouseInside && !isOpen)
        {
            fridgeRenderer.sprite = openSprite;
            isOpen = true;
        }

        if (!mouseInside && isOpen && !Input.GetMouseButton(0))
        {
            fridgeRenderer.sprite = closedSprite;
            isOpen = false;
        }
    }
}