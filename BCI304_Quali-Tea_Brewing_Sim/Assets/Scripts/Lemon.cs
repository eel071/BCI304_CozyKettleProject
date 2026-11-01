using UnityEngine;
using System.Collections.Generic;

public class Lemon : MonoBehaviour, IDataPersistence
{
    private Tree tree;
    public bool isActive = false;
    [SerializeField] private string id;
    
    [SerializeField] private AudioClip pickLemonSFX;

    

    void Start()
    {
        tree = FindAnyObjectByType(typeof(Tree)) as Tree;
    }

    void OnEnable()
    {
        isActive = true;
    }

    void OnMouseEnter()
    {
        CursorUI.instance.HandPoint();
    }

    void OnMouseExit()
    {
        CursorUI.instance.Pointer();
    }

    public void LoadData(GameData data)
    {
        data.activeLemons.TryGetValue(id, out bool active);
        if (active)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.activeLemons.ContainsKey(id))
        {
            data.activeLemons.Remove(id);
        }
        data.activeLemons.Add(id, isActive);
    }

    private void OnMouseDown()
    {
        tree.HarvestLemon();
        AudioManager.instance.PlaySFXClip(pickLemonSFX, transform, 1f, 0.15f);
        CursorUI.instance.Pointer();
        gameObject.SetActive(false); 
    }
}
