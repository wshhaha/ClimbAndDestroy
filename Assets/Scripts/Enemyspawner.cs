using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Enemyspawner : MonoBehaviour 
{
    public List<GameObject> elist;
    public GameObject rewards;
    public UISprite slot1;
    public UISprite slot2;
    public UISprite slot3;
    public GameObject p;
    public GameObject d;
    public int e;
    public bool eturn1;
    public bool eturn2;
    public bool eturn3;
    public GameObject target;
    public bool uc;
    public TextAsset moblist;
    int min;
    int max;
    public int tier1num;
    public int tier1max;
    public int tier2num;
    public int tier2max;
    public int tier3num;
    public int tier3max;
    public int nowtier;
    public int goldmin;
    public GameObject nextturn;

    void Start () 
	{   
        uc = false;
        d = GameObject.Find("Deck");
        p = GameObject.Find("Player");
        rewards.SetActive(false);
        slot1.gameObject.SetActive(false);
        slot2.gameObject.SetActive(false);
        slot3.gameObject.SetActive(false);
        eturn1 = false;
        eturn2 = false;
        eturn3 = false;
        elist.Add(slot1.gameObject);
        elist.Add(slot2.gameObject);
        elist.Add(slot3.gameObject);        
        Givemob();
        Slotnum();
        Selepat();
    }
   
    public void Givemob()
    {
        e = Random.Range(1, 4);
        if (e == 3 && Datamanager.i().stage <= 5)
        {
            Givemob();
            return;
        }
        int j = Datamanager.i().stage / 10;
        if (j > 9)
        {
            j = 9;
        }
        switch (j)
        {
            case 0:
                min = 0;
                max = 10;
                tier1max = 3;
                tier2max = 0;
                tier3max = 0;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 1:
                min = 0;
                max = 16;
                tier1max = 3;
                tier2max = 1;
                tier3max = 0;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 2:
                min = 0;
                max = 16;
                tier1max = 3;
                tier2max = 2;
                tier3max = 0;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 3:
                min = 0;
                max = 16;
                tier1max = 2;
                tier2max = 3;
                tier3max = 0;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 4:
                min = 0;
                max = 20;
                tier1max = 1;
                tier2max = 3;
                tier3max = 1;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 5:
                min = 0;
                max = 20;
                tier1max = 0;
                tier2max = 3;
                tier3max = 2;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 6:
                min = 0;
                max = 20;
                tier1max = 0;
                tier2max = 3;
                tier3max = 3;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 7:
                min = 0;
                max = 20;
                tier1max = 0;
                tier2max = 2;
                tier3max = 3;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 8:
                min = 0;
                max = 20;
                tier1max = 0;
                tier2max = 1;
                tier3max = 3;
                Slotmob(tier1max, tier2max, tier3max);
                break;
            case 9:
                min = 0;
                max = 20;
                tier1max = 0;
                tier2max = 0;
                tier3max = 3;
                Slotmob(tier1max, tier2max, tier3max);
                break;
        }
    }
    public void Slotmob(int max1, int max2, int max3)
    {
        switch (e)
        {
            case 3:
                Looktier(min, max);
                Givemstat(slot1.gameObject, nowtier);
                Looktier(min, max);
                Givemstat(slot2.gameObject, nowtier);
                Looktier(min, max);
                Givemstat(slot3.gameObject, nowtier);
                break;
            case 2:
                Looktier(min, max);
                Givemstat(slot2.gameObject, nowtier);
                Looktier(min, max);
                Givemstat(slot3.gameObject, nowtier);
                break;
            case 1:
                Looktier(min, max);
                Givemstat(slot2.gameObject, nowtier);
                break;
        }
    }
    public void Slotnum()
    {   
        switch (e)
        {
            case 3:
                slot1.gameObject.SetActive(true);
                slot2.gameObject.SetActive(true);
                slot3.gameObject.SetActive(true);                
                break;
            case 2:
                slot1.gameObject.SetActive(false);
                slot2.gameObject.SetActive(true);
                slot3.gameObject.SetActive(true);                
                break;
            case 1:
                slot1.gameObject.SetActive(false);
                slot2.gameObject.SetActive(true);
                slot3.gameObject.SetActive(false);                
                break;
        }        
    }
    void Looktier(int min,int max)
    {
        int t = Random.Range(min, max);
        nowtier = t;
        int num = 0;
        if (t >= 0 && t < 10)
        {
            num = 1;
        }
        if (t >= 10 && t < 16)
        {
            num = 2;
        }
        if (t >= 16 && t < 20)
        {
            num = 3;
        }
        switch (num)
        {
            case 1:
                if (tier1num < tier1max)
                {
                    tier1num++;
                }
                else
                {
                    Looktier(min, max);
                    return;
                }
                break;
            case 2:
                if (tier2num < tier2max)
                {
                    tier2num++;
                }
                else
                {
                    Looktier(min, max);
                    return;
                }
                break;
            case 3:
                if (tier3num < tier3max)
                {
                    tier3num++;
                }
                else
                {
                    Looktier(min, max);
                    return;
                }
                break;
        }
    }
    public void Givemstat(GameObject slot,int num)
    {
        var mob1 = JSON.Parse(moblist.text);
        slot.GetComponent<Enemy>().ename = mob1[num]["name"];
        slot.gameObject.name = mob1[num]["name"];
        slot.GetComponent<Enemy>().maxhp = Random.Range(mob1[num]["hpmin"], mob1[num]["hpmax"]);
        slot.GetComponent<Enemy>().ehp = slot.GetComponent<Enemy>().maxhp;
        slot.GetComponent<Enemy>().patnum = mob1[num]["patnum"];
        slot.GetComponent<Enemy>().pat1 = mob1[num]["pat1"];
        slot.GetComponent<Enemy>().pat2 = mob1[num]["pat2"];
        slot.GetComponent<Enemy>().pat3 = mob1[num]["pat3"];
        slot.GetComponent<Enemy>().val1 = mob1[num]["val1"];
        slot.GetComponent<Enemy>().val2 = mob1[num]["val2"];
        slot.GetComponent<Enemy>().val3 = mob1[num]["val3"];
        slot.GetComponent<Enemy>().tier = mob1[num]["tier"];
    }
    public void Enemyturn()
    {
        StartCoroutine(Epatten());
    }
    IEnumerator Epatten()
    {
        if (eturn1 == true)
        {
            eturn1 = slot1.gameObject.GetComponent<Enemy>().Eaction();
            eturn2 = true;
            eturn2 = slot2.gameObject.GetComponent<Enemy>().Eaction();
            eturn3 = true;
            eturn3 = slot3.gameObject.GetComponent<Enemy>().Eaction();
        }
        if (eturn2 == true)
        {
            eturn2 = slot2.gameObject.GetComponent<Enemy>().Eaction();
            eturn3 = true;
            if (slot3.gameObject.activeSelf == true)
            {
                eturn3 = slot3.gameObject.GetComponent<Enemy>().Eaction();
            }
        }
        p.GetComponent<Player>().turn = true;
        Datamanager.i().curmana = Datamanager.i().inmaxmana;
        Datamanager.i().shd = 0;
        d.GetComponent<Builddeck>().St();
        yield return new WaitForSecondsRealtime(0.5f);
        Selepat();
        nextturn.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        nextturn.SetActive(false);
        //yield return new WaitForEndOfFrame();
    }
    public void Targetlock(GameObject e)
    {
        if (p.GetComponent<Player>().uc == true)
        {
            target = e;
        }
        else
        {
            target = null;
        }
        if (p.GetComponent<Player>().readycard != null)
        {   
            p.GetComponent<Player>().readycard.GetComponent<Usecard>().Startread();
        }
    }
    public void Targetunlock()
    {
        StartCoroutine(unlock());
    }
    IEnumerator unlock()
    {
        target = null;
        yield return new WaitForEndOfFrame();
    }
    void Selepat()
    {
        for (int i = 0; i < 3; i++)
        {
            if (elist[i].activeSelf == true)
            {
                elist[i].gameObject.GetComponent<Enemy>().Eccdown();
                switch (elist[i].gameObject.GetComponent<Enemy>().ename)
                {
                    case "ogre":
                        int ran = Random.Range(0, 3);
                        if (ran == 2)
                        {
                            elist[i].gameObject.GetComponent<Enemy>().p = 0;
                        }
                        else
                        {
                            elist[i].gameObject.GetComponent<Enemy>().p = 1;
                        }
                        break;
                    case "necromancer":
                        elist[i].gameObject.GetComponent<Enemy>().p = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            if (elist[k].activeSelf == true)
                            {
                                continue;
                            }
                            elist[k].gameObject.GetComponent<Enemy>().p = Random.Range(0, elist[k].gameObject.GetComponent<Enemy>().patnum);
                        }
                        break;
                    default:
                        elist[i].gameObject.GetComponent<Enemy>().p = Random.Range(0, elist[i].gameObject.GetComponent<Enemy>().patnum);
                        break;
                }
                switch (elist[i].gameObject.GetComponent<Enemy>().p)
                {
                    case 0:
                        if (elist[i].gameObject.GetComponent<Enemy>().Returnval(elist[i].gameObject.GetComponent<Enemy>().pat1 , elist[i].gameObject.GetComponent<Enemy>().val1) == 0)
                        {
                            elist[i].gameObject.GetComponent<Enemy>().patstat.text = elist[i].gameObject.GetComponent<Enemy>().pat1;
                        }
                        else
                        {
                            elist[i].gameObject.GetComponent<Enemy>().patstat.text = elist[i].gameObject.GetComponent<Enemy>().pat1 + elist[i].gameObject.GetComponent<Enemy>().Returnval(elist[i].gameObject.GetComponent<Enemy>().pat1, elist[i].gameObject.GetComponent<Enemy>().val1);
                        }
                        break;
                    case 1:
                        if (elist[i].gameObject.GetComponent<Enemy>().Returnval(elist[i].gameObject.GetComponent<Enemy>().pat2, elist[i].gameObject.GetComponent<Enemy>().val2) == 0)
                        {
                            elist[i].gameObject.GetComponent<Enemy>().patstat.text = elist[i].gameObject.GetComponent<Enemy>().pat2;
                        }
                        else
                        {
                            elist[i].gameObject.GetComponent<Enemy>().patstat.text = elist[i].gameObject.GetComponent<Enemy>().pat2 + elist[i].gameObject.GetComponent<Enemy>().Returnval(elist[i].gameObject.GetComponent<Enemy>().pat2, elist[i].gameObject.GetComponent<Enemy>().val2);
                        }
                        break;
                    case 2:
                        if (elist[i].gameObject.GetComponent<Enemy>().Returnval(elist[i].gameObject.GetComponent<Enemy>().pat3, elist[i].gameObject.GetComponent<Enemy>().val3) == 0)
                        {
                            elist[i].gameObject.GetComponent<Enemy>().patstat.text = elist[i].gameObject.GetComponent<Enemy>().pat3;
                        }
                        else
                        {
                            elist[i].gameObject.GetComponent<Enemy>().patstat.text = elist[i].gameObject.GetComponent<Enemy>().pat3 + elist[i].gameObject.GetComponent<Enemy>().Returnval(elist[i].gameObject.GetComponent<Enemy>().pat3, elist[i].gameObject.GetComponent<Enemy>().val3);
                        }
                        break;
                }
            }
        }
    }
}