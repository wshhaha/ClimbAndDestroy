using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{
    public bool turn;
    public bool uc;
    public GameObject h;
    public GameObject gy;
    public GameObject spawner;
    public List<GameObject> elist;
    public GameObject gameover;
    public UILabel hplabel;
    public UISlider hpbar;
    public UILabel manalabel;
    public GameObject readycard;
    public bool te;

	void Start () 
	{
        te = false;
        uc = false;
        if (Itemmanager.instance().inven.Count != 0)
        {
            for (int i = 0; i < Itemmanager.instance().inven.Count; i++)
            {
                string e = Itemmanager.instance().inven[i].GetComponent<Iteminfo>().eft;
                switch (e)
                {
                    case "str":
                        Datamanager.i().str++;
                        break;
                    case "agi":
                        Datamanager.i().agi++;
                        break;
                    case "maxmana":
                        Datamanager.i().maxmana++;
                        break;
                    case "maxhp":
                        break;
                }
            }
        }
        gameover.SetActive(false);
        h = GameObject.Find("Hand");
        gy = GameObject.Find("Graveyard");
        spawner = GameObject.Find("Enemyspawner");
        elist.Add(spawner.GetComponent<Enemyspawner>().slot1.gameObject);
        elist.Add(spawner.GetComponent<Enemyspawner>().slot2.gameObject);
        elist.Add(spawner.GetComponent<Enemyspawner>().slot3.gameObject);
        turn = true;
        Datamanager.i().inmaxmana = Datamanager.i().maxmana;
    }
    void Ccdown()
    {
        if (Datamanager.i().genamr == true)
        {
            Datamanager.i().shd += Datamanager.i().gennum;
        }
        if (Datamanager.i().r == true)
        {
            Datamanager.i().r = false;
        }
        if (Datamanager.i().w == true)
        {
            Datamanager.i().wnum--;
            if (Datamanager.i().wnum == 0)
            {
                Datamanager.i().w = false;
            }
        }
        if (Datamanager.i().l == true)
        {
            Datamanager.i().lnum--;
            if (Datamanager.i().lnum == 0)
            {
                Datamanager.i().l = false;
            }
        }
        if (Datamanager.i().d == true)
        {
            Datamanager.i().dnum--;
            Datamanager.i().curhp -= 2;
            if (Datamanager.i().curhp <= 0)
            {
                Datamanager.i().curhp = 1;
            }
            if (Datamanager.i().dnum == 0)
            {
                Datamanager.i().w = false;
            }
        }
    }
	public void Turnend()
    {
        if (te == true)
        {
            return;
        }
        te = true;
        Ccdown();
        turn = false;
        Throwcard();
        if (spawner.GetComponent<Enemyspawner>().slot1.gameObject.activeSelf == true)
        {
            spawner.GetComponent<Enemyspawner>().eturn1 = true;
        }
        else
        {
            spawner.GetComponent<Enemyspawner>().eturn2 = true;
        }
        for (int i = 0; i < 3; i++)
        {
            if (elist[i].activeSelf == true)
            {
                elist[i].GetComponent<Enemy>().shd = 0;
            }
        }
        spawner.GetComponent<Enemyspawner>().Enemyturn();
    }
    void Throwcard()
    {
        int j = h.GetComponent<Hand>().handlist.Count;
        for (int i = 0; i < j; i++)
        {
            gy.GetComponent<Gyard>().gylist.Add(h.GetComponent<Hand>().handlist[i]);
            h.GetComponent<Hand>().handlist[i].transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
            h.GetComponent<Hand>().handlist[i].GetComponent<UIPanel>().depth = 2;
            h.GetComponent<Hand>().handlist[i].GetComponent<Usecard>().Gymoving();
        }
        h.GetComponent<Hand>().handlist.RemoveRange(0, j);
    }
    public void Gototitle()
    {   
        Datamanager.i().stage = 0;
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
        Deckmanager.instance().Removedeck();
        SceneManager.LoadScene("1_Title");
        Datamanager.i().curscore = 0;
    }
    private void Update()
    {
        hplabel.text = Datamanager.i().curhp + " / " + Datamanager.i().maxhp;
        hpbar.value = ((float)Datamanager.i().curhp / (float)Datamanager.i().maxhp);
        gameover.GetComponentInChildren<UILabel>().text = "YOU DIED\nYOUR SCORE\n" + Datamanager.i().curscore;
        manalabel.text= Datamanager.i().curmana + "  /  " + Datamanager.i().inmaxmana;
    }
}
