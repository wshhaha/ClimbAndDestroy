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
        Datamanager.instance().curhp += (int)(Datamanager.instance().maxhp * 0.3f);
        if(Datamanager.instance().curhp>= Datamanager.instance().maxhp)
        {
            Datamanager.instance().curhp = Datamanager.instance().maxhp;
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
