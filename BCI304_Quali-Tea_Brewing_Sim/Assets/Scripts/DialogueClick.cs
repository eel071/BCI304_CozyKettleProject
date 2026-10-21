using UnityEngine;

public class DialogueClick : MonoBehaviour
{
    private Dialogue dialogue;

    void Awake()
    {
        dialogue = FindAnyObjectByType(typeof(Dialogue)) as Dialogue;
    }

    private void OnMouseDown()
    {
        Debug.Log("clicked dialogue");
        if (dialogue != null) dialogue.SkipLine();
    }

}
