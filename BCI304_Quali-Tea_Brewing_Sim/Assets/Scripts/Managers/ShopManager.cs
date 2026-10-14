using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private BankManager bankManager;
    [SerializeField] private TMP_Text moneyText;    

    [SerializeField] public ShopItem[] shopItems;

    void Awake()
    {
        bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
        shopItems = FindObjectsByType<ShopItem>(FindObjectsInactive.Include);       
    }
    

    void OnEnable()
    {
        UpdateShop();
    }

    public void UpdateShop()
    {        
        Debug.Log("Updating");
        if (bankManager != null) 
        {
            moneyText.text = bankManager.money.ToString("$#0.00");
            foreach (ShopItem i in shopItems) i.UpdateShopItem(bankManager.money);
        }
        else Debug.Log("bank manager null");
    }
}
