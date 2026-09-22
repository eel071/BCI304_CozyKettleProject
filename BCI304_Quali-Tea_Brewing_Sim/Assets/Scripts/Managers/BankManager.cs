using UnityEngine;

public class BankManager : MonoBehaviour, IDataPersistence
{
    public float money = 0f;
    public float reputation;

    public void UpdateMoney(float amount)
    {
        money += amount;
    }

    public void LoadData(GameData data)
    {
        this.money = data.money;
    }

    public void SaveData(ref GameData data)
    {
        data.money = this.money;
    }
}
