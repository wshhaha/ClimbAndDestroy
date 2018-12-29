using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Introcnt : MonoBehaviour 
{   
	void Start () 
	{
        Time.timeScale = 2;
        PlayerPrefs.SetInt("character", 0);
        SceneManager.LoadScene("1_Title");
    }
}