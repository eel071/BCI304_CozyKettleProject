using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeaInfoMenu : MonoBehaviour
{
    public TMP_Text teaNameText;
    public TMP_Text teaDescriptionText;

    public Image teaLiquidImage;

    public GameObject teaInfoDisplay;

    public Color blackTeaColour;
    public Color greenTeaColour;
    public Color whiteTeaColour;


    void Start()
    {
        teaInfoDisplay.SetActive(false);
    }

    void ShowTeaInfo()
    {
     
    }

    public void ShowBlackTea()

    {
        teaInfoDisplay.SetActive(true);

        teaNameText.text = "Black Tea";
        teaDescriptionText.text = "A bold, full-bodied tea with a rich flavour and dark colour. Black tea is allowed to fully oxidise during processing, which develops its deeper colour and stronger flavour. Depending on the variety, it can taste malty, fruity, roasted or slightly smoky.";

        teaLiquidImage.color = blackTeaColour;
    }

    public void ShowGreenTea()
    {
        teaInfoDisplay.SetActive(true);

        teaNameText.text = "Green Tea";
        teaDescriptionText.text = "A light and refreshing tea that can have grassy, vegetal, sweet or toasted flavours. After harvesting, the leaves are quickly heated by steaming or pan-firing to stop oxidation, helping them retain their greener colour and fresher flavour.";

        teaLiquidImage.color = greenTeaColour;
    }

    public void ShowWhiteTea()
    {
        teaInfoDisplay.SetActive(true);

        teaNameText.text = "White Tea";
        teaDescriptionText.text = "A delicate tea made with very little processing. The young leaves and buds are typically withered and dried, with much less oxidation than black tea. This gives white tea a light colour and soft, subtle flavour.";

        teaLiquidImage.color = whiteTeaColour;
    }

    public void ResetTeaInfo()
    {
        teaInfoDisplay.SetActive(false);
    }
}