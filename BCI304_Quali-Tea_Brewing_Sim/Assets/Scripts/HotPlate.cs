using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    private float waterTimer = 0f;
    public float finalTime = 0f;

    [SerializeField] private Teapot teapot;

    [SerializeField] private float maxHeatTime = 10f;
    public float tempGoal = 8f; //will be changed depending on tea type

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameObject heatBar;

    [SerializeField] private AudioClip boilingSound;
    [SerializeField] private AudioSource myAudioSource;

    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Teapot" && teapot.waterHeated == false)
        {
            Debug.Log($"Water is Heating");
            draggable.transform.position = transform.position + new Vector3(0.2f, 1.25f, 0);
            teapot.waterHeating = true;

            //heating progress bar
            heatBar.SetActive(true);
            progressBar.SetBar(maxHeatTime, tempGoal);

            if (boilingSound!= null && myAudioSource != null)
            {
                // This plays the specific clip you dragged in
                myAudioSource.clip = boilingSound;
                myAudioSource.loop = true; // Make sure it keeps boiling
                myAudioSource.Play();
                Debug.Log("boiling sound start");
            }

        }        
        else
        {
            Debug.Log($"Tried to Heat {draggable.tag}");
            draggable.transform.position = draggable.startPosition;
        }                
    }

    public void OnPickUp(Draggable draggable)
    {
        if (draggable.tag == "Teapot")
        {
            Debug.Log($"Heating has stopped");            
            finalTime = waterTimer;
            teapot.waterHeating = false;
            waterTimer = 0f;
            // Debug.Log($"Heat Timer : {waterTimer}");
            Debug.Log($"Heat Final Time : {finalTime}");
            teapot.waterHeated = true;
            heatBar.SetActive(false); //hide heating progress bar

            if (boilingSound != null)
            {
                myAudioSource.Stop();
                Debug.Log("boiling sound stop");
            }
        }
    }

    private void Update()
    {
        if (teapot.waterHeating == true)
        {
            waterTimer += Time.deltaTime;
            progressBar.SetProgress(waterTimer); //updates heating progress bar
            // Debug.Log($"Heat Timer : {waterTimer}");
        }
    }
}
