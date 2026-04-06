using UnityEngine;

public class TeaPot : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    float steepTimer = 0f;
    float finalSteep = 0f;
    bool teaSteeping = false;
    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Tea")
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
