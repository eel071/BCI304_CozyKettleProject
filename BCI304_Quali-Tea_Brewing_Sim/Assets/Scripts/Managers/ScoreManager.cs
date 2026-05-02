using UnityEngine;

public class ScoreManager : MonoBehaviour
{    
    private float heatScore;   
    private float steepScore;
    private float fillScore;
    private float teaScore;
    public int finalScore;

    [SerializeField] Teacup teacup;
    [SerializeField] Teapot teapot;
    [SerializeField] HotPlate hotPlate;
    [SerializeField] TeaManager teaManager;

    private void Start()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
    }

    public void CalculateScore()
    {
        heatScore = hotPlate.finalTime / hotPlate.tempGoal * 100;
        steepScore = teapot.finalSteep / teapot.steepGoal * 100;
        fillScore = teacup.fillLevel / 0.75f * 100;

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

        if (teaManager.customerOrder == teaManager.tea)
        {
            finalScore = (int)((heatScore + steepScore + fillScore) / 3); 
        }
        else
        {
            Debug.Log("tea does not match customer order");
            finalScore = 0;
        }
    }


}
