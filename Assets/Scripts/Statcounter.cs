using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statcounter : MonoBehaviour 
{
    public UILabel hp;
    public UILabel floor;
    public UILabel gold;
    public UILabel power;
    public UILabel armor;
    public UILabel mana;
    public UISprite war;
    public UISprite wiz;
    private void Start()
    {
        switch (PlayerPrefs.GetInt("character"))
        {
            case 1:
                wiz.enabled = false;
                break;
            case 2:
                war.enabled = false;
                break;
        }
    }
    void Update () 
	{
        hp.text = Datamanager.i().curhp + "/" + Datamanager.i().maxhp;
        floor.text = "" + Datamanager.i().stage;
        gold.text = "" + Datamanager.i().gold;
        power.text = "" + Itemmanager.instance().Returnstack("power ring");
        armor.text = "" + Itemmanager.instance().Returnstack("agility ring");
        mana.text = "" + Itemmanager.instance().Returnstack("mana ring");
    }
}