using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeaManager : MonoBehaviour
{
    public enum TeaTypes {Water, Green, Black, White};
    public TeaTypes customerOrder;
    public TeaTypes tea;
    public int sugarCubesOrder, sugarCubes;
    public bool lemonOrder, lemon;
    
    
    [SerializeField] GameObject teacup; 
    [SerializeField] SpriteRenderer teaRenderer;       
    [SerializeField] private Color water, green, black, white;
    private float tOpacity;
    private Color tColor;
    public Color teaOrderColor;

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
                break;
            case "LeavesBlack(Clone)":
                tea = TeaTypes.Black;
                tColor = black;
                break;
            case "LeavesWhite(Clone)":
                tea = TeaTypes.White;
                tColor = white;
                break;
        }
        
        tOpacity = Mathf.Clamp((steepTime/maxSteep), 0, 1);

        Debug.Log("tea = " + tea);
    }

    public void UpdateTea()
    {
        teaRenderer.color = new Color(tColor.r, tColor.g, tColor.b, tOpacity);
    }

    public void Additions(string addition)
    {
        switch (addition)
        {
            case "LemonSlice(Clone)":
                lemon = true;
                break;
            case "Sugar(Clone)":
                sugarCubes += 1;
                break;
    
        }
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
        sugarCubes = 0;
        lemon = false;

    }

    public void SetCustomerOrder()
    {
        customerOrder = (TeaTypes)Random.Range(0, 4);
        lemonOrder = Random.value < 0.5f;
        sugarCubesOrder = Random.Range(0,3);
    
        switch (customerOrder)
        {
            case TeaTypes.Water:
                teaOrderColor = water;
                break;
            case TeaTypes.Green:
                teaOrderColor = green;
                break;
            case TeaTypes.Black:
                teaOrderColor = black;
                break;
            case TeaTypes.White:
                teaOrderColor = white;
                break;
        }
    }
}
