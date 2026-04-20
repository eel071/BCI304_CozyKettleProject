using UnityEngine;
using UnityEngine.UIElements;



public class FinishTea : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] Teacup teacup;
    [SerializeField] ScoreManager score;

    [SerializeField] private AudioClip chimeSound;
    [SerializeField] private AudioSource myAudioSource;
    public void OnDrop(Draggable draggable)
    {
        if (draggable.tag == "Teacup" && teacup.teaFilled == true)
        {
            score.CalculateScore();
            Debug.Log($"{score.finalScore}%");

            if (chimeSound != null && myAudioSource != null)
            {
                myAudioSource.PlayOneShot(chimeSound);
            }
        }
        else
        {
            Debug.Log($"Tried to Submit {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }
    }
}
