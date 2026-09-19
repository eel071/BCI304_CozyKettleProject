using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private float price;
    private int owned = 0;
    [SerializeField] private float priceIncreaseMultiplier;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI ownedText;

    [SerializeField]private BankManager bankManager;
    [SerializeField]private ShopManager shopManager;
    [SerializeField]private ContainerManager containerManager;
    [SerializeField]private UpgradeManager upgradeManager;

    private enum Purchasable {Seeds, Sugar, Milk, Honey, Customer}
    [SerializeField] private Purchasable purchasable;

    [SerializeField] private bool isUpgrade;

    public void PurchaseItem()
    {
        if (bankManager == null) bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;

        bankManager.UpdateMoney((price * -1));
        
        if (containerManager.containerCount.ContainsKey(purchasable.ToString())) //check if item is in container dictionary
        {
            containerManager.containerCount[purchasable.ToString()] += 1;
            containerManager.UpdateContainers();
        }
        else if (upgradeManager.upgrade.ContainsKey(purchasable.ToString())) //check if item is in upgrade dictionary
        {
            upgradeManager.upgrade[purchasable.ToString()] += 1;
            price = Mathf.Round((price * priceIncreaseMultiplier)* 2f) / 2f; //round to the neared .5
        }
            else Debug.Log($"couldn't find {purchasable} in any dictionary"); //item is not in either dictionary
        
        if (shopManager == null) shopManager = FindAnyObjectByType(typeof(ShopManager)) as ShopManager;
        shopManager.UpdateShop();
    }


    public void UpdateShopItem(float currentMoney)
    {
        if (containerManager == null) containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
        if (upgradeManager == null) upgradeManager = FindAnyObjectByType(typeof(UpgradeManager)) as UpgradeManager;


        priceText.text = price.ToString("$##.00");
        if (currentMoney < price) button.interactable = false;
        else button.interactable = true;

        if (isUpgrade) //shop item is an upgrade
        {
            if (upgradeManager.upgrade.ContainsKey(purchasable.ToString()))
            {
                owned = upgradeManager.upgrade[purchasable.ToString()];
                if (upgradeManager.upgrade[purchasable.ToString()] >= upgradeManager.maxUpgrade[purchasable.ToString()])
                {
                    button.interactable = false;
                    priceText.text = "SOLD OUT";
                }
            }
            else Debug.Log($"{purchasable} is not in upgrade dictionary");
            
        }
        else //shop item is not an upgrade
        {
            owned = containerManager.containerCount[purchasable.ToString()];   
        }    
        ownedText.text = $"owned: {owned}";
    }
}
