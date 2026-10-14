using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileID = "";

    [Header("Content")]

    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI saveNameText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moneyText;

    private Button saveSlotButton;

    private SaveSlotsMenu saveSlotsMenu;

    public bool hasData;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
        saveSlotsMenu = FindAnyObjectByType(typeof(SaveSlotsMenu)) as SaveSlotsMenu;
    }

    public void SetData(GameData data)
    {
        if (data == null)
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            
        }

        else
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            saveNameText.text = data.shopName;
            dayText.text = $"DAY {data.dayCount}";
            moneyText.text = data.money.ToString("$#0.00");
        }
    }

    public string GetProfileID()
    {
        return this.profileID;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }

    public void DeleteSaveSlot()
    {
        DataPersistenceManager.instance.DeleteSaveSlot(profileID);
        saveSlotsMenu.ActivateMenu();
    }

    
}
