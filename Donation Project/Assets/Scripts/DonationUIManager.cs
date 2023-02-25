using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DonationUIManager : MonoBehaviour
{
    [SerializeField] GameObject moneyInfoPanel;
    [SerializeField] GameObject computerInfoPanel;
    [SerializeField] GameObject donateButton;
    [SerializeField] TMP_Dropdown donationTypeDropdown;
    public void ChangeDonationType()
    {
        donateButton.SetActive(donationTypeDropdown.value == 0);
        computerInfoPanel.SetActive(donationTypeDropdown.value == 0);
        moneyInfoPanel.SetActive(donationTypeDropdown.value == 1);
    }
}
