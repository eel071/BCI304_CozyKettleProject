using UnityEngine;

public class HoneyAnimation : MonoBehaviour
{
    private Animator animator;
    private Transform drizzlePosition;



    void Awake()
    {
        Debug.Log("HoneyAnimation AWAKE");
        animator = GetComponent<Animator>();

        GameObject target = GameObject.Find("HoneyDrizzlePosition");

        if (target != null)
        {
            drizzlePosition = target.transform;
        }
    }

    void Start()
    {
        Debug.Log("HoneyAnimation START");
    }

    public void PlayHoneyAnimation()
    {
        Debug.Log("DRIZZLE CALLED");

        if (drizzlePosition != null)
        {
            transform.position = drizzlePosition.position;
        }

        animator.SetTrigger("Drizzle");
    }

    public void DestroyHoney()
    {
        Destroy(gameObject);
    }
}
