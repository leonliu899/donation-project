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

    
}
