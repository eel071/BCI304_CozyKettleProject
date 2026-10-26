using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SteepingLeaves : MonoBehaviour
{
    [SerializeField] private Teapot teapotScript;
    [SerializeField] private Sprite[] leafSprites;
    private Image leafImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leafImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (teapotScript.teapotTea)
        {
            case TeaTypes.Green:
                leafImage.sprite = leafSprites[0];
                break;
            case TeaTypes.Black:
                leafImage.sprite = leafSprites[1];
                break;
            case TeaTypes.White:
                leafImage.sprite = leafSprites[2];
                break;
        }
    }
}
