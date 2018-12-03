using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datamanager : MonoBehaviour 
{
    public int bestscore;
    public int curscore;
    public int playerhp;
    public int gold;
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
