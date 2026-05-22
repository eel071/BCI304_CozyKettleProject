using UnityEngine;

public class ScoreManager : MonoBehaviour
{    
    private float heatScore, steepScore, fillScore, teaScore, sugarScore, lemonScore;   
    public float finalScore;

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

        //calculate the lemon score
        if (teaManager.lemonOrder == teaManager.lemon)
        {
            lemonScore = 100f;
        }
        else
        {
            lemonScore = 0f;
        }

        //calculate the sugar score 
        //100 - (the difference between customers orders and number of cubes in the tea * 37.5)
        sugarScore = Mathf.Clamp((100f - Mathf.Abs(teaManager.sugarCubesOrder-teaManager.sugarCubes) * 37.5f), 0f, 100f);
    
        
        if (teaManager.customerOrder == teaManager.tea)
        {
            if (teaManager.tea == TeaManager.TeaTypes.Water)
            {
                finalScore = (int)((heatScore + fillScore + lemonScore + sugarScore) / 4);
            }
            else
                finalScore = (int)((heatScore + steepScore + fillScore + lemonScore + sugarScore) / 5); 
        }
        else
        {
            Debug.Log("tea does not match customer order");
            finalScore = 0;
        }

        teaManager.finalScore = finalScore;
    }


}
