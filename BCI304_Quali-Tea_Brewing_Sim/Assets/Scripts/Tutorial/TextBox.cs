using UnityEngine;
using TMPro; 
using System.Collections;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;

    [SerializeField] private float textSpeed;
    [SerializeField] private TextMeshProUGUI textBox;
    private string currentText;
       
    public void SetText(string text)
    {
        currentText = text;
        StartCoroutine(TypeLine());        
    }

    private IEnumerator TypeLine()
    {
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

            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void OnMouseDown()
    {
        if (textBox.text != currentText)
        {
            // If still typing, finish the text instantly
            StopAllCoroutines();
            textBox.text = currentText;
        }
        else
        {
            tutorial.FinishedText();
        }

    }

}
