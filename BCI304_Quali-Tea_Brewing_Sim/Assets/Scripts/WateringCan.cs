using UnityEngine;

public class WateringCan : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>(); //get a reference to the animator
    }

    public void WateringAnimation(Transform plantTransform, int growth)
    {
        Vector3 plantPos = plantTransform.position;

        //calculate the height of the watering position based on the growth stage of the plant
        float height = Mathf.Clamp((2.5f + (0.5f * growth)), 2.5f, 4f); 

        //update position of watering can to be above plant
        transform.position = new Vector3(plantPos.x + 1.5f, plantPos.y + height, 0f); 

        //trigger animation
        anim.SetTrigger("waterPlant"); 
        
        //once animation is finished it calls ReturnItem in draggable script.
    }
}


