using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{   
    public GameObject spawner;
    public int ehp;
    public bool l;
    public int lnum;
    public bool w;
    public int wnum;
    public bool s;
    public bool d;
    public int dnum;
    public string ename;
    public int maxhp;
    public int shd;
    public int patnum;
    public string pat1;
    public int val1;
    public string pat2;
    public int val2;
    public string pat3;
    public int val3;
    public int tier;
    public int str;

    public void Discount()
    {
        spawner.GetComponent<Enemyspawner>().e--;
        if(spawner.GetComponent<Enemyspawner>().e <= 0)
        {
            spawner.GetComponent<Enemyspawner>().gomap.gameObject.SetActive(true);
            spawner.GetComponent<Enemyspawner>().rewards.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public bool Eaction()
    {
        StartCoroutine(Readpat());
        return false;
    }
    IEnumerator Readpat()
    {
        int p = 0;
        switch (ename)
        {
            case "ogre":
                int ran = Random.Range(0, 3);
                if (ran == 2)
                {
                    p = 0;
                }
                else
                {
                    p = 1;
                }
                break;
            //case "necromancer":
            //    p = 0;
            //    for (int i = 0; i < 3; i++)
            //    {
            //        if (spawner.GetComponent<Enemyspawner>().elist[i].activeSelf == true)
            //        {
            //            continue;
            //        }
            //        p = Random.Range(0, patnum);
            //    }
            //    break;
            default:
                p = Random.Range(0, patnum);
                break;
        }        
        switch (p)
        {
            case 0:
                StartCoroutine(Pateffect(pat1, val1));
                print(pat1);
                break;
            case 1:
                StartCoroutine(Pateffect(pat2, val2));
                print(pat2);
                break;
            case 2:
                StartCoroutine(Pateffect(pat3, val3));
                print(pat3);
                break;
        }
        yield return new WaitForEndOfFrame();
    }
    void Attack(int val)
    {
        float weakf = 1.0f;
        if (w == true)
        {
            weakf = .75f;
        }
        else
        {
            weakf = 1;
        }
        float lockonf = 1;
        if (Datamanager.i().l == true)
        {
            lockonf = 1.25f;
        }
        else
        {
            lockonf = 1;
        }
        int dam= (int)((val + str) * weakf * lockonf);
        Datamanager.i().shd -= dam;
        if (Datamanager.i().shd < 0)
        {
            Datamanager.i().curhp += Datamanager.i().shd;
            Datamanager.i().shd = 0;
        }
    }
    IEnumerator Pateffect(string pat,int val)
    {
        switch (pat)
        {
            case "atk":
                Attack(val);
                break;
            case "def":
                shd += val;
                break;
            case "weak":
                Datamanager.i().w = true;
                Datamanager.i().wnum += val;
                break;
            case "lockon":
                Datamanager.i().l = true;
                Datamanager.i().lnum += val;
                break;
            case "burnmana":
                Datamanager.i().b = true;
                Datamanager.i().bnum += val;
                break;
            case "str":
                str += val;
                break;
            case "carboom":
                int c = Random.Range(0, 100);
                print(c + "     !@#");
                if (c < 34)
                {
                    Attack(val);
                    ehp = 0;
                    Discount();
                }
                else
                {
                    StartCoroutine(Readpat());
                    yield break;
                }
                break;
            case "bind":
                Datamanager.i().str--;
                break;
            case "devour":
                Datamanager.i().gold -= 10;
                if (Datamanager.i().gold < 0)
                {
                    Datamanager.i().gold = 0;
                }                
                break;
            case "dot":
                Datamanager.i().d = true;
                Datamanager.i().dnum += val;
                break;
            case "heal":
                ehp += val;
                break;
            case "charm":
                Datamanager.i().str--;
                Datamanager.i().agi--;
                break;
            case "lifedrain":
                Attack(val);
                ehp += 3;
                break;
            case "summon":
                //List<UISprite> elist = new List<UISprite>();
                //elist.Add(spawner.GetComponent<Enemyspawner>().slot1);
                //elist.Add(spawner.GetComponent<Enemyspawner>().slot2);
                //elist.Add(spawner.GetComponent<Enemyspawner>().slot3);
                //for (int i = 0; i < 3; i++)
                //{
                //    if (elist[i].gameObject.activeSelf == false)
                //    {
                //        spawner.GetComponent<Enemyspawner>().Givemstat(elist[i].gameObject, 4);
                //        break;
                //    }
                //}
                break;
            case "deathblade":
                int ran = Random.Range(0, 100);
                print(ran + "     !@#");
                if (ran < 34)
                {
                    Attack(val);
                    Datamanager.i().d = true;
                    Datamanager.i().dnum += 2;
                    Datamanager.i().w = true;
                    Datamanager.i().wnum += 2;
                    Datamanager.i().l = true;
                    Datamanager.i().lnum += 2;
                }
                else
                {
                    StartCoroutine(Readpat());
                    yield break;
                }
                break;
            case "stay":
                print("stay");
                break;
        }
        yield return new WaitForEndOfFrame();
    }
    public void Eccdown()
    {
        if (w == true)
        {
            wnum--;
            if (wnum == 0)
            {
                w = false;
            }
        }
        if (l == true)
        {
            lnum--;
            if (lnum == 0)
            {
                l = false;
            }
        }
        if (d == true)
        {
            dnum--;
            ehp -= 2;
            if (ehp <= 0)
            {
                Discount();
            }
            if (dnum == 0)
            {
                w = false;
            }
        }
    }
}