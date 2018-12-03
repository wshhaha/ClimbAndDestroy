using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

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
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()        
        .RequestEmail()
        .RequestServerAuthCode(false)
        .RequestIdToken()
        .Build();        
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }		   
}
