using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ticket;
    [SerializeField] private Image teaOrder;
    [SerializeField] private TeaManager teaManager;

    void Start()
    {
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager; //get a reference to the tea manager
    }

    public void ShowTicket()
    {
        ticket.SetActive(true);
        teaOrder.color = teaManager.teaOrderColor;
    }

    public void HideTicket()
    {
        ticket.SetActive(false);
    }
}
