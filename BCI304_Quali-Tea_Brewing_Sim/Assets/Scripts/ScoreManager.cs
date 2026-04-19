using UnityEngine;

public class ScoreManager : MonoBehaviour
{    
    private float heatScore;   
    private float steepScore;
    private float fillScore;
    public int finalScore;

    [SerializeField] Teacup teacup;
    [SerializeField] Teapot teapot;
    [SerializeField] HotPlate hotPlate;

    public void CalculateScore()
    {
        heatScore = hotPlate.finalTime / hotPlate.tempGoal * 100;
        steepScore = teapot.finalSteep / teapot.steepGoal * 100;
        fillScore = teacup.fillLevel / 0.9f * 100;

        if (heatScore > 100)
        {
            heatScore = heatScore - ((heatScore - 100) * 2);
        }

        if (steepScore > 100)
        {
            steepScore = steepScore - ((steepScore - 100) * 2);
        }
        
        if (fillScore > 100)
        {
            fillScore = fillScore - ((fillScore - 100) * 2);
        }


        finalScore = (int)((heatScore + steepScore + fillScore) / 3);
    }


}
