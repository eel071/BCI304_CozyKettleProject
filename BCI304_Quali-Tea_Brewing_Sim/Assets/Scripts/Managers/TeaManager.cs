using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TeaTypes {Water, Green, Black, White};

public class TeaManager : MonoBehaviour
{
    
    public TeaTypes customerOrder;
    public TeaTypes tea;
    public int sugarCubesOrder, sugarCubes;
    public bool lemonOrder, lemon;
    public string teaOrder;

    public float finalScore;
    
    [SerializeField] GameObject teacup; 
    [SerializeField] SpriteRenderer teaRenderer;       
    [SerializeField] private Color water, green, black, white;
    private float tOpacity;
    private Color tColor;
    public Color teaOrderColor; //the colour for the ticket UI

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

    public void RandomiseCustomerOrder()
    {
        customerOrder = (TeaTypes)Random.Range(0, 4);
        lemonOrder = Random.value < 0.5f;
        sugarCubesOrder = Random.Range(0,3);
        updateOrderVariables();
    }

    public void SetCustomerOrder(TeaTypes teaOrder, bool lemon, int sugar)
    {
        customerOrder = teaOrder;
        lemonOrder = lemon;
        sugarCubesOrder = sugar;
        updateOrderVariables();
    }


    private void updateOrderVariables()
    {
        //updates the colour for the order ticket UI and the string for the dialogue 
        switch (customerOrder)
        {
            case TeaTypes.Water:
                teaOrderColor = water; 
                teaOrder = "Hot water";
                break;
            case TeaTypes.Green:
                teaOrderColor = green;
                teaOrder = "Green tea";
                break;
            case TeaTypes.Black:
                teaOrderColor = black;
                teaOrder = "Black tea";
                break;
            case TeaTypes.White:
                teaOrderColor = white;
                teaOrder = "White tea";
                break;
        }
    }

    
}
