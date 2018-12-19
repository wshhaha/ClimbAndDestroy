using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Usecard : MonoBehaviour 
{
    public GameObject gy;
    public GameObject p;
    public GameObject spawner;
    public List<GameObject> elist;
    public Builddeck deck;
    GameObject h;
    public Rest rest;
    public UISprite back;
    public UILabel namelabel;
    public UILabel goldlabel;
    public UISprite psprite;

    private void Start()
    {
        p = GameObject.Find("Player");
        Changevertical(GetComponent<Cardstat>().cname);
        switch (Application.loadedLevelName)
        {
            case "Battle":
                h = GameObject.Find("Hand");
                deck = GameObject.Find("Deck").GetComponent<Builddeck>();
                spawner = GameObject.Find("Enemyspawner");
                elist = new List<GameObject>();
                elist.Add(spawner.GetComponent<Enemyspawner>().slot1.gameObject);
                elist.Add(spawner.GetComponent<Enemyspawner>().slot2.gameObject);
                elist.Add(spawner.GetComponent<Enemyspawner>().slot3.gameObject);
                psprite = GameObject.Find("Character").GetComponent<UISprite>();
                break;
        }
    }
    public void Usingcard()
    {
        p.GetComponent<Player>().uc = true;
        if (GetComponent<Cardstat>().target == true)
        {
            p.GetComponent<Player>().readycard = gameObject;
        }
        else
        {
            p.GetComponent<Player>().readycard = null;
            Startread();
        }
    }
    public void Startread()
    {
        StartCoroutine(Reading());
    }
    IEnumerator Reading()
    {   
        if (Application.loadedLevelName != "Battle")
        {
            yield break;
        }
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
            while (spawner.GetComponent<Enemyspawner>().target == null)
            {
                yield return null;
            }
        }
        Datamanager.i().curmana -= GetComponent<Cardstat>().mana;
        yield return StartCoroutine(Cardeffect(GetComponent<Cardstat>().eft1, GetComponent<Cardstat>().val1));
        yield return StartCoroutine(Cardeffect(GetComponent<Cardstat>().eft2, GetComponent<Cardstat>().val2));
        spawner.GetComponent<Enemyspawner>().Targetunlock();
        gy.GetComponent<Gyard>().gylist.Add(gameObject);
        transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
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
        p.GetComponent<Player>().uc = false;
    }
    IEnumerator Attackmove()
    {
        if(GetComponent<Cardstat>().cname== "forked lighting")
        {
            yield break;
        }
        Vector3 ori = psprite.transform.localPosition;
        for (int i = 1; i < 6; i++)
        {
            psprite.transform.localPosition = ori + new Vector3(i * 10, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        psprite.transform.localPosition = ori;
    }
    void Attack(int val)
    {
        StartCoroutine(Attackmove());
        float weakf = 1.0f;
        if (Datamanager.i().w == true)
        {
            weakf = .75f;
        }
        else
        {
            weakf = 1;
        }
        float lockonf = 1;
        
        int dam = (val + Datamanager.i().str);
        if (dam < 0)
        {
            dam = 0;
        }
        if (spawner.GetComponent<Enemyspawner>().target == null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (elist[i].activeSelf == true)
                {
                    if (elist[i].GetComponent<Enemy>().l == true)
                    {
                        lockonf = 1.5f;
                    }
                    else
                    {
                        lockonf = 1;
                    }
                    dam = (int)(dam * weakf * lockonf);
                    print(dam);
                    elist[i].GetComponent<Enemy>().shd -= dam;
                    if (elist[i].GetComponent<Enemy>().shd < 0)
                    {
                        elist[i].GetComponent<Enemy>().ehp += elist[i].GetComponent<Enemy>().shd;
                        elist[i].GetComponent<Enemy>().shd = 0;
                    }
                    elist[i].GetComponent<Enemy>().Hitmove();
                    if (elist[i].GetComponent<Enemy>().ehp <= 0)
                    {
                        elist[i].GetComponent<Enemy>().Discount();
                    }
                }
            }
        }
        else
        {
            if (spawner.GetComponent<Enemyspawner>().target.activeSelf == true)
            {
                if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().l == true)
                {
                    lockonf = 1.5f;
                }
                else
                {
                    lockonf = 1;
                }
                dam = (int)(dam * weakf * lockonf);
                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().shd -= dam;
                if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().shd < 0)
                {
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp += spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().shd;
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().shd = 0;
                }
                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Hitmove();
                if (spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().ehp <= 0)
                {
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().Discount();
                }
            }
        }
    }
    void Deffence(int val)
    {
        int dam = val + Datamanager.i().agi;
        if (dam < 0)
        {
            dam = 0;
        }
        Datamanager.i().shd += dam;
    }
    IEnumerator Cardeffect(string eft,int val)
    {
        switch (eft)
        {
            case "atk":
                Attack(val);
                break;
            case "def":
                Deffence(val);
                break;
            case "bringarmor":
                Attack(Datamanager.i().shd);
                break;
            case "allin":
                for (int i = 0; i < Datamanager.i().curmana; i++)
                {
                    Attack(val);
                }
                Datamanager.i().curmana = 0;
                break;
            case "genamr":
                Datamanager.i().genamr = true;
                Datamanager.i().gennum = val;
                break;
            case "str":
                Datamanager.i().str += val;
                break;
            case "mana":
                Datamanager.i().curmana += val;
                break;
            case "heal":
                Datamanager.i().curhp += val;
                break;
            case "lockon":
                if (spawner.GetComponent<Enemyspawner>().target == null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (elist[i].activeSelf == true)
                        {
                            elist[i].GetComponent<Enemy>().l = true;
                            elist[i].GetComponent<Enemy>().lnum += val;
                        }
                    }
                }
                else
                {
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().l = true;
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().lnum += val;
                }
                break;
            case "stun":
                int j = Random.Range(0, 100);
                if (j >= 20 && j < 40)
                {
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().s = true;
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().patstat.text = "stun";
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().p = 3;
                    print("stun sucsses");
                }
                else
                {
                    print("stun fail");
                }
                break;
            case "draw":
                deck.Drawing(val);
                break;
            case "weak":
                if (spawner.GetComponent<Enemyspawner>().target == null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (elist[i].activeSelf == true)
                        {
                            elist[i].GetComponent<Enemy>().w = true;
                            elist[i].GetComponent<Enemy>().wnum += val;
                        }
                    }
                }
                else
                {
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().w = true;
                    spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().wnum += val;
                }
                break;
            case "rebound":
                Datamanager.i().curhp -= val;
                break;
            case "bringstr":
                for (int i = 0; i < val; i++)
                {
                   Attack((Datamanager.i().str + 1) * 5);
                }
                break;
            case "random":
                for (int i = 0; i < Datamanager.i().curmana; i++)
                {
                    Randomtarget(val);
                }
                Datamanager.i().curmana = 0;
                break;
            case "manaup":
                Datamanager.i().inmaxmana += val;
                break;
            case "dot":
                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().d = true;
                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().dnum += val;
                break;
            case "reflect":
                Datamanager.i().r = true;
                Datamanager.i().rnum = val;
                break;
            case "instant":
                Datamanager.i().ins = true;
                Datamanager.i().insnum = val;
                break;
            case null:
                break;
        }
        yield return new WaitForEndOfFrame();
    }
    public void Gymoving()
    {
        StartCoroutine(Gogy());
    }
    IEnumerator Gogy()
    {
        back.enabled = true;
        transform.localScale = new Vector3(.5f, .5f, .5f);
        transform.Rotate(0, 0, -90);
        Vector3 ori = new Vector3(1.5f, -0.7f, 0);
        float factor = 0;
        while (factor < 1)
        {
            transform.position = new Vector3(ori.x * factor, ori.y, ori.z);
            factor += 0.2f;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = Vector3.zero;
        transform.Rotate(0, 0, 90);
        GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponent<UIPanel>().depth = 2;
        spawner.GetComponent<Enemyspawner>().target = null;
    }
    IEnumerator Extinc()
    {   
        h.GetComponentInChildren<UIGrid>().enabled = true;
        h.GetComponent<Hand>().handlist.Remove(gameObject);
        gy.GetComponent<Gyard>().gylist.Remove(gameObject);
        Destroy(gameObject);
        yield return new WaitForEndOfFrame();
    }
    void Randomtarget(int num)
    {
        int k = Random.Range(0, elist.Count);
        if (elist[k].activeSelf == false)
        {
            Randomtarget(num);
            return;
        }
        else
        {
            spawner.GetComponent<Enemyspawner>().target = elist[k];
        }
        Attack(num);
        spawner.GetComponent<Enemyspawner>().target = null;
    }

    public void Upgrade(GameObject card)
    {
        if (Application.loadedLevelName != "Rest")
        {
            return;
        }
        rest = GameObject.Find("Rest").GetComponent<Rest>();
        rest.yesno.SetActive(true);
        rest.target = card;
    }

    public void Buycard(GameObject card)
    {
        if (Application.loadedLevelName != "Store")
        {
            return;
        }
        if (card.GetComponent<Cardstat>().gold > Datamanager.i().gold)
        {
            return;
        }
        card.transform.parent = Deckmanager.instance().gameObject.transform;
        Deckmanager.instance().orideck.Add(card);
        card.transform.localScale = new Vector3(1, 1, 1);
        card.transform.localPosition = Vector3.zero;
        Datamanager.i().gold -= card.GetComponent<Cardstat>().gold;
        goldlabel.enabled = false;
        card.SetActive(false);
    }

    void Changevertical(string cardname)
    {
        string temp = null;
        string newname = null;
        for (int i = 0; i < cardname.Length; i++)
        {
            char c = cardname[i];
            c = (char)(c - 32);
            if (i < cardname.Length - 1)
            {
                temp = c + "\n";
            }
            else
            {
                temp = c + "";
            }
            newname += temp;
        }
        namelabel.text = newname;
        switch (GetComponent<Cardstat>().grade)
        {
            case "n":
                namelabel.color = Color.white;
                break;
            case "u":
                namelabel.color = Color.blue;
                break;
            case "l":
                namelabel.color = Color.magenta;
                break;
        }
    }
}