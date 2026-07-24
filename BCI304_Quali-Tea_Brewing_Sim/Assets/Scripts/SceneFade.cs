using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;

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
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        //set the start and target colours
        Color startColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        Color targetColour = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);

        gameObject.SetActive(true);
        
        //start fade coroutine
        yield return FadeCoroutine(startColour, targetColour, duration);        
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
