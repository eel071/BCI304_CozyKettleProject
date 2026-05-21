using UnityEngine;

public class TeaBushes : MonoBehaviour, IOnDropBaseCollision
{
    // option 1: similar to teacup pour.
    // option 2: just a drop, sets plant to watered and plays a little watering animation

    private int growthstage = 1;
    private bool watered = false;
    private bool finishedGrowing = false;

   /* bool wateringBushes = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Draggable drag = other.GetComponent<Draggable>(); //get a refence to the other objects Draggable script

        if (other.gameObject.CompareTag("WateringCan") && drag.dragging) //checks that the watering can is the object being dragged
        {
            wateringBushes = true;
            Debug.Log("watering bush");           
        }
    }*/

    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "WateringCan")
        {
            Debug.Log($"Watered {gameObject.name}");
            watered = true;
            //animation would go here
            draggable.transform.position = draggable.startPosition;
        }
    }

    public void UpdateGrowth()
    {
        if (watered || finishedGrowing)
        {
            if (growthstage < 2)
            {
                growthstage++;
                if (growthstage == 2)
                {
                    finishedGrowing = true;
                }
            } 
            
        }
        else
        {
            if (growthstage > 0)
            {
                growthstage--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
