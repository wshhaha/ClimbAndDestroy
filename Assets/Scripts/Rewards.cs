using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour 
{
    public GameObject btn;
    public List<GameObject> rewardlist;

	public void Takedone()
    {
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
        GameObject.Find("Gotomap").GetComponent<Backtomap>().Callmap();
    }
    public void Addreward(string btnname,int val)
    {
        UIGrid g = GetComponentInChildren<UIGrid>();
        GameObject b = Instantiate(btn);
        b.name = btnname;
        b.GetComponent<Rewardbtninfo>().val = val;
        switch (btnname)
        {
            case "gold":
                b.GetComponentInChildren<UILabel>().text = val + "gold";
                break;
            case "card":
                b.GetComponentInChildren<UILabel>().text = Deckmanager.instance().Returnname(val);
                break;
            case "treasure":
                b.GetComponentInChildren<UILabel>().text = Itemmanager.instance().Returnname(val);
                break;
        }
        b.transform.parent = g.gameObject.transform;
        b.transform.localScale = new Vector3(1, 1, 1);
        g.enabled = true;
        rewardlist.Add(b);
    }
}
