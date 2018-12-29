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
    public UISprite character;
    public UIGrid menu;
    public GameObject loadbtn;

    void Start()
    {
        if (PlayerPrefs.GetInt("job") == 0)
        {
            loadbtn.SetActive(false);
            menu.enabled = true;
        }
        Effectsound.instance().bgm.clip = Effectsound.instance().bgmlist[0];
        Effectsound.instance().bgm.Play();
        character.spriteName = null;
        charwindow.SetActive(false);
        blind.SetActive(false);
        waron = false;
        wizon = false;
    }
    public void Loadgame()
    {
        PlayerPrefs.SetInt("character", PlayerPrefs.GetInt("job"));
        switch (PlayerPrefs.GetInt("character"))
        {
            case 1:
                Datamanager.i().maxhp = 120;
                break;
            case 2:
                Datamanager.i().maxhp = 100;
                break;
        }
        Datamanager.i().stage = PlayerPrefs.GetInt("stage");
        Datamanager.i().gold = PlayerPrefs.GetInt("gold");
        Datamanager.i().curhp = PlayerPrefs.GetInt("hp");
        Datamanager.i().curscore = PlayerPrefs.GetInt("score");
        Datamanager.i().maxmana = 3;
        Datamanager.i().save = true;
        Deckmanager.instance().Loaddeck();
        Itemmanager.instance().Loaditem();
        PlayerPrefs.SetInt("job", 0);
        SceneManager.LoadScene(2);
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
            character.spriteName = null;
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
            Datamanager.i().curhp = 120;
            Datamanager.i().maxhp = 120;
            Datamanager.i().gold = 90;
            Datamanager.i().maxmana = 3;
            character.spriteName = "warsel";
        }       
    }
    public void Selwizard()
    {
        wizon = true;
        if (wizon == true)
        {
            waron = false;
            selchar = 2;
            Datamanager.i().curhp = 100;
            Datamanager.i().maxhp = 100;
            Datamanager.i().gold = 120;
            Datamanager.i().maxmana = 3;
            character.spriteName = "wizsel";
        }        
    }
    public void Decide()
    {
        PlayerPrefs.SetInt("character", selchar);
        Deckmanager.instance().Starterdeck();
        Letsclimb();
    }
}