using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision
{
    public void OnDrop(Draggable draggable)
    {
        Debug.Log("Water is Heating");
        draggable.transform.position = transform.position + new Vector3(0,1,0);
    }
}
