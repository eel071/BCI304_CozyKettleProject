using UnityEngine;

public class ContainerItem : MonoBehaviour
{
    public Container container;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnDestroy()
    {
        if (container != null)
        {
            container.itemSpawned = false;
        }
        else
        {
            Debug.Log("could not find container");
        }
    }
}
