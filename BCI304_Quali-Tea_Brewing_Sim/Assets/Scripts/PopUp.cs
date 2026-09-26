using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    public string popUpText;
    private float duration = 1.5f;
    Color startColour = new Color32(50, 50, 50, 255);
    Color endColour = new Color32(50, 50, 50, 0);

    [SerializeField] private Image image;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = popUpText;
        StartCoroutine(FadeAway());
    }

    public IEnumerator FadeAway()
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;
        
        yield return new WaitForSeconds(.5f);
        while (elapsedPercentage <1)
        {
            elapsedPercentage = elapsedTime/duration;
            text.color = Color.Lerp(startColour, endColour, elapsedPercentage);
            if (image != null) image.color = Color.Lerp(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 0), elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Destroy(gameObject);

        
    }

}



