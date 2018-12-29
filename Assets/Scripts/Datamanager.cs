using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datamanager : MonoBehaviour 
{
    public bool save;
    public int next;
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
    //아래 몬스터 전용
    public bool w;
    public int wnum;
    public bool l;
    public int lnum;
    public bool b;
    public int bnum;
    public bool d;
    public int dnum;

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
