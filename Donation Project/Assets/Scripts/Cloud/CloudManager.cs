using System;
using CloudLoginUnity;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CloudManager : MonoBehaviour
{
    [Header("Server")]
    public string gameId;
    public string gameToken;
    public UnityEvent OnSignIn;    

    [Header("Sign In")]
    [ReadOnly] public string signinEmail;
    [ReadOnly] public string signinPass;
    [Header("Sign Up")]
    [ReadOnly] public string signUpUsername;
    [ReadOnly] public string signUpEmail;
    [ReadOnly] public string signUpPass;
    [ReadOnly] public string signUpPassConfirm;

    [Header("Debug")]
    [ReadOnly] public bool applicationSetup;
    [ReadOnly] public bool signedUp;
    [ReadOnly] public bool signedIn;
    [ReadOnly] public bool hasSignUpError;
    [ReadOnly] public string signUpErrorMessage;
    [ReadOnly] public bool hasSignInError;
    [ReadOnly] public string signInErrorMessage;

    public static CloudManager Instance;
    

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        signUpErrorMessage = "";
        signInErrorMessage = "";
        CloudLogin.SetUpGame(gameId, gameToken, ApplicationSetUp, true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) OnSignIn?.Invoke();
    }

    void ApplicationSetUp(string message, bool hasError)
    {
        if(hasError)
        {
            Debug.LogWarning(message);
            applicationSetup = false;
        } else {
            Debug.Log("Application Setup Success");
            Debug.Log(message);
            applicationSetup = true;
        }
    }


    public void SignUp()
    {
        if(applicationSetup)
            CloudLogin.SignUp(signUpEmail, signUpPass, signUpPassConfirm, signUpUsername, SignedUpConfirm);
    }

    void SignedUpConfirm(string message, bool hasError)
    {
        if(hasError)
        {
            Debug.LogWarning(message);
            signUpErrorMessage = message;
            hasSignUpError = true;
            signedUp = false;
        } else 
        {
            Debug.Log("Signed up " + signUpUsername);
            Debug.Log(message);
            signUpErrorMessage = message;
            hasSignUpError = false;
            signedUp = true;

            OnSignIn?.Invoke();
        }
    }



    public void SignIn()
    {
        CloudLogin.SignIn(signinEmail, signinPass, SignedInConfirm);
    }

    void SignedInConfirm(string message, bool hasError)
    {
        if(hasError)
        {
            Debug.LogWarning(message);
            signInErrorMessage = message;
            hasSignInError = true;
            signedIn = false;
        } else 
        {
            Debug.Log("Logged in: " + CloudLoginUser.CurrentUser.GetUsername());
            Debug.Log(message);
            signInErrorMessage = message;
            hasSignInError = false;
            signedIn = true;

            OnSignIn?.Invoke();
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
