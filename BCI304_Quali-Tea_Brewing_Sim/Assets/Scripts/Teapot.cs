using System;
using UnityEngine;

public class Teapot : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    float steepTimer = 0f;
    float finalSteep = 0f;
    public bool teaSteeping = false;
    public bool teaSteeped = false;
    public bool waterHeating = false;
    public bool waterHeated = false;
    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Tea" && waterHeating == false && waterHeated == true && teaSteeping == false && teaSteeped == false)
        {
            draggable.transform.position = transform.position + new Vector3(0, 1.5f, 0);
            Debug.Log($"Steeping {draggable.gameObject.name}");
            teaSteeping = true;
        }        
        else
        {
            Debug.Log($"Tried to Add {draggable.tag} to teapot");
            draggable.transform.position = draggable.startPosition;
        }
        
    }
    public void OnPickUp(Draggable draggable)
    {
        if (draggable.tag == "Tea")
        {
            Destroy(draggable.gameObject);
            Debug.Log($"Steeping has stopped");
            finalSteep = steepTimer;
            teaSteeping = false;
            steepTimer = 0f;
            Debug.Log($"Steep Timer : {steepTimer}");
            Debug.Log($"Steep Final Time : {finalSteep}");
            teaSteeped = true;
        }
    }

    private void Update()
    {
        if (teaSteeping == true)
        {
            steepTimer += Time.deltaTime;
            Debug.Log($"Steep Timer : {steepTimer}");
        }
    }

}
