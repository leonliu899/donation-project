using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoginFieldUpdater : MonoBehaviour
{
    public LevelLoader levelLoader;
    [Header("Login")]
    public TMP_InputField loginEmailField;
    public TMP_InputField loginPassField;
    public TMP_Text loginMessageText;
    [Space(15)]
    public GameObject loginEmailEmpty;
    public GameObject loginPassEmpty;

    [Header("Signup")]
    public TMP_InputField signupUsernameField;
    public TMP_InputField signupEmailField;
    public TMP_InputField signupPassField;
    public TMP_InputField signupPassConfirmField;
    [Space(15)]
    public GameObject signupUserEmpty;
    public GameObject signupEmailEmpty;
    public GameObject signupPassEmpty;
    public GameObject signupPassConfirmEmpty;
    public TMP_Text signupPassConfirmFail;
    public TMP_Text signupSuccessText;


    CloudManager cloudManager;

    void Start()
    {
        cloudManager = CloudManager.Instance;

        signupPassConfirmFail.gameObject.SetActive(false);
        signupSuccessText.gameObject.SetActive(false);
        
    }

    void Update()
    {
        signupSuccessText.gameObject.SetActive(cloudManager.signedUp);
        if(cloudManager.signedIn)
            levelLoader.LoadReadyScene();

        if(!cloudManager.signedInError)
            loginMessageText.text = cloudManager.signedInText;
    }

    public void Login()
    {
        if(!EmptyLoginFieldCheck())
        {
            cloudManager.loginEmail = loginEmailField.text;
            cloudManager.loginPass = loginPassField.text;
            cloudManager.Signin();
        }
    }
    
    public void Signup()
    {
        if(!EmptySignupFieldCheck())
        {
            cloudManager.signupUsername = signupUsernameField.text;
            cloudManager.signupEmail = signupEmailField.text;
            cloudManager.signupPass = signupPassField.text;
            cloudManager.signupPassConfirm = signupPassConfirmField.text;
            if(cloudManager.signupPassConfirm.Equals(cloudManager.signupPass))
            {
                cloudManager.SignUp();
            }
            else
                signupPassConfirmFail.gameObject.SetActive(true);
        }
    }

    bool EmptyLoginFieldCheck()
    {
        bool hasEmpty = false;
        if(EmptyText(loginEmailField.text))
        {
            loginEmailEmpty.SetActive(true);
            hasEmpty = true;
        } else
        {
            loginEmailEmpty.SetActive(false);
        }

        if(EmptyText(loginPassField.text))
        {
            loginPassEmpty.SetActive(true);
            hasEmpty = true;
        } else
        {
            loginPassEmpty.SetActive(false);
        }

        return hasEmpty;
    }

    bool EmptySignupFieldCheck()
    {
        return false;
    }
    
    bool EmptyText(string text)
    {
        return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
    }
}
