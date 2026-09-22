using UnityEngine;
using System.Collections.Generic;

public class ContainerManager : MonoBehaviour, IDataPersistence
{
        
    public Dictionary<string, int> containerCount = new Dictionary<string, int>()
    {
        {"GreenTea", 15},
        {"BlackTea", 15},
        {"WhiteTea", 15},
        {"Lemon", 6},
        {"Sugar", 15},
        {"Milk", 5},
        {"Honey", 5},
        {"Seeds", 0}
    };

    public Dictionary<string, int> containerMax = new Dictionary<string, int>()
    {
        {"GreenTea", 15},
        {"BlackTea", 15},
        {"WhiteTea", 15},
        {"Lemon", 20},
        {"Sugar", 30},
        {"Milk", 20},
        {"Honey", 20},
        {"Seeds", 99}
    };
    
    
    [SerializeField] private Container[] containers;

    private static ContainerManager uniqueInstance;
    
    private void Awake()
    {
        containers = FindObjectsByType<Container>();
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

    public void LoadData(GameData data)
    {
        foreach (string key in new List<string>(containerCount.Keys))
        {
            if (data.containerItems.TryGetValue(key, out int count))
            {
                containerCount[key] = count;
            }
            else
            {
                Debug.Log($"{key} was not found in the saved data. ");
            }
        }
        
    }

    public void SaveData(ref GameData data)
    {
        foreach (string key in containerCount.Keys)
        {
            data.containerItems[key] = containerCount[key];
        }
    }

    public void AddLeaves()
    {
        AddContainerCount("GreenTea", 15);
        AddContainerCount("BlackTea", 15);
        AddContainerCount("WhiteTea", 15);      
    }

    public void UpdateContainers()
    {
        foreach (Container c in containers) c.UpdateStorage();
    }


    public void AddContainerCount(string container, int amount)
    {
        containerCount[container] = Mathf.Clamp(containerCount[container] + amount, 0, containerMax[container]);
        UpdateContainers();
    }
}
