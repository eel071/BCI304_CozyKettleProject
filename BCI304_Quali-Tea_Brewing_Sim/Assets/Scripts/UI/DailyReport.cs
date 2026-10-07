using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DailyReport : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dailyReport;
    [SerializeField] private TextMeshProUGUI dayText;
    
    [SerializeField] private RectMask2D starsRect;
    private int maxStarsRect = 600;
    
    [SerializeField] private GameObject buttons;

    private CustomerSpawner customerSpawner;
    private ClockManager clockManager;
    private TipJar tipJar;
    private BankManager bankManager;

    void Awake()
    {
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        clockManager = FindAnyObjectByType(typeof(ClockManager)) as ClockManager;
        tipJar = FindAnyObjectByType(typeof(TipJar)) as TipJar;
        bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
    }
 
    public void DailyReportUpdate()
    {
        buttons.SetActive(false);
        dayText.text = $"Day {clockManager.dayCounter}";
        dailyReport.text = $"customers served: {customerSpawner.customersServed} \n tips earned: {tipJar.currentTips.ToString("$#0.00")} \n reputation gained: {bankManager.dailyReputation}";
        StartCoroutine(UpdateStars());
    }

    private IEnumerator UpdateStars()
    {
        Debug.Log("Updating stars");
        float currentRep = maxStarsRect * (1f - (float)bankManager.reputation / bankManager.maxReputation);
        float targetRep = maxStarsRect * (1f - (float)(bankManager.reputation + bankManager.dailyReputation) / bankManager.maxReputation);
        starsRect.padding = new Vector4(0f, 0f, currentRep, 0f);

        while (currentRep != targetRep)
        {
            currentRep = Mathf.MoveTowards(currentRep, targetRep, 15f * Time.deltaTime);
            Debug.Log($"updating: {currentRep}");
            starsRect.padding = new Vector4(0f, 0f, currentRep, 0f);
            yield return null;
        }

        Debug.Log("starts finished updating");
        bankManager.UpdateReputation();
        buttons.SetActive(true);
    }
}
