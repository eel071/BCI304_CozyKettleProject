using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{

    public Dictionary<string, int> upgrade = new Dictionary<string, int>()
    {
        {"Customer", 0}
    };

    public Dictionary<string, int> maxUpgrade = new Dictionary<string, int>()
    {
        {"Customer", 3}
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
