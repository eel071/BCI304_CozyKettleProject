using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class FinishTea : MonoBehaviour, IOnDropBaseCollision
{
    [SerializeField] Teacup teacup;    
    [SerializeField] ScoreManager score;
    [SerializeField] LoadManager loadManager;

    [SerializeField] private AudioClip chimeSound;
    [SerializeField] private AudioSource myAudioSource;

    private void Start()
    {
        loadManager = FindAnyObjectByType(typeof(LoadManager)) as LoadManager;
    }
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
            //draggable.transform.position = draggable.startPosition;
            //draggable.transform.position = transform.position + new Vector3(0, 1, 0);
            teacup.transform.position = new Vector3(0f, -3.5f, 0);            
            loadManager.LoadFrontCounter();
        }
        else
        {
            Debug.Log($"Tried to Submit {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }
    }
}
