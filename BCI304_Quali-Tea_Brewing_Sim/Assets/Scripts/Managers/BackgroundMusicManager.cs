using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic uniqueInstance;
    void Awake()
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
}