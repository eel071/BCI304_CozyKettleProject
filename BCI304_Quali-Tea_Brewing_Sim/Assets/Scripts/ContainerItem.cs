using UnityEngine;

public enum ContainerItems {BlackLeaves, GreenLeaves, WhiteLeaves, Lemon, Sugar, Milk, Honey, Seeds};

public class ContainerItem : MonoBehaviour
{
    public Container container;
    
    public ContainerItems item;


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

    public void ReturnItem()
    {
        container.ReturnItem();
        Destroy(gameObject); 
    }
}
