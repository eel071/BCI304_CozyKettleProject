using UnityEngine;
using UnityEngine.U2D;

public class SteepingLeaves : MonoBehaviour
{
    [SerializeField] private Teapot teapotScript;
    [SerializeField] private Sprite[] leafSprites;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (teapotScript.teapotTea)
        {
            case TeaTypes.Green:
                spriteRenderer.sprite = leafSprites[0];
                break;
            case TeaTypes.Black:
                spriteRenderer.sprite = leafSprites[1];
                break;
            case TeaTypes.White:
                spriteRenderer.sprite = leafSprites[2];
                break;
        }
    }
}
