using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Krivodeling.UI.Effects;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AuthenticationUIManager : MonoBehaviour
{
    [Required][SerializeField] GameObject loadingScreen;
    [Required][SerializeField] GameObject loadingBar;
    [Required][SerializeField] GameObject homeScreen;
    [Required][SerializeField] GameObject loginScreen;
    [Required][SerializeField] GameObject signupScreen;
    [SerializeField] UnityEvent OnLoad;
    
    [Space(20)]
    [Header("Login")]
    public TMP_InputField signinEmailField;
    public TMP_InputField signinPassField;
    public TMP_Text signinErrorText;

    [Header("Signup")]
    public TMP_InputField signupUsernameField;
    public TMP_InputField signupEmailField;
    public TMP_InputField signupPassField;
    public TMP_InputField signupPassConfirmField;
    public TMP_Text signUpErrorText;

    // POSSIBLE ERROR MESSAGES
    // Failed to create user ({:email=>[\"is invalid\"], :password=>[\"can't be blank\"], :username=>[\"can't be blank\"]})
    // Failed to create user ({:email=>[\"is invalid\"], :password_confirmation=>[\"doesn't match Password\"], 
    // :password=>[\"is too short (minimum is 6 characters)\"]})

    CloudManager cloudManager;
    bool doLoadingAnimation;

    void Start()
    {
        loadingBar.SetActive(true);
        doLoadingAnimation = true;

        loadingBar.GetComponent<CanvasGroup>().alpha = 1;
        homeScreen.SetActive(false);

        cloudManager = CloudManager.Instance;

        ShowLoginScreen();
        DoLoadingAnimation();

    }

    void Update()
    {
        signUpErrorText.text = cloudManager.signUpErrorMessage;
        cloudManager.signUpEmail = signupEmailField.text;
        cloudManager.signUpUsername = signupUsernameField.text;
        cloudManager.signUpPass = signupPassField.text;
        cloudManager.signUpPassConfirm = signupPassConfirmField.text;

        signinErrorText.text = cloudManager.signInErrorMessage;
        cloudManager.signinEmail = signinEmailField.text;
        cloudManager.signinPass = signinPassField.text;
    }

    void DoLoadingAnimation()
    {
        DOVirtual.Float(0, 1, 1, x =>
        {
            if(doLoadingAnimation)
                loadingBar.GetComponentInChildren<Image>().fillAmount = x;

            if(x <= 0.01f && cloudManager.applicationSetup && doLoadingAnimation)
            {
                doLoadingAnimation = false;
                UnBlurLoadingScreen(1);
                OnLoad.Invoke();
            }
        }).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
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

    // INSPECTOR CALLS

    public void UnBlurLoadingScreen(float duration)
    {
        
        DOVirtual.Float(0.5f, 0, duration, x =>
        {
            loadingScreen.GetComponent<UIBlur>().Intensity = x;
            loadingScreen.GetComponentInChildren<CanvasGroup>().alpha = x;
            loadingScreen.GetComponent<UIBlur>().Color = Color.Lerp(loadingScreen.GetComponent<UIBlur>().Color, Color.white, Time.deltaTime * 4);
        }).SetEase(Ease.Linear).OnComplete(()=>
        {
            loadingScreen.SetActive(false);
        });
    }
}
