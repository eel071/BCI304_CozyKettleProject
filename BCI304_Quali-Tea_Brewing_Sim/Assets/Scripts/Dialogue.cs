using UnityEngine;
using TMPro; 

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customerText;

    [SerializeField] private TeaManager teaManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
    }

    private void SetCustomerText(string text)
    {
        customerText.enabled = true;
        customerText.text = text;
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

        SetCustomerText(customerOrder);
    }

    public void ScoreDialogue()
    {
        string scoreDialogue = "";

        if (teaManager.customerOrder != teaManager.tea)
        {
            scoreDialogue = "This isn't what I ordered!";
        }
        else
        {
            switch (teaManager.finalScore)
            {
                case >= 90:
                    scoreDialogue = "This is Perfect!";
                    break;
                case >= 75:
                    scoreDialogue = "Yum";
                    break;
                case >= 50:
                    scoreDialogue = "Okay...";
                    break;
                case >= 25:
                    scoreDialogue = "I've had better tea.";
                    break;
                case < 25:
                    scoreDialogue = "Can you even call this tea?";
                    break;
            }
            
        }
        SetCustomerText(scoreDialogue);
    }

}
