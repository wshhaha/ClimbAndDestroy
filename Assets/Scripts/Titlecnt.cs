using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlecnt : MonoBehaviour 
{
    public GameObject charwindow;
    public GameObject blind;
    public bool callchar = false;
    public bool waron;
    public bool wizon;
    public int selchar;

    void Start()
    {
        charwindow.SetActive(false);
        blind.SetActive(false);
        waron = false;
        wizon = false;
    }
    public void Selcharbtn()
    {
        callchar = !callchar;
        if (callchar == true)
        {
            charwindow.SetActive(true);
            blind.SetActive(true);
        }
        else
        {
            charwindow.SetActive(false);
            blind.SetActive(false);
        }
    }
    public void Letsclimb()
    {
        if(PlayerPrefs.GetInt("character")==1|| PlayerPrefs.GetInt("character") == 2)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("select character");
        }
    }
    public void Exitgame()
    {
        Application.Quit();
    }
    public void Selwarrior()
    {
        waron = true;
        if(waron==true)
        {
            wizon = false;
            selchar = 1;
            Datamanager.instance().curhp = 120;
            Datamanager.instance().maxhp = 120;
            Datamanager.instance().gold = 90;
            Datamanager.instance().maxmana = 3;
        }       
    }
    public void Selwizard()
    {
        wizon = true;
        if (wizon == true)
        {
            waron = false;
            selchar = 2;
            Datamanager.instance().curhp = 100;
            Datamanager.instance().maxhp = 100;
            Datamanager.instance().gold = 120;
            Datamanager.instance().maxmana = 3;
        }        
    }
    public void Decide()
    {
        PlayerPrefs.SetInt("character", selchar);
        Deckmanager.instance().Starterdeck();
        Letsclimb();
    }
}
