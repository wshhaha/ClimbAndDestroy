using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statcounter : MonoBehaviour 
{	
	void Update () 
	{
        GetComponent<UILabel>().text = "HP " + Datamanager.i().curhp + "/" + Datamanager.i().maxhp + 
            "   Floor " + Datamanager.i().stage + "   Gold " + Datamanager.i().gold +
            "   P " + Itemmanager.instance().Returnstack("power ring") + "   A "+ Itemmanager.instance().Returnstack("agility ring")
            + "   M " + Itemmanager.instance().Returnstack("mana ring");
    }
}