using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public UILabel hplabel;
    public UISlider hpbar;
    public UILabel patstat;
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
    public int p;
    Vector3 ori;
    Player player;
    private void Start()
    {
        ori = transform.localPosition;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Discount()
    {
        switch (tier)
        {
            case 1:
                Datamanager.i().curscore++;
                spawner.GetComponent<Enemyspawner>().goldmin += 10;
                break;
            case 2:
                Datamanager.i().curscore += 5;
                spawner.GetComponent<Enemyspawner>().goldmin += 50;
                break;
            case 3:
                Datamanager.i().curscore += 10;
                spawner.GetComponent<Enemyspawner>().goldmin += 90;
                break;
        }
        spawner.GetComponent<Enemyspawner>().e--;
        if(spawner.GetComponent<Enemyspawner>().e <= 0)
        {   
            spawner.GetComponent<Enemyspawner>().rewards.SetActive(true);
            int rang = Random.Range(spawner.GetComponent<Enemyspawner>().goldmin, spawner.GetComponent<Enemyspawner>().goldmin + 20);
            spawner.GetComponent<Enemyspawner>().rewards.GetComponent<Rewards>().Addreward("gold", rang);
            Rewardcard();
            Rewardt();
        }
        gameObject.SetActive(false);
    }
    public bool Eaction()
    {
        StartCoroutine(Readpat());
        return false;
    }
    public void Hitmove()
    {
        GetComponent<TweenPosition>().ResetToBeginning();
        GetComponent<TweenPosition>().to = ori + new Vector3(50, 0, 0);
        GetComponent<UITweener>().delay = 0.05f;
        GetComponent<UITweener>().PlayForward();
    }
    public void Atkmove()
    {
        GetComponent<TweenPosition>().ResetToBeginning();
        GetComponent<TweenPosition>().to = ori + new Vector3(-50, 0, 0);
        GetComponent<UITweener>().delay = 0;
        GetComponent<UITweener>().PlayForward();
    }
    IEnumerator Readpat()
    {
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
            case 3:
                StartCoroutine(Pateffect("stun", 0));
                print("stun");
                break;
            case 4:
                break;
        }
        yield return new WaitForEndOfFrame();
    }
    public int Returnval(string p,int num)
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
            lockonf = 1.5f;
        }
        else
        {
            lockonf = 1;
        }
        int dam = (int)((num + str) * weakf * lockonf);
        switch (p)
        {
            default:
                return 0;
            case "atk":
                return dam;
            case "carboom":
                return dam;
            case "lifedrain":
                return dam;
            case "deathblade":
                return dam;
            case "def":
                return (num);
        }
    }
    void Attack(int val)
    {   
        Atkmove();
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
            lockonf = 1.5f;
        }
        else
        {
            lockonf = 1;
        }
        int dam= (int)((val + str) * weakf * lockonf);
        Datamanager.i().shd -= dam;
        player.Hitmove();
        if (Datamanager.i().shd < 0)
        {
            Datamanager.i().curhp += Datamanager.i().shd;
            Datamanager.i().shd = 0;
        }
        if (Datamanager.i().curhp <= 0)
        {   
            player.gameover.SetActive(true);
        }
        if (Datamanager.i().r == true)
        {
            shd -= Datamanager.i().rnum;            
            if (shd < 0)
            {
                ehp += shd;
                shd = 0;
            }
            if (ehp < 0)
            {
                Discount();
            }
        }
    }
    IEnumerator Pateffect(string pat,int val)
    {
        switch (pat)
        {
            case "atk":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(0);
                Effectmanager.i().Startsfx(2);
                Attack(val);
                break;
            case "def":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(1);
                Effectmanager.i().Startsfx(22);
                shd += val;
                break;
            case "weak":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(14);
                Effectmanager.i().Startsfx(23);
                Datamanager.i().w = true;
                Datamanager.i().wnum += val;
                break;
            case "lockon":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(15);
                Effectmanager.i().Startsfx(24);
                Datamanager.i().l = true;
                Datamanager.i().lnum += val;
                break;
            case "burnmana":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(2);
                Effectmanager.i().Startsfx(27);
                Datamanager.i().b = true;
                Datamanager.i().bnum += val;
                break;
            case "str":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(3);
                Effectmanager.i().Startsfx(21);
                str += val;
                break;
            case "carboom":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(4);
                Effectmanager.i().Startsfx(28);
                Attack(val);
                ehp = 0;
                Discount();
                break;
            case "bind":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(5);
                Effectmanager.i().Startsfx(26);
                Datamanager.i().str--;
                break;
            case "devour":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(6);
                Effectmanager.i().Startsfx(30);
                Effectmanager.i().Startsfx(35);
                Datamanager.i().gold -= Random.Range(8,13);
                if (Datamanager.i().gold < 0)
                {
                    Datamanager.i().gold = 0;
                }                
                break;
            case "dot":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(13);
                Effectmanager.i().Startsfx(31);
                Datamanager.i().d = true;
                Datamanager.i().dnum += val;
                break;
            case "heal":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(7);
                Effectmanager.i().Startsfx(25);
                ehp += val;
                break;
            case "charm":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(8);
                Effectmanager.i().Startsfx(29);
                Datamanager.i().str--;
                Datamanager.i().agi--;
                break;
            case "lifedrain":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(9);
                Effectmanager.i().Startsfx(32);
                Attack(val);
                ehp += 3;
                break;
            case "summon":
                List<UISprite> elist = new List<UISprite>();
                elist.Add(spawner.GetComponent<Enemyspawner>().slot1);
                elist.Add(spawner.GetComponent<Enemyspawner>().slot2);
                elist.Add(spawner.GetComponent<Enemyspawner>().slot3);
                for (int i = 0; i < 3; i++)
                {
                    if (elist[i].gameObject.activeSelf == false)
                    {
                        Effectmanager.i().eeftpos = elist[i].gameObject;
                        Effectmanager.i().Starteeft(10);
                        Effectmanager.i().Startsfx(33);
                        spawner.GetComponent<Enemyspawner>().Givemstat(elist[i].gameObject, 4);
                        elist[i].GetComponent<TweenPosition>().to = elist[i].gameObject.transform.localPosition;
                        elist[i].GetComponent<Enemy>().p = 4;
                        elist[i].gameObject.SetActive(true);
                        spawner.GetComponent<Enemyspawner>().e++;
                        break;
                    }
                }
                break;
            case "deathblade":
                Effectmanager.i().eeftpos = player.gameObject;
                Effectmanager.i().Starteeft(11);
                Effectmanager.i().Startsfx(34);
                Attack(val);
                Datamanager.i().d = true;
                Datamanager.i().dnum += 2;
                Datamanager.i().w = true;
                Datamanager.i().wnum += 2;
                Datamanager.i().l = true;
                Datamanager.i().lnum += 2;
                break;
            case "stay":
                Effectmanager.i().eeftpos = gameObject;
                Effectmanager.i().Starteeft(12);
                print("stay");
                break;
            case "stun":
                s = false;
                Destroy(GetComponentInChildren<ParticleSystem>().gameObject);
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
    void Rewardcard()
    {
        int ranc = Random.Range(0, 100);
        int cardnum = 0;
        if (ranc >= 0 && ranc < 80)
        {
            cardnum = Random.Range(1, 9);
        }
        if (ranc >= 80 && ranc < 95)
        {
            cardnum = Random.Range(10, 16);
        }
        if (ranc >= 95 && ranc < 100)
        {
            cardnum = Random.Range(16, 20);
        }
        spawner.GetComponent<Enemyspawner>().rewards.GetComponent<Rewards>().Addreward("card", cardnum);
    }
    void Rewardt()
    {
        int rant = Random.Range(0, 100);
        if (rant < 10)
        {
            int lotto = Random.Range(0, 100);
            int num = 0;            
            if (lotto >= 0 && lotto < 90)
            {
                num = Random.Range(0, 2);
            }
            if (lotto >= 90 && lotto < 100)
            {
                num = Random.Range(2, 4);
            }
            spawner.GetComponent<Enemyspawner>().rewards.GetComponent<Rewards>().Addreward("treasure", num);
        }
    }
    private void Update()
    {
        hplabel.text = ehp + " / " + maxhp;
        hpbar.value = (float)ehp / (float)maxhp;
        //if (GetComponent<UITweener>().isActiveAndEnabled == false)
        //{
        //    transform.localPosition = ori;
        //}
    }
}
