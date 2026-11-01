using UnityEngine;
using UnityEngine.UI;

public class CursorUI : MonoBehaviour
{
    public static CursorUI instance;

    private Image cursor;

    [SerializeField] private Sprite pointer;
    [SerializeField] private Sprite handOpen;
    [SerializeField] private Sprite handClosed;
    [SerializeField] private Sprite handPoint;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        cursor = GetComponentInChildren<Image>();
    }

    public void HandOpen()
    {
        if (cursor != null && handOpen != null) cursor.sprite = handOpen;
    }

    public void HandClosed()
    {
        if (cursor != null && handClosed != null) cursor.sprite = handClosed;
    }

    public void Pointer()
    {
        if (cursor != null && pointer != null) cursor.sprite = pointer;
    }

    public void HandPoint()
    {
        if (cursor != null && handPoint != null) cursor.sprite = handPoint;
    }

}
