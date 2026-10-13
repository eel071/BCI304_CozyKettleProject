using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;

    private bool fadeOutEnabled = false;

    private void Awake()
    {
        sceneFadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        
        //set the start and target colours
        Color startColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);
        Color targetColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);

        //start fade coroutine
        yield return FadeCoroutine(startColour, targetColour, duration);

        gameObject.SetActive(false);
        fadeOutEnabled = true;
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
        float elapsedPercentage = 0;
        
        while (elapsedPercentage <1)
        {
            elapsedPercentage = elapsedTime/duration;
            sceneFadeImage.color = Color.Lerp(startColour, targetColour, elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

    }

}
