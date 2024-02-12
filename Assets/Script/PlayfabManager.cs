using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;




public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance { set; get; }


    public GameObject RowPrefab;
    public Transform RowsParent;
    public GameObject LeaderboardPanel;
    public GameObject NameWindow;
    public TMP_InputField NameInput;


    public GameObject LockPanel;

    string loggedInPlayfabID;


    private void Awake()
    {
        if (name == null)
        {
            LockPanel.SetActive(true);
        }
        else
        {
            LockPanel.SetActive(false);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        loggedInPlayfabID = result.PlayFabId;
        Debug.Log("Successful login/acccount create");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
            NameWindow.SetActive(true);
        else NameWindow.SetActive(false);

    }


    public void SubmitName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = NameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);


    }


    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated Display Name!");
        NameWindow.SetActive(false);
    }






    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account:");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }


    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }



    public void GetLeadboard()
    {
        LeaderboardPanel.SetActive(true);

        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 6
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }


    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Score",
            MaxResultsCount = 6
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardArondPlayerGet, OnError);

    }

    void OnLeaderboardArondPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in RowsParent)
        {
            Destroy(item.gameObject);
        }


        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(RowPrefab, RowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInPlayfabID)
            {
                texts[0].color = Color.cyan;
                texts[1].color = Color.cyan;
                texts[2].color = Color.cyan;
            }

            Debug.Log(string.Format("PlACE: {0} | ID: {1} | VALUE: {2}",
                item.Position, item.PlayFabId, item.StatValue));
        }
    }



    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in RowsParent)
        {
            Destroy(item.gameObject);
        }


        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(RowPrefab, RowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInPlayfabID)
            {
                texts[0].color = Color.cyan;
                texts[1].color = Color.cyan;
                texts[2].color = Color.cyan;
            }


            Debug.Log(string.Format("PlACE: {0} | ID: {1} | VALUE: {2}",
                item.Position, item.PlayFabId, item.StatValue));
        }
    }


    public void CloseLeaderboardPanel()
    {
        LeaderboardPanel.SetActive(false);
    }

}
