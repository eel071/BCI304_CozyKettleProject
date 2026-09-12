using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private float price;
    private int owned = 0;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI ownedText;

    private BankManager bankManager;
    private ShopManager shopManager;
    private ContainerManager containerManager;

    private enum Purchasable {seeds, sugar}
    [SerializeField] private Purchasable purchasable;


    public void PurchaseItem()
    {
        if (bankManager == null) bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
        bankManager.UpdateMoney((price * -1));
        
        switch (purchasable)
        {
            case Purchasable.seeds:
                containerManager.seedCount += 1;
                break;
            case Purchasable.sugar:
                containerManager.sugarCount += 1;
                break;
        }

        if (shopManager == null) shopManager = FindAnyObjectByType(typeof(ShopManager)) as ShopManager;
        shopManager.UpdateShop();
    }


    public void UpdateShopItem(float currentMoney)
    {
        if (containerManager == null) containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
        switch (purchasable)
        {
            case Purchasable.seeds:
                owned = containerManager.seedCount;
                break;
            case Purchasable.sugar:
                owned = containerManager.sugarCount;
                break;
        }   
        
        priceText.text = price.ToString("$##.00");
        ownedText.text = $"owned: {owned}";
        if (currentMoney < price) button.interactable = false;
    }
}
