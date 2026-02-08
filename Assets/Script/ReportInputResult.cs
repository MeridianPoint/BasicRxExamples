using TMPro;
using UnityEngine;

public class ReportInputResult : MonoBehaviour
{
    public RectTransform CorrectIndicator;
    public RectTransform IncorrectIndicator;
    public TextMeshProUGUI ErrorMessage;

    public void DisplayError(string textMessage)
    {
        ErrorMessage.text = textMessage;
        ErrorMessage.gameObject.SetActive(true);
        CorrectIndicator.gameObject.SetActive(false);
        IncorrectIndicator.gameObject.SetActive(true);
    }

    public void DisplayCorrect()
    {
        ErrorMessage.gameObject.SetActive(false);
        CorrectIndicator.gameObject.SetActive(true);
        IncorrectIndicator.gameObject.SetActive(false);
    }

    public void DisplayNeutral()
    {
        ErrorMessage.gameObject.SetActive(false);
        CorrectIndicator.gameObject.SetActive(false);
        IncorrectIndicator.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ErrorMessage.gameObject.SetActive(false);
        CorrectIndicator.gameObject.SetActive(false);
        IncorrectIndicator.gameObject.SetActive(false);
    }
}
