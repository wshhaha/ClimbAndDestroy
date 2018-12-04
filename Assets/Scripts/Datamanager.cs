using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datamanager : MonoBehaviour 
{
    public int bestscore;
    public int curscore;
    public int curhp;
    public int maxhp;
    public int shiled;
    public int gold;
    public int str;
    public int agi;
    public int curmana;
    public int maxmana;
    public int stage;

    static Datamanager _instance;
    public static Datamanager instance()
    {
        return _instance;
    }
	void Start () 
	{
        if(_instance==null)
        {
            _instance = this;
        }
        bestscore = PlayerPrefs.GetInt("bestscore");
        DontDestroyOnLoad(gameObject);
	}	
}
