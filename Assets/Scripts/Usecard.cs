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
    public UISprite image;
    public UILabel des;
    public GameObject mana;
    public UILabel manalabel;
    
    Vector3 ori;
    private void Start()
    {
        p = GameObject.Find("Player");
        Changevertical(GetComponent<Cardstat>().cname);
        Writedes(GetComponent<Cardstat>().des1);
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
    public void Oninfo()
    {
        if (Application.loadedLevelName != "Battle")
        {
            return;
        }
        p.GetComponent<Player>().cardinfo.GetComponentInChildren<UILabel>().text = des.text;
        p.GetComponent<Player>().cardinfo.SetActive(true);
    }
    public void Offinfo()
    {
        if (Application.loadedLevelName != "Battle")
        {
            return;
        }
        p.GetComponent<Player>().cardinfo.SetActive(false);
    }
    public void Usingcard()
    {
        if (Application.loadedLevelName != "Battle")
        {
            return;
        }
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
        if (Application.loadedLevelName != "Battle")
        {
            return;
        }
        StartCoroutine(Reading());
    }
    IEnumerator Reading()
    {  
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
        ori = psprite.transform.localPosition;
        p.GetComponentInChildren<TweenPosition>().ResetToBeginning();
        p.GetComponentInChildren<TweenPosition>().to = ori + new Vector3(50, 0, 0);
        p.GetComponentInChildren<UITweener>().delay = 0;
        p.GetComponentInChildren<UITweener>().PlayForward();
        yield return new WaitForEndOfFrame();
    }
    void Attack(int val)
    {   
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
                StartCoroutine(Attackmove());
                switch (GetComponent<Cardstat>().cname)
                {
                    case "hit":
                        Effectmanager.i().eftpos = p;
                        Effectmanager.i().Starteft(23);
                        break;
                    case "staff swing":
                        Effectmanager.i().eftpos = p;
                        Effectmanager.i().Starteft(23);
                        break;
                    case "smash":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(18);
                        break;
                    case "stab":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(2);
                        break;
                    case "bash":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(27);
                        break;
                    case "headbutt":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(30);
                        break;
                    case "weapon breaker":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(29);
                        break;
                    case "armor breaker":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(28);
                        break;
                    case "wind cutter":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(24);
                        break;
                    case "ice bolt":
                        Effectmanager.i().eftpos = p;
                        GameObject ice = Effectmanager.i().Starteft(6);
                        ice.GetComponentInChildren<TweenPosition>().to = spawner.GetComponent<Enemyspawner>().target.transform.localPosition + new Vector3(100, 50, 0);
                        ice.GetComponentInChildren<UITweener>().PlayForward();
                        yield return new WaitForSeconds(1);
                        break;
                    case "lighting shock":
                        Effectmanager.i().eftpos = p;
                        GameObject shock = Effectmanager.i().Starteft(7);
                        shock.GetComponentInChildren<TweenPosition>().to = spawner.GetComponent<Enemyspawner>().target.transform.localPosition + new Vector3(100, 50, 0);
                        shock.GetComponentInChildren<UITweener>().PlayForward();
                        yield return new WaitForSeconds(1);
                        break;
                    case "magic arrow":
                        Effectmanager.i().eftpos = p;
                        GameObject arrow = Effectmanager.i().Starteft(26);
                        arrow.GetComponentInChildren<TweenPosition>().to = spawner.GetComponent<Enemyspawner>().target.transform.localPosition + new Vector3(100, 50, 0);
                        arrow.GetComponentInChildren<UITweener>().PlayForward();
                        break;
                    case "lightnova":
                        Effectmanager.i().eftpos = elist[1];
                        Effectmanager.i().Starteft(15);
                        break;
                    case "meteor fall":
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(3);
                        yield return new WaitForSeconds(0.5f);
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        Effectmanager.i().Starteft(16);
                        break;
                }
                Attack(val);
                break;
            case "def":
                Deffence(val);
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(19);
                break;
            case "bringarmor":
                Attack(Datamanager.i().shd);
                Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                Effectmanager.i().Starteft(1);
                break;
            case "allin":
                for (int i = 0; i < Datamanager.i().curmana; i++)
                {
                    Attack(val);
                    Effectmanager.i().eftpos = p;
                    Effectmanager.i().Starteft(23);
                }
                Datamanager.i().curmana = 0;
                break;
            case "genamr":
                Datamanager.i().genamr = true;
                Datamanager.i().gennum = val;
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(17);
                break;
            case "str":
                Datamanager.i().str += val;
                Effectmanager.i().eftpos = p;
                switch (PlayerPrefs.GetInt("character"))
                {
                    case 1:
                        Effectmanager.i().Starteft(10);
                        break;
                    case 2:
                        Effectmanager.i().Starteft(9);
                        break;
                }
                break;
            case "mana":
                Datamanager.i().curmana += val;
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(9);
                break;
            case "heal":
                Datamanager.i().curhp += val;
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(14);
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
                if (GetComponent<Cardstat>().val1==0)
                {
                    break;
                }
                else
                {
                    int j = Random.Range(0, 100);
                    if (j >= 20 && j < 40)
                    {
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().s = true;
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().patstat.text = "stun";
                        spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().p = 3;
                        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                        GameObject stunstar = Effectmanager.i().Starteft(21);
                        stunstar.transform.parent = spawner.GetComponent<Enemyspawner>().target.transform;
                        print("stun sucsses");
                    }
                    else
                    {
                        print("stun fail");
                    }
                    break;
                }
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
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(0);
                break;
            case "bringstr":
                Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
                switch (GetComponent<Cardstat>().cname)
                {
                    case "aura blade":
                        Effectmanager.i().Starteft(12);
                        break;
                    case "powerword kill":
                        Effectmanager.i().Starteft(11);
                        break;
                }
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
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(13);
                break;
            case "dot":
                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().d = true;
                spawner.GetComponent<Enemyspawner>().target.GetComponent<Enemy>().dnum += val;
                break;
            case "reflect":
                Datamanager.i().r = true;
                Datamanager.i().rnum = val;
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(5);
                break;
            case "instant":
                Datamanager.i().ins = true;
                Datamanager.i().insnum = val;
                Datamanager.i().str += Datamanager.i().insnum;
                Effectmanager.i().eftpos = p;
                Effectmanager.i().Starteft(22);
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
        mana.SetActive(false);
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
        Effectmanager.i().eftpos = spawner.GetComponent<Enemyspawner>().target;
        Effectmanager.i().Starteft(8);
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
            if (c >= 97 && c <= 122)
            {
                c = (char)(c - 32);
            }
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
                namelabel.color = Color.cyan;
                break;
            case "l":
                namelabel.color = Color.magenta;
                break;
        }
    }

    public void Writedes(string descript)
    {
        manalabel.text = GetComponent<Cardstat>().mana + "";
        des.text = Changestring(descript);
        switch (GetComponent<Cardstat>().sort)
        {
            case 1:
                image.spriteName = "efta";
                break;
            case 2:
                image.spriteName = "eftb";
                break;
            case 3:
                image.spriteName = "eftc";
                break;
        }
    }
    string Changestring(string a)
    {
        List<string> temp = new List<string>();
        List<int> space = new List<int>();
        int s = 0;
        string t = null;
        for (int i = 0; i < a.Length; i++)
        {
            char c = a[i];
            if (c == ' ')
            {
                space.Add(i);
            }
        }
        space.Add(a.Length);
        for (int i = 0; i < space.Count; i++)
        {
            for (int j = s; j < space[i]; j++)
            {
                t += a[j];
            }
            temp.Add(t);
            t = null;
            s = space[i];
        }
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] == " val1")
            {
                temp[i] = " " + Settextval(GetComponent<Cardstat>().eft1, GetComponent<Cardstat>().val1);
            }
            if (temp[i] == " val2")
            {
                temp[i] = " " + Settextval(GetComponent<Cardstat>().eft2, GetComponent<Cardstat>().val2);
            }
        }
        a = null;
        for (int i = 0; i < temp.Count; i++)
        {
            a += temp[i];
        }
        return a;
    }
    int Settextval(string eft,int val)
    {
        int v = 0;
        switch (eft)
        {
            case "atk":
                if (Datamanager.i().w == true)
                {
                    v = (int)((val + Datamanager.i().str)*0.75f);
                }
                else
                {
                    v = (val + Datamanager.i().str);
                }
                break;
            case "def":
                v = val + Datamanager.i().agi;
                break;
            default:
                v = val;
                break;
        }
        return v;
    }
}