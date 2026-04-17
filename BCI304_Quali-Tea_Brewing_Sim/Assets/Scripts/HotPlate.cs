using UnityEngine;

public class HotPlate : MonoBehaviour, IOnDropBaseCollision, IOnPickUpBaseCollision
{
    float waterTimer = 0f;
    float finalTime = 0f;

    [SerializeField] private GameObject teapot;
    private Teapot teapot_script;

    [SerializeField] private float maxHeatTime = 5f;
    public float tempGoal = 3f; //will be changed depending on tea type

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameObject heatBar;

    private void Start()
    {
        teapot_script = teapot.GetComponent<Teapot>();
    }

    public void OnDrop(Draggable draggable)
    {        
        if (draggable.tag == "Teapot" && teapot_script.waterHeated == false)
        {
            Debug.Log($"Water is Heating");
            draggable.transform.position = transform.position + new Vector3(0.2f, 1.25f, 0);
            teapot_script.waterHeating = true;

            //heating progress bar
            heatBar.SetActive(true);
            progressBar.SetBar(maxHeatTime, tempGoal);
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
            teapot_script.waterHeating = false;
            waterTimer = 0f;
            // Debug.Log($"Heat Timer : {waterTimer}");
            Debug.Log($"Heat Final Time : {finalTime}");
            teapot_script.waterHeated = true;
            heatBar.SetActive(false); //hide heating progress bar
        }
    }

    private void Update()
    {
        if (teapot_script.waterHeating == true)
        {
            waterTimer += Time.deltaTime;
            progressBar.SetProgress(waterTimer); //updates heating progress bar
            // Debug.Log($"Heat Timer : {waterTimer}");
        }
    }
}
