using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TeaTypes {Water, Green, Black, White};

public class TeaManager : MonoBehaviour
{
    
    public TeaTypes customerOrder;
    public TeaTypes tea;
    public int sugarCubesOrder, sugarCubes;
    public bool lemonOrder, lemon, honeyOrder, honey, milkOrder, milk;    
    public string teaOrder;

    public float finalScore;
    
    [SerializeField] GameObject teacup; 
    [SerializeField] SpriteRenderer teaRenderer;       
    [SerializeField] private Color water, green, black, white;
    private float tOpacity;
    public Color tColor;
    public Color teaOrderColor; //the colour for the ticket UI

    private float teaOpacity;

    //private static TeaManager uniqueInstance;
    
    [SerializeField] private ContainerManager containerManager;
    private Customer customer;

    private Tutorial tutorial;
    
    private void Awake()
    {
        /*if (uniqueInstance == null)
        {
            uniqueInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */
        tutorial = FindAnyObjectByType(typeof(Tutorial)) as Tutorial;
        containerManager = FindAnyObjectByType(typeof(ContainerManager)) as ContainerManager;
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TeaStation"))
        {
            teacup = GameObject.Find("Teacup");
            teaRenderer = teacup.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        }        
    }

    public void UpdateSteepValue(float steepTime, float maxSteep)
    {
        teaOpacity = Mathf.Clamp((steepTime/maxSteep), 0, 1);
    }

    public void SetTea()
    {
        Teacup teacupScript = teacup.GetComponent<Teacup>();
        switch (teacupScript.cupTea)
        {
            case TeaTypes.Green:
                tea = TeaTypes.Green;
                tColor = green;
                break;
            case TeaTypes.Black:
                tea = TeaTypes.Black;
                tColor = black;
                break;
            case TeaTypes.White:
                tea = TeaTypes.White;
                tColor = white;
                break;
            case TeaTypes.Water:
                tea = TeaTypes.Water;
                tColor = water;
                teaOpacity = 1;
                break;
        }
        
        tOpacity = teaOpacity;
    }

    public void UpdateTea()
    {
        teaRenderer.color = new Color(tColor.r, tColor.g, tColor.b, tOpacity);
    }

    public void Additions(ContainerItems itemAdded)
    {
        switch (itemAdded)
        {
            case ContainerItems.Lemon:
                lemon = true;
                break;
            case ContainerItems.Sugar:
                sugarCubes += 1;
                break;
            case ContainerItems.Honey:
                honey = true;
                break;
            case ContainerItems.Milk:
                milk = true;
                break;

        }
    }

    public void CheckTea()
    {
        if (sugarCubesOrder == sugarCubes && milkOrder == milk) tutorial.Ingredients();
        else if (sugarCubesOrder == sugarCubes) tutorial.SugarCubes();
        else if (milkOrder == milk) tutorial.Milk();   
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
        honey = false;
        milk = false;

    }

    public void RandomiseCustomerOrder()
    {
        customerOrder = (TeaTypes)Random.Range(0, 4);
        lemonOrder = Random.value < 0.5f;
        honeyOrder = Random.value < 0.5f;
        milkOrder = Random.value < 0.5f;
        sugarCubesOrder = Random.Range(0,3);
        UpdateOrderVariables();
    }

    public void SetCustomerOrder(TeaTypes teaOrder, bool lemon, int sugar, bool honey, bool milk)
    {
        customerOrder = teaOrder;
        lemonOrder = lemon;
        sugarCubesOrder = sugar;
        honeyOrder = honey;
        milkOrder = milk;
        UpdateOrderVariables();
    }

    private void UpdateOrderVariables()
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
