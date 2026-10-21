using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customerText;
    [SerializeField] private Image dialogueBox;
    [SerializeField] private GameObject rejectButton;
    [SerializeField] private GameObject acceptButton;

    private string currentText;

    [SerializeField] private float textSpeed;
    private string dialogue;

    [SerializeField] private TeaManager teaManager;
    private BankManager bankManager;
    private Tutorial tutorial;
    private PopUpManager popUpManager;
    
    [SerializeField] private LoadManager loadManager;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip R_Angry;
    [SerializeField] private AudioClip typingSFX;

    private bool showButtons;

    private Customer customer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        bankManager = FindAnyObjectByType(typeof(BankManager)) as BankManager;
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
        popUpManager = FindAnyObjectByType(typeof(PopUpManager)) as PopUpManager;
        
        HideDialogue();
        HideButtons();
    }

    public void SetCustomerText(string text, bool buttons, AudioClip soundToPlay, Transform transform)
    {
        customerText.enabled = true;
        dialogueBox.enabled = true;

        currentText = text;
        showButtons = buttons;

        StartCoroutine(TypeLine());        

        if (soundToPlay != null)
        {
            AudioManager.instance.PlaySFXClip(soundToPlay, transform, 1f);
        }
    }

    private IEnumerator TypeLine()
    {
        //reset text
        customerText.text = "";
        string displayedText = "";
        int charLength = 0;

        //display 1 character at a time
        foreach (char c in currentText.ToCharArray())
        {
            charLength ++; //increase the character length
            customerText.text = currentText;

            displayedText = customerText.text.Insert(charLength, "<color=#00000000>");
            customerText.text = displayedText;

            if (audioSource != null && typingSFX != null) audioSource.PlayOneShot(typingSFX);
            yield return new WaitForSeconds(textSpeed);
        }

        FinishLine();

       
    }

    public void SkipLine()
    {
        if (customerText.text != currentText)
        {
            // If still typing, finish the text instantly
            StopAllCoroutines();
            customerText.text = currentText;
            FinishLine();
        }
    }

    private void FinishLine()
    {
         if (GameObject.FindWithTag("Customer") != null) customer = FindAnyObjectByType(typeof(Customer)) as Customer; 
        customer.StopTalking();

        if (showButtons) ShowButtons();
        if (tutorial.tutorialActive) tutorial.ShowTutorialStep();
    }

    
    

    #region show/hide
    public void HideDialogue()
    {
        customerText.enabled = false;
        dialogueBox.enabled = false;
    }

    private void ShowButtons()
    {
        if (tutorial.tutorialActive) rejectButton.SetActive(false);
        else rejectButton.SetActive(true);
        acceptButton.SetActive(true);
    }

    private void HideButtons()
    {
        rejectButton.SetActive(false);
        acceptButton.SetActive(false);
    }
    #endregion

    #region interactions

    public void RejectCustomer()
    {
        HideButtons();
        string dialogue = "Whatever."; //default rejection dialogue
        AudioClip angrySound = null;

        if (GameObject.FindWithTag("Customer") != null) customer = FindAnyObjectByType(typeof(Customer)) as Customer; 
        if (customer != null)
        {
            if (customer.rejectD != "") dialogue = customer.rejectD; //set unique rejection dialogue
            customer.CustomerTalk();
            angrySound = customer.angrySound; //set customer angry sound
            customer.destroyAfterTalk = true; //destroy customer after dialogue ends
        }
        
        SetCustomerText(dialogue, false, angrySound, transform); 
        //bankManager.reputation -= 5;
        bankManager.dailyReputation -= 5;
        popUpManager.RepPopUp(-5);
    }

    public void AcceptCustomer()
    {
        if (tutorial.tutorialActive) tutorial.AcceptOrder();
        HideButtons();
        StartCoroutine(WaitBeforeLoad()); //load the tea station
    }
    #endregion

    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(1f);
        loadManager.Load("TeaBrew");
        HideDialogue();
    }
}