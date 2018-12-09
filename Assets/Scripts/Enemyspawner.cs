using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Enemyspawner : MonoBehaviour 
{
    public List<GameObject> elist;
    public UISprite gomap;
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
    int tier1num;
    int tier1max;
    int tier2num;
    int tier2max;
    int tier3num;
    int tier3max;
    int nowtier;
    int texttier;
    
    void Start () 
	{   
        uc = false;
        d = GameObject.Find("Deck");
        p = GameObject.Find("Player");
        rewards.SetActive(false);
        gomap.gameObject.SetActive(false);
        slot1.gameObject.SetActive(false);
        slot2.gameObject.SetActive(false);
        slot3.gameObject.SetActive(false);
        eturn1 = false;
        eturn2 = false;
        eturn3 = false;
        elist.Add(slot1.gameObject);
        elist.Add(slot2.gameObject);
        elist.Add(slot3.gameObject);
        Slotnum();
        Givemob();
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
                nowtier = Random.Range(min, max);
                Looktier(nowtier);
                Givemstat(slot1.gameObject, nowtier);
                nowtier = Random.Range(min, max);
                Looktier(nowtier);
                Givemstat(slot2.gameObject, nowtier);
                nowtier = Random.Range(min, max);
                Looktier(nowtier);
                Givemstat(slot3.gameObject, nowtier);
                break;
            case 2:                
                nowtier = Random.Range(min, max);
                Looktier(nowtier);
                Givemstat(slot2.gameObject, nowtier);
                nowtier = Random.Range(min, max);
                Looktier(nowtier);
                Givemstat(slot3.gameObject, nowtier);
                break;
            case 1:                
                nowtier = Random.Range(min, max);
                Looktier(nowtier);
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
    void Looktier(int t)
    {
        int num = 0;
        if (t < 10)
        {
            num = 1;
        }
        if (t < 16)
        {
            num = 2;
        }
        else
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
                    Slotmob(tier1max, tier2max, tier3max);
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
                    Slotmob(tier1max, tier2max, tier3max);
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
                    Slotmob(tier1max, tier2max, tier3max);
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
            print("1");
            eturn2 = slot2.gameObject.GetComponent<Enemy>().Eaction();
            eturn3 = true;
            print("2");
            eturn3 = slot3.gameObject.GetComponent<Enemy>().Eaction();
            print("3");
        }
        if (eturn2 == true)
        {
            eturn2 = slot2.gameObject.GetComponent<Enemy>().Eaction();
            eturn3 = true;
            print("2");
            if (slot3.gameObject.activeSelf == true)
            {
                eturn3 = slot3.gameObject.GetComponent<Enemy>().Eaction();
                print("3");
            }
        }
        p.GetComponent<Player>().turn = true;
        Datamanager.i().curmana = Datamanager.i().inmaxmana;
        Datamanager.i().shd = 0;
        d.GetComponent<Builddeck>().Startturn();
        for (int i = 0; i < 3; i++)
        {
            if (elist[i].activeSelf == true)
            {
                elist[i].gameObject.GetComponent<Enemy>().Eccdown();
            }
        }
        yield return new WaitForEndOfFrame();
    }
    public void Targetlock(GameObject e)
    {
        if (uc == true)
        {
            target = e;
        }
        else
        {
            return;
        }
    }
}