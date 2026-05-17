using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public int teaMax, lemonMax, sugarMax;
    public int greenTeaCount, blackTeaCount, whiteTeaCount, lemonCount, sugarCount;

    private static ContainerManager uniqueInstance;
    
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
