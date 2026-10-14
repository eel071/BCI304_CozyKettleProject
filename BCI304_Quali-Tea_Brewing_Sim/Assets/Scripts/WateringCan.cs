using UnityEngine;

public class WateringCan : MonoBehaviour
{
    private Animator anim;
    private Draggable draggable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        draggable = gameObject.GetComponent<Draggable>();
    }

    public void WateringAnimation(Transform plantTransform, int growth)
    {
        Vector3 plantPos = plantTransform.position;
        float height = Mathf.Clamp((2.5f + (0.5f * growth)), 2.5f, 4f);
        transform.position = new Vector3(plantPos.x + 1.5f, plantPos.y + height, 0f);
        anim.SetTrigger("waterPlant");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
