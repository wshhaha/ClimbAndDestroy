using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introcnt : MonoBehaviour 
{
	void Start () 
	{        
        PlayerPrefs.SetInt("character", 0);
        SceneManager.LoadScene("1_Title");
	}		
}