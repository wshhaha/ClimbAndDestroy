﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public bool turn;
    public GameObject h;
    public GameObject gy;
    public GameObject spawner;
    public List<GameObject> elist;
	void Start () 
	{   
        h = GameObject.Find("Hand");
        gy = GameObject.Find("Graveyard");
        spawner = GameObject.Find("Enemyspawner");
        elist.Add(spawner.GetComponent<Enemyspawner>().slot1.gameObject);
        elist.Add(spawner.GetComponent<Enemyspawner>().slot2.gameObject);
        elist.Add(spawner.GetComponent<Enemyspawner>().slot3.gameObject);
        turn = true;
        Datamanager.i().inmaxmana = Datamanager.i().maxmana;
    }
    void Ccdown()
    {
        if (Datamanager.i().w == true)
        {
            Datamanager.i().wnum--;
            if (Datamanager.i().wnum == 0)
            {
                Datamanager.i().w = false;
            }
        }
        if (Datamanager.i().l == true)
        {
            Datamanager.i().lnum--;
            if (Datamanager.i().lnum == 0)
            {
                Datamanager.i().l = false;
            }
        }
        if (Datamanager.i().d == true)
        {
            Datamanager.i().dnum--;
            Datamanager.i().curhp -= 2;
            if (Datamanager.i().curhp <= 0)
            {
                Datamanager.i().curhp = 1;
            }
            if (Datamanager.i().dnum == 0)
            {
                Datamanager.i().w = false;
            }
        }
    }
	public void Turnend()
    {
        Ccdown();
        turn = false;
        Throwcard();
        if (spawner.GetComponent<Enemyspawner>().slot1.gameObject.activeSelf == true)
        {
            spawner.GetComponent<Enemyspawner>().eturn1 = true;
        }
        else
        {
            spawner.GetComponent<Enemyspawner>().eturn2 = true;
        }
        for (int i = 0; i < 3; i++)
        {
            if (elist[i].activeSelf == true)
            {
                elist[i].GetComponent<Enemy>().shd = 0;
            }
        }
        spawner.GetComponent<Enemyspawner>().Enemyturn();
    }
    void Throwcard()
    {
        while (h.GetComponent<Hand>().handlist.Count!=0)
        {
            int i = 0;
            gy.GetComponent<Gyard>().gylist.Add(h.GetComponent<Hand>().handlist[i]);
            h.GetComponent<Hand>().handlist[i].transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
            h.GetComponent<Hand>().handlist[i].GetComponent<UISprite>().depth = 0;
            h.GetComponent<Hand>().handlist[i].transform.localScale = new Vector3(.5f, .5f, .5f);
            h.GetComponent<Hand>().handlist[i].transform.localPosition = Vector3.zero;
            h.GetComponent<Hand>().handlist[i].GetComponent<BoxCollider>().enabled = false;
            h.GetComponent<Hand>().handlist.Remove(h.GetComponent<Hand>().handlist[i]);            
        }
    }
}
