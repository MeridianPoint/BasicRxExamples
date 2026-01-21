using System.Net.Mail;
using System.Net;
using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BasicRxUI : MonoBehaviour
{
    public TMP_InputField EmailInputField;

    public ReportInputResult EmailInputReport;

    public TMP_InputField PasswordInputField;

    public TMP_InputField ConfirmPasswordInputField;

    public ReportInputResult PasswordInputReport;

    public ReportInputResult ConfirmPasswordInputReport;

    private void EmailFieldLogic()
    {
        if (EmailInputField != null) {
            EmailInputField.onValueChanged.AsObservable()
                .Subscribe(HandleEmailInput)
                .AddTo(this);
        }
    }

    private void PasswordLogic()
    {
        var password = PasswordInputField.onValueChanged
            .AsObservable()
            .ToReactiveProperty();

        var confirmPassword = ConfirmPasswordInputField.onValueChanged
            .AsObservable()
            .ToReactiveProperty();

        var combined = password.CombineLatest(confirmPassword, 
            (password1, password2) => (password1, password2));

        password
            .Subscribe(str  =>
            {
                if (str == string.Empty)
                {
                    PasswordInputReport.DisplayNeutral();
                    return;
                }

                if (!ValidationUtility.ValidatePassword(str))
                {
                    PasswordInputReport.DisplayError("Invalid Format");
                }
                else
                {
                    PasswordInputReport.DisplayCorrect();
                }

            })
            .AddTo(this);

        confirmPassword
            .Subscribe(str =>
            {
                if (str == string.Empty)
                {
                    ConfirmPasswordInputReport.DisplayNeutral();
                    return;
                }

                if (!ValidationUtility.ValidatePassword(str))
                {
                    ConfirmPasswordInputReport.DisplayError("Invalid Format");
                }
                else
                {
                    ConfirmPasswordInputReport.DisplayCorrect();
                }

            })
            .AddTo(this);

        combined
            .Where(pair => pair.password1 == string.Empty &&
                pair.password2 == string.Empty)
            .Subscribe(pair => HandlePassword(pair.password1, pair.password2))
            .AddTo(this);
    }

    private void HandleEmailInput(string email)
    {
        if (email == string.Empty)
        {
            EmailInputReport.DisplayNeutral();
            return;
        }

        if (!ValidationUtility.ValidateEmailAddress(email)) {
            EmailInputReport.DisplayError("Invalid Format");
        }
        else
        {
            EmailInputReport.DisplayCorrect();
        }
    }

    private void HandlePassword(string password, string confirmPassword)
    {
        if(password == string.Empty &&
                confirmPassword == string.Empty)
        {
            PasswordInputReport.DisplayNeutral();
        }

        if (password == confirmPassword) { 
            PasswordInputReport.DisplayCorrect();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EmailFieldLogic();
        PasswordLogic();
    }
}
