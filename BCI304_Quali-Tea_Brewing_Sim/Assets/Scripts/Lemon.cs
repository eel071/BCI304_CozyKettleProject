using UnityEngine;

public class Lemon : MonoBehaviour
{
    private Tree tree;

    void Start()
    {
        tree = FindAnyObjectByType(typeof(Tree)) as Tree;
    }

    private void OnMouseDown()
    {
        tree.HarvestLemon();
        gameObject.SetActive(false); 
    }
}
