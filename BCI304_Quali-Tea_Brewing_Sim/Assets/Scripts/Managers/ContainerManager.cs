using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public int teaMax, lemonMax, sugarMax, honeyMax, milkMax;
    public int greenTeaCount, blackTeaCount, whiteTeaCount, lemonCount, sugarCount, honeyCount, milkCount;

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

    public void AddLeaves()
    {
        //temporarily resets the tea counts, will change this later when theres a way to make the different tea leaves.
        greenTeaCount = teaMax;
        blackTeaCount = teaMax;
        whiteTeaCount = teaMax;
    }
    public void AddLemons()
    {
        lemonCount = lemonMax;
    }
}
