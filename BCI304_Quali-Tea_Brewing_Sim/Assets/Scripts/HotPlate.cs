using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    float waterTimer = 0f;
    float finalTime = 0f;

    [SerializeField] private GameObject teapot;
    private Teapot teapot_script;

    private void Start()
    {
        teapot_script = teapot.GetComponent<Teapot>();
    }

    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Teapot" && teapot_script.waterHeated == false)
        {
            Debug.Log($"Water is Heating");
            draggable.transform.position = transform.position + new Vector3(0, 1, 0);
            teapot_script.waterHeating = true;
        }        
        else
        {
            Debug.Log($"Tried to Heat {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }                
    }

    public void OnPickUp(Draggable draggable)
    {
        if (draggable.tag == "Teapot")
        {
            Debug.Log($"Heating has stopped");            
            finalTime = waterTimer;
            teapot_script.waterHeating = false;
            waterTimer = 0f;
            Debug.Log($"Heat Timer : {waterTimer}");
            Debug.Log($"Heat Final Time : {finalTime}");
            teapot_script.waterHeated = true;
        }
    }

    private void Update()
    {
        if (teapot_script.waterHeating == true)
        {
            waterTimer += Time.deltaTime;
            Debug.Log($"Heat Timer : {waterTimer}");
        }
    }
}
