using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ticket;
    [SerializeField] private Image teaOrder;
    [SerializeField] private GameObject[] sugarCubes;
    [SerializeField] private GameObject sugarPanel;
    [SerializeField] private GameObject lemon;
    [SerializeField] private TeaManager teaManager;

    void Start()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager; //get a reference to the tea manager
    }

    public void ShowTicket()
    {
        ticket.SetActive(true);

        teaOrder.color = teaManager.teaOrderColor;

        if (teaManager.lemonOrder)
        {
            lemon.SetActive(true);
        }
        else
        {
            lemon.SetActive(false);
        }

        //reset sugar cubes
        for (int i = 0; i < sugarCubes.Length; i++)
        {
            sugarCubes[i].SetActive(false);
        }

        //show sugar cubes
        for (int i = 0; i < teaManager.sugarCubesOrder; i++)
        {
            sugarCubes[i].SetActive(true);
        }
    }

    public void HideTicket()
    {
        ticket.SetActive(false);
    }
}
