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
    
    private void Start()
    {
        
    }
    void Update () 
	{
        hp.text = Datamanager.i().curhp + "/" + Datamanager.i().maxhp;
        floor.text = "" + Datamanager.i().stage;
        gold.text = "" + Datamanager.i().gold;
        power.text = "" + Itemmanager.instance().Returnstack("Power ring");
        armor.text = "" + Itemmanager.instance().Returnstack("Orichalcon");
        mana.text = "" + Itemmanager.instance().Returnstack("Mana ring");
    }
}