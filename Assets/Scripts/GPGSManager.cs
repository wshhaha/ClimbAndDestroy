using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GPGSManager : MonoBehaviour 
{
    static GPGSManager _instance;
    public static GPGSManager instance()
    {
        return _instance;
    }
	void Start () 
	{
        DontDestroyOnLoad(gameObject);
        if(_instance==null)
        {
            _instance = this;
        }
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
            .Builder()
            .EnableSavedGames()
            .RequestEmail()
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                SceneManager.LoadScene("1_Title");
            }
            else
            {
                print("a");
                //GameObject.Find("Introcnt").GetComponent<Introcnt>().cube.SetActive(true);
            }
        });
    }
    public void SendBoardScore()
    {
        Social.ReportScore(Datamanager.i().curscore, "CgkIqdPV8NIREAIQAg", (bool success) => 
        {
            if (success == true)
            {

            }
            else
            {

            }
        });
    }
    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}