using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform progressBar;
    [SerializeField] private RectTransform goal;
    [SerializeField] private float progress, max, width, height;

    public void SetBar(float maxNum, float progressGoal) 
    {
        max = maxNum;
        
        goal.anchoredPosition = new Vector2((progressGoal/max) * width, 0f); 
    }


    public void SetProgress(float currentProgress)
    {
        progress = currentProgress;
        float newWidth = (progress/max) * width;

        progressBar.sizeDelta = new Vector2(newWidth, height);
    }


}
