using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;

    [SerializeField] private bool fadeOutEnabled = false;

    private void Awake()
    {
        sceneFadeImage = GetComponent<Image>();
    }

    private void Start()
    {
        fadeOutEnabled = false;
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        //set the start and target colours
        Color startColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);
        Color targetColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);

        //start fade coroutine
        yield return FadeCoroutine(startColour, targetColour, duration);

        fadeOutEnabled = true;
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        if (fadeOutEnabled) //stops it from fading out when first loading into the game
        {
            //set the start and target colours
            Color startColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
            Color targetColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);

            gameObject.SetActive(true);
            
            //start fade coroutine
            yield return FadeCoroutine(startColour, targetColour, duration);
        } 
        else yield return duration;       
    }

    private IEnumerator FadeCoroutine(Color startColour, Color targetColour, float duration)
    {

        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            float t = elapsedTime/duration;
            sceneFadeImage.color = Color.Lerp(startColour, targetColour, t);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        sceneFadeImage.color = targetColour;
        
    }

}
