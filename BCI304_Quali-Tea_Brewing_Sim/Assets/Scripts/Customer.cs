using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Customer : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] private TeaManager teaManager;
    [SerializeField] LoadManager loadManager;

    void Awake()
    {
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
        teaManager = FindAnyObjectByType(typeof(TeaManager)) as TeaManager;
        teaManager.SetCustomerOrder();
    }    

    void OnMouseUp()
    {
        Debug.Log($"{teaManager.customerOrder}");
        loadManager.LoadTeaStation();
    }

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup")
        {
            Destroy(draggable.transform.parent.gameObject);
            teaManager.ResetTea();
            Destroy(gameObject);
        }
    }
}
