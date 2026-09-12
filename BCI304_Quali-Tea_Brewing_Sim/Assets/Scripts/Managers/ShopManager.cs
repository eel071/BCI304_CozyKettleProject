using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private BankManager bankManager;
    [SerializeField] private TMP_Text moneyText;    

    [SerializeField] private ShopItem[] shopItems;

    void Awake()
    {
        bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
        UpdateShop();
    }

    public void UpdateShop()
    {        
        if (bankManager != null) moneyText.text = bankManager.money.ToString("$#0.00");
        foreach (ShopItem i in shopItems) i.UpdateShopItem(bankManager.money);
    }
}
