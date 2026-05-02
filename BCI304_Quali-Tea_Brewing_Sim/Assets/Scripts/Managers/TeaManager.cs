using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeaManager : MonoBehaviour
{
    public enum TeaTypes {Water, Green, Black, White};
    public TeaTypes customerOrder;
    public TeaTypes tea;
    
    [SerializeField] GameObject teacup; 
    [SerializeField] SpriteRenderer teaRenderer;       
    [SerializeField] private Color water, green, black, white;
    private float tOpacity;
    private Color tColor;

    private static TeaManager uniqueInstance;
    private void Awake()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TeaStation"))
        {
            teacup = GameObject.Find("Teacup");
            teaRenderer = teacup.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        }        
    }

    public void SetTea(string teaName, float steepTime, float maxSteep)
    {
        switch (teaName)
        {
            case "LeavesGreen(Clone)":
                tea = TeaTypes.Green;
                tColor = green;
                //teaRenderer.color = new Color(green.r, green.g, green.b, Mathf.Clamp((steepTime/maxSteep), 0, 1));
                break;
            case "LeavesBlack(Clone)":
                tea = TeaTypes.Black;
                tColor = black;
                //teaRenderer.color = new Color(black.r, black.g, black.b, Mathf.Clamp((steepTime/maxSteep), 0, 1));
                break;
            case "LeavesWhite(Clone)":
                tea = TeaTypes.White;
                tColor = white;
                //teaRenderer.color = new Color(white.r, white.g, white.b, Mathf.Clamp((steepTime/maxSteep), 0, 1));
                break;
        }
        
        tOpacity = Mathf.Clamp((steepTime/maxSteep), 0, 1);

        Debug.Log("tea = " + tea);
    }

    public void UpdateTea()
    {
        teaRenderer.color = new Color(tColor.r, tColor.g, tColor.b, tOpacity);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTea();             
    }

    public void ResetTea()
    {
        tea = TeaTypes.Water;
        tColor = water;
        tOpacity = 1f;
    }

    public void SetCustomerOrder()
    {
        customerOrder = (TeaTypes)Random.Range(0, 3);
    }
}
