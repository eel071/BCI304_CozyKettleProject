using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private TeaManager teaManager;
    private ClockManager clockManager;
    private Tutorial tutorial;
    private Customer customer;
    private CustomerSpawner customerSpawner;
    private ContainerManager containerManager;
    

    [Header("End of Day UI")]
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject dayOverScreen;
    [SerializeField] private DailyReport dailyReport;

    //shop
    [SerializeField] private GameObject ingredientsPanel;
    [SerializeField] private GameObject gardenPanel;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private TextMeshProUGUI ingredientsText;
    [SerializeField] private TextMeshProUGUI gardenText;
    [SerializeField] private TextMeshProUGUI upgradeText;


    [Header("Tea Station UI")]
    [SerializeField] private GameObject ticket;
    [SerializeField] private GameObject ticketButton;
    [SerializeField] private GameObject binButton;
    [SerializeField] private GameObject startDayButton;
    [SerializeField] private GameObject toBrewButton;
    [SerializeField] private GameObject toAddButton;
    [SerializeField] private GameObject brewStationCounts;
    [SerializeField] private GameObject addStationCounts;
    [SerializeField] private TextMeshProUGUI customerOrderText;

    [Header("GardenUI")]
    [SerializeField] private GameObject seedCount;


    [Header("Audio")]
    [SerializeField] private AudioClip clickSfx;

    void Start()
    {
        //get a references
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager; 
        clockManager = FindAnyObjectByType(typeof(ClockManager)) as ClockManager;
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;     
        customerSpawner = FindAnyObjectByType(typeof(CustomerSpawner)) as CustomerSpawner;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
    }

    public void BrewingStationUI(bool active)
    {
        ticketButton.SetActive(active);
        binButton.SetActive(active);
        toAddButton.SetActive(active);
        brewStationCounts.SetActive(active);
    }

    public void AdditionStationUI(bool active)
    {
        ticketButton.SetActive(active);
        binButton.SetActive(active);
        toBrewButton.SetActive(active);
        addStationCounts.SetActive(active);
    }

     public void GardenUI(bool active)
    {
        seedCount.SetActive(active); 
        startDayButton.SetActive(active);
    }

    public void ShowTicket()
    {
        if (tutorial.tutorialActive) tutorial.OpenTicket();
        ticket.SetActive(true);

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
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        ticket.SetActive(false);
        if (tutorial.tutorialActive) tutorial.CloseTicket();
    }

    public void OpenShop()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        shop.SetActive(true);
    }

    public void CloseShop()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        shop.SetActive(false);
    }

    public void DayOverScreen()
    {
        dayOverScreen.SetActive(true);
        dailyReport.DailyReportUpdate();
    }

    public void CloseDayOverscreen()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        dayOverScreen.SetActive(false);
    }  

    public void ShopGardenPanel()
    {   
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        gardenPanel.SetActive(true);
        ingredientsPanel.SetActive(false);
        upgradesPanel.SetActive(false);

        gardenText.fontStyle = FontStyles.Bold;
        ingredientsText.fontStyle = FontStyles.Normal; 
        upgradeText.fontStyle = FontStyles.Normal; 
    }

    public void ShopIngredientsPanel()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        ingredientsPanel.SetActive(true);
        gardenPanel.SetActive(false);
        upgradesPanel.SetActive(false);

        gardenText.fontStyle = FontStyles.Normal;
        ingredientsText.fontStyle = FontStyles.Bold; 
        upgradeText.fontStyle = FontStyles.Normal; 
    }

    public void ShopUpgradesPanel()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        upgradesPanel.SetActive(true);
        ingredientsPanel.SetActive(false);
        gardenPanel.SetActive(false);

        gardenText.fontStyle = FontStyles.Normal;
        ingredientsText.fontStyle = FontStyles.Normal; 
        upgradeText.fontStyle = FontStyles.Bold; 
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySFXClip(clickSfx, transform, 1f, 0.15f);
        clockManager.UpdateBeforeQuit();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
