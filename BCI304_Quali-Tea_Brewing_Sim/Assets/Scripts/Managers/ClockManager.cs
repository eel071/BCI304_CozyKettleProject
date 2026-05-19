using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private TMP_Text clockText;
    private float elapsedTime;
    [SerializeField] private float timeScale = 24f;
    [SerializeField] private float timeInADay = 86400f;

    private static ClockManager uniqueInstance;
    private void Awake()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        elapsedTime = 6 * 3600f;
    }

    
    void Update()
    {
        elapsedTime += Time.deltaTime * timeScale;
        elapsedTime %= timeInADay;
        UpdateClockUI();
    }

    void UpdateClockUI()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);        

        string clockString = string.Format("{0:00}:{1:00}", hours, minutes);
        clockText.text = clockString;
    }        
}
