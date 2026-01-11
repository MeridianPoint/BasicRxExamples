using System.Net.Mail;
using System.Net;
using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BasicRxUI : MonoBehaviour
{
    public TMP_InputField EmailInputField;

    public ReportInputResult EmailInputReport;

    public TMP_InputField PasswordInputField;

    public TMP_InputField ConfirmPasswordInputField;

    public ReportInputResult PasswordInputReport;

    private void EmailFieldLogic()
    {
        if (EmailInputField != null) {
            EmailInputField.onValueChanged.AsObservable()
                .Subscribe(HandleEmailInput)
                .AddTo(this);
        }
    }

    private void HandleEmailInput(string email)
    {
        if (email == string.Empty)
        {
            EmailInputReport.DisplayNeutral();
            return;
        }

        bool isValid = true;
        try
        {
            email = new MailAddress(email).Address;
        }
        catch (FormatException)
        {
            // address is invalid
            isValid = false;

        }

        if (!isValid) {
            EmailInputReport.DisplayError("Invalid Format");
        }
        else
        {
            EmailInputReport.DisplayCorrect();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EmailFieldLogic();
    }
}
