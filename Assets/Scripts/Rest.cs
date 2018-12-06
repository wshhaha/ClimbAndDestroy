using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour 
{
    public GameObject gotomap;
    private void Start()
    {
        gotomap.SetActive(false);
    }
    public void Camp()
    {
        Datamanager.i().curhp += (int)(Datamanager.i().maxhp * 0.3f);
        if(Datamanager.i().curhp>= Datamanager.i().maxhp)
        {
            Datamanager.i().curhp = Datamanager.i().maxhp;
        }
        gotomap.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Reinforce()
    {
        gotomap.SetActive(true);
        gameObject.SetActive(false);
    }
}
