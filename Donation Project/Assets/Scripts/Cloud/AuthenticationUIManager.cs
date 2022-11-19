using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AuthenticationUIManager : MonoBehaviour
{
    [Required][SerializeField] GameObject loginScreen;
    [Required][SerializeField] GameObject signupScreen;

    void Start()
    {
        ShowLoginScreen();
    }

    public void ShowLoginScreen()
    {
        loginScreen.SetActive(true);
        signupScreen.SetActive(false);
    }

    public void ShowSignUpScreen()
    {
        loginScreen.SetActive(false);
        signupScreen.SetActive(true);
    }
}
