using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject repPopUpPrefab;
    
    private void CreatePopUp(GameObject prefab, Vector3 worldPos, string text)
    {
        GameObject popUp = Instantiate(prefab, worldPos, new Quaternion());
        popUp.GetComponent<PopUp>().popUpText = text;
        popUp.transform.SetParent(canvas.transform, false);
    }

    public void TipPopUp(float money)
    {
        CreatePopUp(popUpPrefab, new Vector3(-270, -25, 0), money.ToString("+$#0.00"));
    }

    public void RepPopUp(float rep)
    {
        CreatePopUp(repPopUpPrefab, new Vector3(250, 200, 0), rep.ToString("+##;-##"));
    }
}




