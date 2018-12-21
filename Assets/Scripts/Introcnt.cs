using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introcnt : MonoBehaviour 
{
    public GameObject gpgs;
    public GameObject cube;
	void Start () 
	{        
        PlayerPrefs.SetInt("character", 0);
        gpgs.GetComponent<GPGSManager>().Login();
        //SceneManager.LoadScene("1_Title");
	}		
}