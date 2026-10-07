using UnityEngine;

public class BankManager : MonoBehaviour, IDataPersistence
{
    public float money;
    public int reputation = 200;
    public int dailyReputation = 0;
    public int maxReputation = 1000;

    public int stars = 1;

    public void UpdateMoney(float amount)
    {
        money += amount;
    }

    public void UpdateReputation()
    {
        reputation += dailyReputation;
        reputation = Mathf.Clamp(reputation, 0, maxReputation);
        dailyReputation = 0;

        CheckStars();
    }

    private void CheckStars()
    {
        switch (reputation)
        {
            case >= 1000:
                if (stars < 5)
                {
                    Debug.Log("earned 5 stars!");
                }
                stars = 5;
                break;
            case >= 800:
                if (stars < 4)
                {
                    Debug.Log("earned 4 stars!");
                }
                else if (stars > 4)
                {
                    Debug.Log("lost a star :(");
                }
                stars = 4;
                break;
            case >= 600:
                if (stars < 3)
                {
                    Debug.Log("earned 3 stars!");
                }
                else if (stars > 3)
                {
                    Debug.Log("lost a star :(");
                }
                stars = 3;
                break;
            case >= 400:
                if (stars < 2)
                {
                    Debug.Log("earned 2 stars!");
                }
                else if (stars > 2)
                {
                    Debug.Log("lost a star :(");
                }
                stars = 2;
                break;
            case >= 200:
                if (stars < 1)
                {
                    Debug.Log("earned 1 star!");
                }
                else if (stars > 1)
                {
                    Debug.Log("lost a star :(");
                }
                stars = 1;
                break;
            case >= 0:
                if (stars > 0)
                {
                    Debug.Log("lost a star :(");
                }
                stars = 0;
                break;
            case <= 0:
                Debug.Log("all reputation lost");
                break;
        }
    }

    public void LoadData(GameData data)
    {
        this.money = data.money;
        this.reputation = data.reputation;
        this.stars = data.stars;
    }

    public void SaveData(ref GameData data)
    {
        data.money = this.money;
        data.reputation = this.reputation;
        data.stars = this.stars;
    }
}
