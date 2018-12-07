﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public GameObject spawner;
    public int ehp;
    public bool l;
    public int lnum;
    public bool w;
    public int wnum;
    public bool s;
    public bool d;
    public int dnum;
    public string ename;
    public int maxhp;    
    public int patnum;
    public string pat1;
    public int val1;
    public string pat2;
    public int val2;
    public string pat3;
    public int val3;
    public int tier;

    public void Discount()
    {
        spawner.GetComponent<Enemyspawner>().e--;
        if(spawner.GetComponent<Enemyspawner>().e <= 0)
        {
            spawner.GetComponent<Enemyspawner>().gomap.gameObject.SetActive(true);
            spawner.GetComponent<Enemyspawner>().rewards.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public bool Eaction()
    {
        return false;
    }
}
