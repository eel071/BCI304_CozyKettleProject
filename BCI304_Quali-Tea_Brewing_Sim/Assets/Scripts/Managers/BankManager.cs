using UnityEngine;

public class BankManager : MonoBehaviour, IDataPersistence
{
    public float money;
    public int reputation = 200;
    //private int maxReputation = 1000;

    public void UpdateMoney(float amount)
    {
        money += amount;
    }

    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.reputation = data.reputation;
    }

    public void SaveData(ref GameData data)
    {
        data.money = this.money;
        data.reputation = this.reputation;
    }
}
