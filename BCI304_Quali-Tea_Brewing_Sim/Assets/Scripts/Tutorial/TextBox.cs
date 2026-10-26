using UnityEngine;
using TMPro; 
using System.Collections;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;

    [SerializeField] private float textSpeed;
    [SerializeField] private TextMeshProUGUI textBox;
    private string currentText;

    private Animator anim;
    private bool animateAfterText;

    [SerializeField] private AudioClip typingSFX;
       
    void Start()
    {
        anim =  gameObject.GetComponent<Animator>();
    }

    public void SetText(string text, bool waitingForAction)
    {
        currentText = text;
        StopAllCoroutines();
        StartCoroutine(TypeLine());     
        animateAfterText = !waitingForAction;   
    }

    private IEnumerator TypeLine()
    {
        int typeCount = 0;
        textBox.text = "";
        string displayedText = "";
        int charLength = 0;

        //display 1 character at a time
        foreach (char c in currentText.ToCharArray())
        {
            charLength ++; //increase the character length
            textBox.text = currentText;

            displayedText = textBox.text.Insert(charLength, "<color=#00000000>");
            textBox.text = displayedText;

            if (typingSFX != null && typeCount % 2 != 0) AudioManager.instance.PlaySFXClip(typingSFX, transform, 0.5f, 0.15f);

            typeCount ++;
            yield return new WaitForSeconds(textSpeed);
        }

        if (animateAfterText) anim.SetTrigger("Pulse");
    }

    private void OnMouseDown()
    {
        if (textBox.text != currentText)
        {
            // If still typing, finish the text instantly
            StopAllCoroutines();
            textBox.text = currentText;
            if (animateAfterText) anim.SetTrigger("Pulse");
        }
        else
        {
            tutorial.FinishedText();
            anim.SetTrigger("StopPulse");
        }

    }

}
