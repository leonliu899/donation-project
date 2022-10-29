using CloudLoginUnity;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [Header("Server")]
    public string gameId;
    public string gameToken;

    [Space(20)]
    [Header("Login")]
    public string loginEmail;
    public string loginPass;
    [Header("Signup")]
    public string signupUsername;
    public string signupEmail;
    public string signupPass;
    public string signupPassConfirm;

    [Space(25)]
    [Header("Debug")]
    public bool applicationSetup;
    public bool signedUp;
    public bool signedIn;

    public static CloudManager Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        CloudLogin.SetUpGame(gameId, gameToken, ApplicationSetUp, true);
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
        CloudLogin.SignUp(signupEmail, signupPass, signupPassConfirm, signupUsername, SignedUpConfirm);
    }

    void SignedUpConfirm(string message, bool hasError)
    {
        if(hasError)
        {
            Debug.LogWarning(message);

            signedUp = false;
        } else 
        {
            Debug.Log("Signed up " + signupUsername);
            Debug.Log(message);

            signedUp = true;
        }
    }

    public void Signin()
    {
        CloudLogin.SignIn(loginEmail, loginPass, SignedInConfirm);
    }

    void SignedInConfirm(string message, bool hasError)
    {
        if(hasError)
        {
            Debug.LogWarning(message);

            signedIn = false;
        } else 
        {
            Debug.Log("Logged in: " + CloudLoginUser.CurrentUser.GetUsername());
            Debug.Log(message);

            signedIn = true;
        }
    }
}
