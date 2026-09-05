using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private GameObject canvas;
    
    private void CreatePopUp(Vector3 worldPos, string text)
    {
        GameObject popUp = Instantiate(popUpPrefab, worldPos, new Quaternion());
        popUp.GetComponent<PopUp>().popUpText = text;
        popUp.transform.SetParent(canvas.transform, false);
    }

    public void TipPopUp(float money)
    {
        CreatePopUp(new Vector3(-270, -25, 0), money.ToString("+$##.00"));
    }
}




