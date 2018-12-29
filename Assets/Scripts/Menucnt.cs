using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menucnt : MonoBehaviour 
{
    public GameObject menu;
	public void Callmenu()
    {
        menu.SetActive(true);
    }
    public void Resume()
    {
        menu.SetActive(false);
    }
    public void Save()
    {
        PlayerPrefs.SetInt("stage", Datamanager.i().stage);
        PlayerPrefs.SetInt("gold", Datamanager.i().gold);
        PlayerPrefs.SetInt("hp", Datamanager.i().curhp);
        PlayerPrefs.SetInt("score", Datamanager.i().curscore);
        PlayerPrefs.SetInt("job", PlayerPrefs.GetInt("character"));
        Datamanager.i().next = GameObject.Find("Nextstair").GetComponent<Nextstair>().j;
        PlayerPrefs.SetString("deck", GameObject.Find("Nextstair").GetComponent<Nextstair>().Convertcard());
        PlayerPrefs.SetString("item", Itemmanager.instance().Convertitem());
        Application.Quit();
    }
    public void Gototitle()
    {
        PlayerPrefs.SetInt("character", 0);
        PlayerPrefs.SetInt("job", 0);
        Datamanager.i().stage = 0;
        Datamanager.i().shd = 0;
        Datamanager.i().str = 0;
        Datamanager.i().agi = 0;
        Datamanager.i().maxmana = 3;
        Datamanager.i().insnum = 0;
        Datamanager.i().ins = false;
        Datamanager.i().genamr = false;
        Datamanager.i().gennum = 0;
        Datamanager.i().r = false;
        Datamanager.i().rnum = 0;
        Datamanager.i().w = false;
        Datamanager.i().wnum = 0;
        Datamanager.i().l = false;
        Datamanager.i().lnum = 0;
        Datamanager.i().d = false;
        Datamanager.i().dnum = 0;
        Datamanager.i().b = false;
        Datamanager.i().bnum = 0;
        Deckmanager.instance().Removedeck();
        SceneManager.LoadScene("1_Title");
        Datamanager.i().curscore = 0;
    }
    public void Exitgame()
    {
        Application.Quit();
    }
}