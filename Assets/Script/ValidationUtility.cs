using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// utility class for validation
/// </summary>
public class ValidationUtility
{
    // Individual regex patterns for different requirements
    private static readonly Regex MinMaxLength = new Regex(@"^.{8,32}$");
    private static readonly Regex HasUppercase = new Regex(@"[A-Z]");
    private static readonly Regex HasLowercase = new Regex(@"[a-z]");
    private static readonly Regex HasDigit = new Regex(@"[0-9]");
    private static readonly Regex HasSpecialChar = new Regex(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]");

    // Combined single regex approach (alternative method)
    private static readonly Regex CombinedPattern = new Regex(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$"
    );

    /// <summary>
    /// validate if <paramref name="emailAddress"/> is a valid email address
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <returns></returns>
    public static bool ValidateEmailAddress(string emailAddress)
    {
        Regex regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

        return regex.IsMatch(emailAddress);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool ValidatePassword(string password) {

        if (!MinMaxLength.IsMatch(password) ||
            !HasUppercase.IsMatch(password) ||
            !HasLowercase.IsMatch(password) ||
            !HasDigit.IsMatch(password) ||
            !HasSpecialChar.IsMatch(password)
            )
        {
            return false;
        }

        return true;
    }
}
