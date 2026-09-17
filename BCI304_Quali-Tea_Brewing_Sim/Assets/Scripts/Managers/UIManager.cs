using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ticket;
    [SerializeField] private Image teaOrder;
    /*[SerializeField] private GameObject[] sugarCubes;
    [SerializeField] private GameObject sugarPanel;
    [SerializeField] private GameObject lemon;
    */
    private TeaManager teaManager;
    private ClockManager clockManager;
    [SerializeField] private TextMeshProUGUI customerOrderText;
    [SerializeField] private Customer customer;

    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject dayOverScreen;
    [SerializeField] private TextMeshProUGUI dailyReport;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private GameObject seedCounter;
    [SerializeField] private TextMeshProUGUI seedCounterText;
    [SerializeField] private ContainerManager containerManager;


    [SerializeField] TipJar tipJar;
    [SerializeField] CustomerSpawner customerSpawner;


    [SerializeField] Button openShopButton;

    void Start()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager; //get a reference to the tea manager
        clockManager = FindAnyObjectByType(typeof(ClockManager)) as ClockManager;
    }

    public void ShowTicket()
    {
        ticket.SetActive(true);

        /*teaOrder.color = teaManager.teaOrderColor;

        if (teaManager.lemonOrder)
        {
            lemon.SetActive(true);
        }
        else
        {
            lemon.SetActive(false);
        }

        //reset sugar cubes
        for (int i = 0; i < sugarCubes.Length; i++)
        {
            sugarCubes[i].SetActive(false);
        }

        //show sugar cubes
        for (int i = 0; i < teaManager.sugarCubesOrder; i++)
        {
            sugarCubes[i].SetActive(true);
        }*/

        if (customer == null)
        {
            customer = FindAnyObjectByType(typeof(Customer)) as Customer;
        }

        string customerOrder;

        if (customer.orderD != "") customerOrder = customer.orderD; //if the customer has custom dialogue then use this dialogue
        else //generate default customer order dialogue
        {
            //sugar and lemon 
            if (teaManager.sugarCubesOrder > 0 && teaManager.lemonOrder) customerOrder = $"{teaManager.teaOrder} with {teaManager.sugarCubesOrder} sugar and lemon.";
            //just sugar
            else if (teaManager.sugarCubesOrder > 0) customerOrder = $"{teaManager.teaOrder} with {teaManager.sugarCubesOrder} sugar.";
            //just lemom
            else if (teaManager.lemonOrder) customerOrder = $"{teaManager.teaOrder} with lemon.";
            //no sugar or lemon
            else customerOrder = $"{teaManager.teaOrder}";
        }
        customerOrderText.text = customerOrder;
    }

    public void HideTicket()
    {
        ticket.SetActive(false);
    }

    public void OpenShop()
    {
        shop.SetActive(true);
    }

    public void CloseShop()
    {
        shop.SetActive(false);
    }

    public void DayOverScreen()
    {
        dayOverScreen.SetActive(true);
        dayText.text = $"Day {clockManager.dayCounter}";
        dailyReport.text = $"customers served: {customerSpawner.customersServed} \n tips earned:{tipJar.currentTips.ToString("$#0.00")}";
    }

    public void CloseDayOverscreen()
    {
        dayOverScreen.SetActive(false);
    }

    public void GardenUI(bool active)
    {
        seedCounter.SetActive(active);
        UpdateSeedCounter(containerManager.seedCount);
        openShopButton.gameObject.SetActive(active); 
    }

    public void UpdateSeedCounter(int amount)
    {
        seedCounterText.text = $"{amount}";
    }

}
