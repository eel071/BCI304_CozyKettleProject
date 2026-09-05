using UnityEngine;
using TMPro;
using System.Collections;

public class PopUp : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    public string popUpText;
    private float duration = 1.5f;
    Color startColour = new Color32(50, 50, 50, 255);
    Color endColour = new Color32(50, 50, 50, 0);

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

            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Destroy(gameObject);
    }

}



