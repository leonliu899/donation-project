using System.Collections;
using System.Collections.Generic;
using CloudLoginUnity;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public int computerAmount;
    public int moneyAmount;
    public Transform parentLeaderboard;
    public GameObject leaderboardPrefab;
    public GameObject note;

    int addedMoney;

    public static LeaderboardManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        note.SetActive(!CloudManager.Instance.signedIn);
    }

    public void Clear()
    {
        CloudLoginUser.CurrentUser.ClearLeaderboardEntries("Computers");
        CloudLoginUser.CurrentUser.ClearLeaderboardEntries("Money");
    }

    public void UpdateLeadboard()
    {
        if(CloudManager.Instance.signedIn)
        {
            foreach (Transform childTransform in parentLeaderboard)
            {
                GameObject.Destroy(childTransform.gameObject);
            }

            CloudLogin.Instance.GetLeaderboard(100, true, "Computers", LeaderboardEntriesRetrievedAll);
        }
    }
    void LeaderboardEntriesRetrievedAll(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error loading leaderboard entries: " + message);
        }
        else
        {
            foreach (CloudLoginLeaderboardEntry entry in CloudLogin.Instance.leaderboardEntries)
            {
                GameObject newEntry = Instantiate(leaderboardPrefab, parentLeaderboard);
                newEntry.transform.GetChild(0).GetComponent<TMP_Text>().text = entry.GetUsername();
                newEntry.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = entry.GetScore() +"";
                CloudLogin.Instance.GetLeaderboard(100, true, "Money", LeaderboardEntriesRetrievedAll2);
            }

        }
    }
    void LeaderboardEntriesRetrievedAll2(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error loading leaderboard entries: " + message);
        }
        else
        {
            foreach (CloudLoginLeaderboardEntry entry in CloudLogin.Instance.leaderboardEntries)
            {
                foreach (Transform childTransform in parentLeaderboard)
                {
                    if(childTransform.GetChild(0).GetComponent<TMP_Text>().text == entry.GetUsername())
                    {
                        childTransform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = "$" + entry.GetScore();
                    }
                }
            
            }

        }
    }


    public void AddLeaderboardComputer()
    {
        if (CloudManager.Instance.signedIn)
        {
            CloudLoginUser.CurrentUser.GetLeaderboard(5, true, ComputerLeaderboardEntriesRetrieved);
        }
    }

    public void AddLeaderboardMoney(int amountAdded)
    {
        if (CloudManager.Instance.signedIn)
        {
            addedMoney = amountAdded;
            CloudLoginUser.CurrentUser.GetLeaderboard(5, true, MoneyLeaderboardEntriesRetrieved);
        }
    }

    void ComputerLeaderboardEntriesRetrieved(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error loading leaderboard entries: " + message);
        }
        else
        {
            foreach (CloudLoginLeaderboardEntry entry in CloudLogin.Instance.leaderboardEntries)
            {
                computerAmount = entry.GetScore();
            }

            CloudLoginUser.CurrentUser.ClearLeaderboardEntries("Computers", ComputersLeaderboardEntryCleared);
        }
    }
    void MoneyLeaderboardEntriesRetrieved(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error loading leaderboard entries: " + message);
        }
        else
        {
            foreach (CloudLoginLeaderboardEntry entry in CloudLogin.Instance.leaderboardEntries)
            {
                moneyAmount = entry.GetScore();
            }

            CloudLoginUser.CurrentUser.ClearLeaderboardEntries("Money", MoneyLeaderboardEntryCleared);
        }
    }
    void ComputersLeaderboardEntryCleared(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error clearing leaderboard: " + message);
        }
        else
        {
            CloudLoginUser.CurrentUser.AddLeaderboardEntry("Computers", computerAmount + 1, LeaderboardEntryAdded);
        }
    }
    void MoneyLeaderboardEntryCleared(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error clearing leaderboard: " + message);
        }
        else
        {
            CloudLoginUser.CurrentUser.AddLeaderboardEntry("Money", moneyAmount + addedMoney, LeaderboardEntryAdded);
        }
    }

    void LeaderboardEntryAdded(string message, bool hasError)
    {
        if (hasError)
        {
            print("Error adding leaderboard entry: " + message);
        }
        else
        {

            print("Set Leaderboard Entry: " + message);
        }
    }
}
