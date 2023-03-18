using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using CloudLoginUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailSender : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField phoneInput;
    [SerializeField] TMP_Dropdown donationType;
    [SerializeField] MoneySenderManager moneySenderManager;
    [SerializeField] Button donateButton;
    [SerializeField] Button paypalButton;
    [SerializeField] Button venmoButton;

    [Space(20)]
    [SerializeField] TMP_InputField computerType;
    [SerializeField] TMP_Dropdown condition;
    [SerializeField] Toggle chargerToggle;
    [SerializeField] Toggle mouseToggle;
    [SerializeField] Toggle monitorToggle;
    [SerializeField] Toggle keyboardToggle;
    [SerializeField] Toggle headsetToggle;
    [SerializeField] Toggle webcamToggle;
    [SerializeField] Toggle othersToggle;
    [SerializeField] TMP_InputField otherInput;

    [Space(20)]
    [SerializeField] string adminSubject;
    [SerializeField] string donatorSubject;

    void Update()
    {
        otherInput.gameObject.SetActive(othersToggle.isOn);
        donateButton.interactable = nameInput.text != "" && emailInput.text != "" && phoneInput.text != "" &&
                                    donationType.value == 0 && computerType.text != "";
        paypalButton.interactable = nameInput.text != "" && emailInput.text != "" && phoneInput.text != "" &&
                                    donationType.value == 1 && moneySenderManager.amountField.text != "";
        venmoButton.interactable = nameInput.text != "" && emailInput.text != "" && phoneInput.text != "" &&
                                    donationType.value == 1 && moneySenderManager.amountField.text != "";
    }

    public void DonateEmail(string service)
    {
        if (donateButton.interactable && donationType.value == 0)
        {
            string adminBody =
            "Hello Leon,\n" +

            nameInput.text + " has donated a computer!\nEmail: " + emailInput.text +
            "\nPhone: " + phoneInput.text + "\n\nComputer Information:\n" +
            "Computer Type: " + computerType.text +
            "\nCondition: " + condition.options[condition.value].text +

            "\n\nAccessories:\n" +
            "Charger: " + chargerToggle.isOn + " | Mouse: " + mouseToggle.isOn +
            " | Monitor: " + monitorToggle.isOn + " | Keyboard: " + keyboardToggle.isOn +
            " | Headset: " + headsetToggle.isOn + " | Webcam: " + webcamToggle.isOn +
            " | Others: " + otherInput.text;
            SaveKey("COMPUTER", adminBody);



            string donatorBody =
            "Dear " + nameInput.text + ",\n\nWe hope this email finds you well. I would like to extend a heartfelt thank you for your generous donation to our computer donation program. thank you for your support. Your generosity is truly appreciated, and we look forward to your continued partnership in helping to make technology accessible to all.\n\n" +
            "For us to receive your physical computer, please drop off or mail the computer at this address: \n\n" +
            "Computer Information:\n" +
            "Computer Type: " + computerType.text +
            "\nCondition: " + condition.options[condition.value].text +

            "\n\nAccessories:\n" +
            "Charger: " + chargerToggle.isOn + " | Mouse: " + mouseToggle.isOn +
            " | Monitor: " + monitorToggle.isOn + " | Keyboard: " + keyboardToggle.isOn +
            " | Headset: " + headsetToggle.isOn + " | Webcam: " + webcamToggle.isOn +
            " | Others: " + otherInput.text;
        }

        if (donationType.value == 1)
        {
            string adminBody =
            "Hello Leon,\n" +

            nameInput.text + " has donated money!\nEmail: " + emailInput.text +
            "\nPhone: " + phoneInput.text + "\n\nAmount: $" + moneySenderManager.currentAmount +
            "\nService: " + service + "\nNote: " + moneySenderManager.noteField.text;

            SaveKey("MONEY", adminBody);
        }
    }


    string tempKey;
    string tempBody;
    public void SaveKey(string key, string body)
    {
        if (CloudManager.Instance.signedIn)
        {
            if(string.IsNullOrWhiteSpace(CloudLoginUser.CurrentUser.GetAttributeValue(key)))
            {
                CloudLoginUser.CurrentUser.SetAttribute(key, body);
            }
            else
            {
                CloudLoginUser.CurrentUser.RemoveAttribute(key, SetAttributeCallback);
                tempKey = key;
                tempBody = body;
            }
        }
    }
    void SetAttributeCallback(string message, bool hasError)
    {
        if (hasError)
        {
            Debug.LogError("Error removing attribute: " + message);
        }
        else
        {
            CloudLoginUser.CurrentUser.SetAttribute(tempKey, tempBody);
        }
    }
}