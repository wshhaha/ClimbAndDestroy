using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usecard : MonoBehaviour 
{
    public GameObject gy;
    public GameObject p;
    public GameObject spawner;
    public List<GameObject> elist;
    public Builddeck deck;
    GameObject h;    

    private void Start()
    {   
        h = GameObject.Find("Hand");
        deck = GameObject.Find("Deck").GetComponent<Builddeck>();
        spawner = GameObject.Find("Enemyspawner");
        elist = new List<GameObject>();
        elist.Add(spawner.GetComponent<Enemyspawner>().slot1.gameObject);
        elist.Add(spawner.GetComponent<Enemyspawner>().slot2.gameObject);
        elist.Add(spawner.GetComponent<Enemyspawner>().slot3.gameObject);
    }
    public void Usingcard()
    {        
        StartCoroutine(Reading());        
    }
    IEnumerator Reading()
    {
        p = GameObject.Find("Player");
        if (p.GetComponent<Player>().turn == false)
        {
            yield break;
        }
        if (Datamanager.i().curmana < GetComponent<Cardstat>().mana)
        {
            yield break;
        }        
        if (GetComponent<Cardstat>().target == true)
        {
            spawner.GetComponent<Enemyspawner>().uc = true;
            while (spawner.GetComponent<Enemyspawner>().target == null)
            {
                yield return null;
            }
        }
        Datamanager.i().curmana -= GetComponent<Cardstat>().mana;        
        StartCoroutine(Cardeffect(GetComponent<Cardstat>().eft1, 1));
        StartCoroutine(Cardeffect(GetComponent<Cardstat>().eft2, 2));
        if (GetComponent<Cardstat>().ex == false)
        {
            StartCoroutine(Gogy());
        }
        else
        {
            StartCoroutine(Extinc());
        }
        h.GetComponentInChildren<UIGrid>().enabled = true;
        h.GetComponent<Hand>().handlist.Remove(gameObject);
        spawner.GetComponent<Enemyspawner>().target = null;
        spawner.GetComponent<Enemyspawner>().uc = false;
    }
    IEnumerator Cardeffect(string eft,int n)
    {
        switch (n)
        {
            case 1:
                switch (eft)
                {
                    case "atk":
                        if (spawner.GetComponent<Enemyspawner>().target == null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (elist[i].activeSelf == true)
                                {
                                    elist[i].GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val1;
                                    if (elist[i].GetComponent<Enemy>().ehp <= 0)
                                    {
                                        elist[i].GetComponent<Enemy>().Discount();
                                    }
                                }
                            }
                        }
                        else
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val1;
                            if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                            {
                                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                            }
                        }
                        break;
                    case "def":
                        Datamanager.i().shd += GetComponent<Cardstat>().val1;
                        break;
                    case "bringarmor":
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= Datamanager.i().shd;
                        if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                        }
                        break;
                    case "allin":
                        for (int i = 0; i < Datamanager.i().curmana; i++)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val1;
                        }
                        if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                        }
                        Datamanager.i().curmana = 0;
                        break;
                    case "genamr":
                        Datamanager.i().genamr = true;
                        Datamanager.i().gennum = GetComponent<Cardstat>().val1;
                        break;
                    case "str":
                        Datamanager.i().str += GetComponent<Cardstat>().val1;
                        break;
                    case "mana":
                        Datamanager.i().curmana += GetComponent<Cardstat>().val1;
                        break;
                    case "heal":
                        Datamanager.i().curhp += GetComponent<Cardstat>().val1;
                        break;
                    case "lockon":
                        if (spawner.GetComponent<Enemyspawner>().target == null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (elist[i].activeSelf == true)
                                {
                                    elist[i].GetComponent<Enemy>().l = true;
                                    elist[i].GetComponent<Enemy>().lnum += GetComponent<Cardstat>().val1;
                                }
                            }
                        }
                        else
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().l = true;
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().lnum += GetComponent<Cardstat>().val1;
                        }
                        break;
                    case "stun":
                        int j = Random.Range(0, 100);
                        if (j >= 20 && j < 40)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().s = true;
                        }
                        break;
                    case "draw":
                        deck.Drawing(GetComponent<Cardstat>().val1);
                        break;
                    case "weak":
                        if (spawner.GetComponent<Enemyspawner>().target == null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (elist[i].activeSelf == true)
                                {
                                    elist[i].GetComponent<Enemy>().w = true;
                                    elist[i].GetComponent<Enemy>().wnum += GetComponent<Cardstat>().val1;
                                }
                            }
                        }
                        else
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().w = true;
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().wnum += GetComponent<Cardstat>().val1;
                        }
                        break;
                    case "rebound":
                        Datamanager.i().curhp -= GetComponent<Cardstat>().val1;
                        break;
                    case "bringstr":
                        for (int i = 0; i < GetComponent<Cardstat>().val1; i++)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= Datamanager.i().str * 5;
                        }
                        if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                        }
                        break;
                    case "random":
                        for (int i = 0; i < Datamanager.i().curmana; i++)
                        {
                            Randomtarget();
                        }
                        Datamanager.i().curmana = 0;
                        break;
                    case "manaup":
                        Datamanager.i().inmaxmana += GetComponent<Cardstat>().val1;
                        break;
                    case "dot":
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().d = true;
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().dnum += GetComponent<Cardstat>().val1;
                        break;
                    case "reflect":
                        Datamanager.i().r = true;
                        Datamanager.i().rnum = GetComponent<Cardstat>().val1;
                        break;
                    case "instant":
                        Datamanager.i().ins = true;
                        Datamanager.i().insnum = GetComponent<Cardstat>().val1;
                        break;
                    case null:
                        break;
                }
                break;
            case 2:
                switch (eft)
                {
                    case "atk":
                        if (spawner.GetComponent<Enemyspawner>().target == null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (elist[i].activeSelf == true)
                                {
                                    elist[i].GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val2;
                                    if (elist[i].GetComponent<Enemy>().ehp <= 0)
                                    {
                                        elist[i].GetComponent<Enemy>().Discount();
                                    }
                                }
                            }
                        }
                        else
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val2;
                            if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                            {
                                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                            }
                        }
                        break;
                    case "def":
                        Datamanager.i().shd += GetComponent<Cardstat>().val2;
                        break;
                    case "bringarmor":
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= Datamanager.i().shd;
                        if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                        }
                        break;
                    case "allin":
                        for (int i = 0; i < Datamanager.i().curmana; i++)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val2;
                        }
                        if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                        }
                        Datamanager.i().curmana = 0;
                        break;
                    case "genamr":
                        Datamanager.i().genamr = true;
                        Datamanager.i().gennum = GetComponent<Cardstat>().val2;
                        break;
                    case "str":
                        Datamanager.i().str += GetComponent<Cardstat>().val2;
                        break;
                    case "mana":
                        Datamanager.i().curmana += GetComponent<Cardstat>().val2;
                        break;
                    case "heal":
                        Datamanager.i().curhp += GetComponent<Cardstat>().val2;
                        break;
                    case "lockon":
                        if (spawner.GetComponent<Enemyspawner>().target == null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (elist[i].activeSelf == true)
                                {
                                    elist[i].GetComponent<Enemy>().l = true;
                                    elist[i].GetComponent<Enemy>().lnum += GetComponent<Cardstat>().val2;
                                }
                            }
                        }
                        else
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().l = true;
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().lnum += GetComponent<Cardstat>().val2;
                        }
                        break;
                    case "stun":
                        int j = Random.Range(0, 100);
                        if (j >= 20 && j < 40)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().s = true;
                        }
                        break;
                    case "draw":
                        deck.Drawing(GetComponent<Cardstat>().val2);
                        break;
                    case "weak":
                        if (spawner.GetComponent<Enemyspawner>().target == null)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (elist[i].activeSelf == true)
                                {
                                    elist[i].GetComponent<Enemy>().w = true;
                                    elist[i].GetComponent<Enemy>().wnum += GetComponent<Cardstat>().val2;
                                }
                            }
                        }
                        else
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().w = true;
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().wnum += GetComponent<Cardstat>().val2;
                        }
                        break;
                    case "rebound":
                        Datamanager.i().curhp -= GetComponent<Cardstat>().val2;
                        break;
                    case "bringstr":
                        for (int i = 0; i < GetComponent<Cardstat>().val2; i++)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp -= Datamanager.i().str * 5;
                        }
                        if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                        {
                            spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                        }
                        break;
                    case "random":
                        for (int i = 0; i < Datamanager.i().curmana; i++)
                        {
                            Randomtarget();
                        }
                        Datamanager.i().curmana = 0;
                        break;
                    case "manaup":
                        Datamanager.i().inmaxmana += GetComponent<Cardstat>().val2;
                        break;
                    case "dot":
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().d = true;
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().dnum += GetComponent<Cardstat>().val2;
                        break;
                    case "reflect":
                        Datamanager.i().r = true;
                        Datamanager.i().rnum = GetComponent<Cardstat>().val2;
                        break;
                    case "instant":
                        Datamanager.i().ins = true;
                        Datamanager.i().insnum = GetComponent<Cardstat>().val2;
                        break;
                    case null:
                        break;
                }
                break;
        }
        yield return new WaitForEndOfFrame();
    }
    IEnumerator Gogy()
    {
        gy.GetComponent<Gyard>().gylist.Add(gameObject);
        transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
        transform.localScale = new Vector3(.5f, .5f, .5f);
        transform.localPosition = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForEndOfFrame();
    }
    IEnumerator Extinc()
    {
        gy.GetComponent<Gyard>().gylist.Add(gameObject);
        transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
        transform.localScale = new Vector3(.5f, .5f, .5f);
        transform.localPosition = Vector3.zero;
        h.GetComponentInChildren<UIGrid>().enabled = true;
        h.GetComponent<Hand>().handlist.Remove(gameObject);
        gy.GetComponent<Gyard>().gylist.Remove(gameObject);
        spawner.GetComponent<Enemyspawner>().target = null;
        Destroy(gameObject);
        yield return new WaitForEndOfFrame();
    }
    void Randomtarget()
    {
        int k = Random.Range(0, 3);
        if (elist[k].activeSelf == false)
        {
            Randomtarget();
            return;
        }
        if (GetComponent<Cardstat>().eft1 == "random")
        {
            elist[k].GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val1;            
        }
        else
        {
            elist[k].GetComponent<Enemy>().ehp -= GetComponent<Cardstat>().val2;            
        }
        if (elist[k].GetComponent<Enemy>().ehp <= 0)
        {
            elist[k].GetComponent<Enemy>().Discount();
        }
    }
}