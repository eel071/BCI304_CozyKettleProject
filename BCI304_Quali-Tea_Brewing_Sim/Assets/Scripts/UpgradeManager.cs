using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour, IDataPersistence
{

    public Dictionary<string, int> upgrade = new Dictionary<string, int>()
    {
        {"Customer", 0}
    };

    public Dictionary<string, int> maxUpgrade = new Dictionary<string, int>()
    {
        {"Customer", 3}
    };

    public void LoadData(GameData data)
    {
        foreach (string key in new List<string>(upgrade.Keys))
        {
            if (data.upgrades.TryGetValue(key, out int count))
            {
                upgrade[key] = count;
            }
            else
            {
                Debug.Log($"{key} was not found in the saved data. ");
            }
        }
        
    }

    public void SaveData(ref GameData data)
    {
        foreach (string key in upgrade.Keys)
        {
            data.upgrades[key] = upgrade[key];
        }
    }
}
