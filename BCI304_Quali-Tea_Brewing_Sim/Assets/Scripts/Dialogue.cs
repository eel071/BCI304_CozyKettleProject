using UnityEngine;
using TMPro; 

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customerText;

    [SerializeField] private TeaManager teaManager;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip R_Order;
    [SerializeField] private AudioClip R_AMAZING; // 90-100
    [SerializeField] private AudioClip R_WOW;     // 75-89
    [SerializeField] private AudioClip R_Sigh;    // 50-74
    [SerializeField] private AudioClip R_Angry;   // 0-49

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
    }

    private void SetCustomerText(string text, AudioClip soundToPlay)
    {
        customerText.enabled = true;
        customerText.text = text;

        if (audioSource != null && soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }

    public void HideDialogue()
    {
        customerText.enabled = false;
    }

    public void OrderDialogue()
    {
        string customerOrder;

        
        if (teaManager.sugarCubesOrder > 0)
        {

            if (teaManager.lemonOrder)
            {
                customerOrder = $"{teaManager.teaOrder} with {teaManager.sugarCubesOrder} sugar and lemon.";
            }
            else
            {
                customerOrder = $"{teaManager.teaOrder} with {teaManager.sugarCubesOrder} sugar.";
            }
        }
        else
        {
            if (teaManager.lemonOrder)
            {
                customerOrder = $"{teaManager.teaOrder} with lemon.";
            }
            else
            {
                customerOrder = $"{teaManager.teaOrder}";
            }
        }

        SetCustomerText(customerOrder, R_Order);
    }

    public void ScoreDialogue()
    {
        string scoreDialogue = "";
        AudioClip chosenReactionSound = null;

        if (teaManager.customerOrder != teaManager.tea)
        {
            scoreDialogue = "This isn't what I ordered!";
            chosenReactionSound = R_Angry;
            Debug.Log("sound");
        }
        else
        {
            switch (teaManager.finalScore)
            {
                case >= 90:
                    scoreDialogue = "This is Perfect!";
                    chosenReactionSound = R_AMAZING;
                    Debug.Log("sound");
                    break;
                case >= 75:
                    scoreDialogue = "Yum";
                    chosenReactionSound = R_WOW;
                    break;
                case >= 50:
                    scoreDialogue = "Okay...";
                    chosenReactionSound = R_Sigh;
                    break;
                case >= 25:
                    scoreDialogue = "I've had better tea.";
                    chosenReactionSound = R_Sigh;
                    break;
                case < 25:
                    scoreDialogue = "Can you even call this tea?";
                    chosenReactionSound = R_Angry;
                    break;
            }
            
        }
        SetCustomerText(scoreDialogue, chosenReactionSound);
    }

}
