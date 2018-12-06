﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datamanager : MonoBehaviour 
{
    public int bestscore;
    public int curscore;
    public int curhp;
    public int maxhp;
    public int shd;
    public int gold;
    public int str;
    public int agi;
    public int curmana;
    public int maxmana;
    public int inmaxmana;
    public int stage;
    public bool ins;
    public int insnum;
    public bool genamr;
    public int gennum;
    public bool r;
    public int rnum;

    static Datamanager _instance;
    public static Datamanager i()
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
