using UnityEngine;

public class TipJar : MonoBehaviour
{
    [SerializeField] private Sprite[] jarSprites;
    private SpriteRenderer spriteRenderer;

    private float currentTips;
    private float maxTips = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetTipJar()
    {
        currentTips = 0;
        UpdateSprite();
    }

    public void AddTips(float amount)
    {
        currentTips += amount;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (jarSprites.Length > 0)
        {
            float currentPhase = (float)currentTips / (float)maxTips;
            if (currentTips >= maxTips) currentPhase = 1;
            int sprite = Mathf.FloorToInt((currentPhase) * (jarSprites.Length-1));
            if (sprite == 0 && currentTips > 0) sprite = 1;
            spriteRenderer.sprite = jarSprites[sprite];
        }
    }
}
