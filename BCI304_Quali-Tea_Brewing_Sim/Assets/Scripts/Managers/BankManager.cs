using UnityEngine;
using TMPro;

public class BankManager : MonoBehaviour
{
    public float money;
    public float reputation;


    [SerializeField] private TMP_Text moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddMoney(float amount)
    {
        money += amount;
        UpdateMoneyUI();
    }



    private void UpdateMoneyUI()
    {
        moneyText.text = money.ToString("$##.00");
    }
}
