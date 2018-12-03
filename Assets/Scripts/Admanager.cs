using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Admanager : MonoBehaviour
{    
    private const string android_game_id = "2945966";
    private const string ios_game_id = "";

    private const string rewarded_video_id = "rewardedVideo";
    static Admanager _instance;
    public static Admanager instance()
    {
        return _instance;
    }
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Initialize();
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize()
    {
        #if UNITY_ANDROID
            Advertisement.Initialize(android_game_id);
        #elif UNITY_IOS
            Advertisement.Initialize(ios_game_id);
        #endif
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");                
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}