using UnityEngine;
using System.Collections;

public class SteamAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip steamClip;

    public void PlaySteam()
    {
        gameObject.SetActive(true);

        animator.Play("SteamAnimation", 0, 0f);

        StopAllCoroutines();
        StartCoroutine(HideAfterAnimation());
    }

    private IEnumerator HideAfterAnimation()
    {
        yield return new WaitForSeconds(steamClip.length);

        gameObject.SetActive(false);
    }

}
