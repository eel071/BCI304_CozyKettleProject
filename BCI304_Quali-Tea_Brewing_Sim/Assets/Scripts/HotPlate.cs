using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    float waterTimer = 0f;
    float finalTime = 0f;
    bool waterHeating = false;
    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teapot")
        {
            Debug.Log($"Water is Heating");
            draggable.transform.position = transform.position + new Vector3(0, 1, 0);
            waterHeating = true;
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
            waterHeating = false;
            waterTimer = 0f;
            Debug.Log($"Heat Timer : {waterTimer}");
            Debug.Log($"Heat Final Time : {finalTime}");
        }
    }

    private void Update()
    {
        if (waterHeating == true)
        {
            waterTimer += Time.deltaTime;
            Debug.Log($"Heat Timer : {waterTimer}");
        }
    }
}
