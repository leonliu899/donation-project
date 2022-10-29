using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenAnim : MonoBehaviour
{
    public RectTransform loginScreen;
    public RectTransform signUpScreen;
    public float swipeSpeed;

    bool login = true;

    public void SwitchToLogin()
    {
        StartCoroutine(LoginSwitchAnim());
    }

    public void SwitchToSignUp()
    {
        StartCoroutine(SignupSwitchAnim());
    }
    
    IEnumerator LoginSwitchAnim()
    {
        login = true;
        while(loginScreen.position.x > Screen.width / 2 && login == true)
        {
            loginScreen.position = Vector3.Lerp(loginScreen.position,
                                                      new Vector3(Screen.width / 2, loginScreen.position.y, 0),
                                                      Time.deltaTime * swipeSpeed);
            signUpScreen.position = Vector3.Lerp(signUpScreen.position,
                                                      new Vector3(-2000 + Screen.width, signUpScreen.position.y, 0),
                                                      Time.deltaTime * swipeSpeed);

            yield return new WaitForSeconds(0.001f);
            
        }
    }
    IEnumerator SignupSwitchAnim()
    {
        login = false;
        while(signUpScreen.position.x < Screen.width / 2 && login == false)
        {
            signUpScreen.position = Vector3.Lerp(signUpScreen.position,
                                                      new Vector3(Screen.width / 2, signUpScreen.position.y, 0),
                                                      Time.deltaTime * swipeSpeed);
            loginScreen.position = Vector3.Lerp(loginScreen.position,
                                                      new Vector3(2000 + Screen.width, loginScreen.position.y, 0),
                                                      Time.deltaTime * swipeSpeed);

            yield return new WaitForSeconds(0.001f);
        }
    }
}
