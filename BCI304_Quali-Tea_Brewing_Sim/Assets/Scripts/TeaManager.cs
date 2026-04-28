using UnityEngine;

public class TeaManager : MonoBehaviour
{
    private enum TeaTypes {Water, Green, Black, White};
    //private TeaTypes customerOrder;
    private TeaTypes tea;

    [SerializeField] SpriteRenderer teaRenderer;
    [SerializeField] private Color water, green, black, white;
    
        
    public void SetTea(string teaName, float steepTime, float maxSteep)
    {
        switch (teaName)
        {
            case "LeavesGreen(Clone)":
                tea = TeaTypes.Green;
                teaRenderer.color = new Color(green.r, green.g, green.b, Mathf.Clamp((steepTime/maxSteep), 0, 1));
                break;
            case "LeavesBlack(Clone)":
                tea = TeaTypes.Black;
                teaRenderer.color = new Color(black.r, black.g, black.b, Mathf.Clamp((steepTime/maxSteep), 0, 1));
                break;
            case "LeavesWhite(Clone)":
                tea = TeaTypes.White;
                teaRenderer.color = new Color(white.r, white.g, white.b, Mathf.Clamp((steepTime/maxSteep), 0, 1));
                break;
        }
        
        Debug.Log("tea = " + tea);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTea();
    }

    public void ResetTea()
    {
        tea = TeaTypes.Water;
        teaRenderer.color = water;
    }
}
