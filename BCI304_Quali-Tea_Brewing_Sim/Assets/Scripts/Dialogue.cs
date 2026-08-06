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

    [SerializeField] private float textSpeed;
    private string dialogue;

    [SerializeField] private TeaManager teaManager;
    [SerializeField] private LoadManager loadManager;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip R_Angry;
    [SerializeField] private AudioClip typingSFX;

    private Customer customer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        HideDialogue();
        HideButtons();
    }

    public void SetCustomerText(string text, AudioClip soundToPlay, bool rejected)
    {
        customerText.enabled = true;
        dialogueBox.enabled = true;

        StartCoroutine(TypeLine(text, rejected));        

        if (audioSource != null && soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }

    private IEnumerator TypeLine(string text, bool rejected)
    {
        //reset text
        customerText.text = "";
        string displayedText = "";
        int charLength = 0;

        //display 1 character at a time
        foreach (char c in text.ToCharArray())
        {
            charLength ++; //increase the character length
            customerText.text = text;

            displayedText = customerText.text.Insert(charLength, "<color=#00000000>");
            customerText.text = displayedText;

            if (audioSource != null && typingSFX != null) audioSource.PlayOneShot(typingSFX);
            yield return new WaitForSeconds(textSpeed);
        }

        if (GameObject.FindWithTag("Customer") != null) customer = FindAnyObjectByType(typeof(Customer)) as Customer; 
        customer.StopTalking();

        if (!rejected) ShowButtons();
    }

    #region show/hide
    public void HideDialogue()
    {
        customerText.enabled = false;
        dialogueBox.enabled = false;
    }

    private void ShowButtons()
    {
        rejectButton.SetActive(true);
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
            angrySound = customer.angrySound; //set customer angry sound
            customer.destroyAfterTalk = true; //destroy customer after dialogue ends
        }
        
        SetCustomerText(dialogue, angrySound, true); 
    }

    public void AcceptCustomer()
    {
        HideButtons();
        StartCoroutine(WaitBeforeLoad()); //load the tea station
    }
    #endregion

    IEnumerator WaitBeforeLoad()
    {
        yield return new WaitForSeconds(1f);
        loadManager.LoadTeaStation();
        HideDialogue();
    }
}