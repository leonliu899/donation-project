using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySenderManager : MonoBehaviour
{
    [SerializeField] string venmoUsername;
    [SerializeField] string paypalUsername;
    [SerializeField] public TMP_InputField amountField;
    [SerializeField] public TMP_InputField noteField;
    [SerializeField] public int currentAmount;
    [SerializeField] int minimumAmount; 
    [SerializeField] EmailSender emailSender;
    public void PayWithVenmo()
    {
        if(int.TryParse(amountField.text, out int v) || currentAmount == 0)
        {
            currentAmount = v;
            if(v < minimumAmount) return;
        }
        Application.OpenURL("https://venmo.com/?txn=pay&recipients=" + venmoUsername + "&amount=" + currentAmount + "&note=" + "Note: " + noteField.text);
        emailSender.DonateEmail("Venmo");

        LeaderboardManager.Instance.AddLeaderboardMoney(currentAmount);
    }

    public void PayWithPaypal()
    {
        if(int.TryParse(amountField.text, out int v) || currentAmount == 0)
        {
            currentAmount = v;
            if(v < minimumAmount) return;
        }
        Application.OpenURL("https://paypal.me/" + paypalUsername + "/" + currentAmount.ToString("0.00"));
        emailSender.DonateEmail("Paypal");

        LeaderboardManager.Instance.AddLeaderboardMoney(currentAmount);
    }
}
