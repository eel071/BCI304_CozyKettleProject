using UnityEngine;

public class BankManager : MonoBehaviour
{
    public float money = 0f;
    public float reputation;

    public void UpdateMoney(float amount)
    {
        money += amount;
    }
}
