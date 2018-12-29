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
    public void SendBoardScore()
    {
        Social.Active.ReportScore(Datamanager.i().curscore, "CgkIqdPV8NIREAIQAg", (bool success) => 
        {
            if (success == true)
            {
                Datamanager.i().stage = 0;
                Datamanager.i().shd = 0;
                Datamanager.i().str = 0;
                Datamanager.i().agi = 0;
                Datamanager.i().maxmana = 3;
                Datamanager.i().insnum = 0;
                Datamanager.i().ins = false;
                Datamanager.i().genamr = false;
                Datamanager.i().gennum = 0;
                Datamanager.i().r = false;
                Datamanager.i().rnum = 0;
                Datamanager.i().w = false;
                Datamanager.i().wnum = 0;
                Datamanager.i().l = false;
                Datamanager.i().lnum = 0;
                Datamanager.i().d = false;
                Datamanager.i().dnum = 0;
                Datamanager.i().b = false;
                Datamanager.i().bnum = 0;
                Deckmanager.instance().Removedeck();
                Datamanager.i().curscore = 0;
                ShowLeaderBoard();
                Itemmanager.instance().Removeinven();
                SceneManager.LoadScene("1_Title");
            }
            else
            {

            }
        });
    }
    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
}