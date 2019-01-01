using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Deckmanager : MonoBehaviour 
{
    public GameObject card;
    public List<GameObject> orideck;
    public TextAsset warriordeck;
    public TextAsset wizarddeck;
    static Deckmanager _instance;
    public static Deckmanager instance()
    {
        return _instance;
    }

	void Start () 
	{
        if (_instance == null)
        {
            _instance = this;
        }                
        DontDestroyOnLoad(transform.root.gameObject);        
    }
    
    public void Starterdeck()
    {
        
        for (int i = 0; i < 5; i++)
        {
            Createcard(0);
        }
        for (int i = 0; i < 5; i++)
        {
            Createcard(9);
        }
        Createcard(1);
    }
    public void Loaddeck()
    {
        string decklist = PlayerPrefs.GetString("deck");
        List<string> temp = new List<string>();
        List<int> space = new List<int>();
        List<bool> up = new List<bool>();
        int s = 0;
        string t = null;
        for (int i = 0; i < decklist.Length; i++)
        {
            char c = decklist[i];
            if (c == ' ')
            {
                space.Add(i);
            }
        }
        for (int i = 0; i < space.Count; i++)
        {
            for (int j = s; j < space[i]; j++)
            {
                t += decklist[j];
            }
            temp.Add(t);
            t = null;
            s = space[i];
        }
        for (int i = temp.Count - 1; i > -1; i--)
        {
            char c = temp[i][temp[i].Length - 1];
            if (c == '+')
            {
                temp[i] = temp[i].Replace("+", "");
                up.Add(true);
            }
            else
            {
                up.Add(false);
            }
        }
        for (int i = temp.Count - 1; i > -1; i--)
        {   
            int c = 0;
            int.TryParse(temp[i],out c);
            Createcard(c);
        }
        for (int i = temp.Count - 1; i > -1; i--)
        {
            if (up[i] == true)
            {
                Plus(orideck[i]);
            }
        }
    }
    public void Createcard(int num)
    {
        GameObject c;
        c = Instantiate(card) as GameObject;
        c.transform.parent = transform;
        c.transform.localScale = new Vector3(1, 1, 1);        
        Givestat(num, c);
        orideck.Add(c);
        c.SetActive(false);        
    }
    public string Returnname(int num)
    {
        string cardname = null;
        var wa = JSON.Parse(warriordeck.text);
        var wi = JSON.Parse(wizarddeck.text);
        if (PlayerPrefs.GetInt("character") == 1)
        {
            cardname = wa[num]["name"];
        }
        if (PlayerPrefs.GetInt("character") == 2)
        {
            cardname = wi[num]["name"];            
        }
        return cardname;
    }
    public void Copystat(GameObject ori,GameObject card)
    {
        card.AddComponent<Cardstat>();
        card.name = ori.name;
        card.GetComponent<Cardstat>().index = ori.GetComponent<Cardstat>().index;
        card.GetComponent<Cardstat>().cname = ori.GetComponent<Cardstat>().cname;
        card.GetComponent<Cardstat>().mana = ori.GetComponent<Cardstat>().mana;
        card.GetComponent<Cardstat>().pmana = ori.GetComponent<Cardstat>().pmana;
        card.GetComponent<Cardstat>().eft1 = ori.GetComponent<Cardstat>().eft1;
        card.GetComponent<Cardstat>().val1 = ori.GetComponent<Cardstat>().val1;
        card.GetComponent<Cardstat>().pval1 = ori.GetComponent<Cardstat>().pval1;
        card.GetComponent<Cardstat>().eft2 = ori.GetComponent<Cardstat>().eft2;
        card.GetComponent<Cardstat>().val2 = ori.GetComponent<Cardstat>().val2;
        card.GetComponent<Cardstat>().pval2 = ori.GetComponent<Cardstat>().pval2;
        card.GetComponent<Cardstat>().ex = ori.GetComponent<Cardstat>().ex;
        card.GetComponent<Cardstat>().up = ori.GetComponent<Cardstat>().up;
        card.GetComponent<Cardstat>().target = ori.GetComponent<Cardstat>().target;
        card.GetComponent<Cardstat>().grade = ori.GetComponent<Cardstat>().grade;
        card.GetComponent<Cardstat>().sort = ori.GetComponent<Cardstat>().sort;
        card.GetComponent<Cardstat>().des1 = ori.GetComponent<Cardstat>().des1;
        card.GetComponent<Cardstat>().des2 = ori.GetComponent<Cardstat>().des2;
    }
    public void Givestat(int num,GameObject card)
    {
        card.AddComponent<Cardstat>();
        var wa = JSON.Parse(warriordeck.text);
        var wi = JSON.Parse(wizarddeck.text);
        if (PlayerPrefs.GetInt("character") == 1)
        {
            card.name = wa[num]["name"];
            card.GetComponent<Cardstat>().index = wa[num]["index"];
            card.GetComponent<Cardstat>().cname = wa[num]["name"];
            card.GetComponent<Cardstat>().mana = wa[num]["mana"];
            card.GetComponent<Cardstat>().pmana = wa[num]["pmana"];
            card.GetComponent<Cardstat>().eft1 = wa[num]["effect1"];
            card.GetComponent<Cardstat>().val1 = wa[num]["value1"];
            card.GetComponent<Cardstat>().pval1 = wa[num]["pvalue1"];
            card.GetComponent<Cardstat>().eft2 = wa[num]["effect2"];
            card.GetComponent<Cardstat>().val2 = wa[num]["value2"];
            card.GetComponent<Cardstat>().pval2 = wa[num]["pvalue2"];
            card.GetComponent<Cardstat>().ex = wa[num]["extinction"];
            card.GetComponent<Cardstat>().up = wa[num]["up"];
            card.GetComponent<Cardstat>().target = wa[num]["target"];
            card.GetComponent<Cardstat>().grade = wa[num]["grade"];
            card.GetComponent<Cardstat>().sort = wa[num]["sort"];
            card.GetComponent<Cardstat>().des1 = wa[num]["des1"];
            card.GetComponent<Cardstat>().des2 = wa[num]["des2"];
        }
        if (PlayerPrefs.GetInt("character") == 2)
        {
            card.name = wi[num]["name"];
            card.GetComponent<Cardstat>().index = wi[num]["index"];
            card.GetComponent<Cardstat>().cname = wi[num]["name"];
            card.GetComponent<Cardstat>().mana = wi[num]["mana"];
            card.GetComponent<Cardstat>().pmana = wi[num]["pmana"];
            card.GetComponent<Cardstat>().eft1 = wi[num]["effect1"];
            card.GetComponent<Cardstat>().val1 = wi[num]["value1"];
            card.GetComponent<Cardstat>().pval1 = wi[num]["pvalue1"];
            card.GetComponent<Cardstat>().eft2 = wi[num]["effect2"];
            card.GetComponent<Cardstat>().val2 = wi[num]["value2"];
            card.GetComponent<Cardstat>().pval2 = wi[num]["pvalue2"];
            card.GetComponent<Cardstat>().ex = wi[num]["extinction"];
            card.GetComponent<Cardstat>().up = wi[num]["up"];
            card.GetComponent<Cardstat>().target = wi[num]["target"];
            card.GetComponent<Cardstat>().grade = wi[num]["grade"];
            card.GetComponent<Cardstat>().sort = wi[num]["sort"];
            card.GetComponent<Cardstat>().des1 = wi[num]["des1"];
            card.GetComponent<Cardstat>().des2 = wi[num]["des2"];
        }
    }
    public void Removedeck()
    {
        for (int i = 0; i < orideck.Count; i++)
        {            
            Destroy(orideck[i]);
        }
        orideck.RemoveRange(0, orideck.Count);
    }
    public void Plus(GameObject card)
    {
        card.GetComponent<Cardstat>().up = true;
        card.GetComponent<Cardstat>().cname += "+";
        card.GetComponent<Usecard>().Changevertical(card.GetComponent<Cardstat>().cname);
        card.GetComponent<Cardstat>().mana = card.GetComponent<Cardstat>().pmana;
        card.GetComponent<Cardstat>().val1 = card.GetComponent<Cardstat>().pval1;
        card.GetComponent<Cardstat>().val2 = card.GetComponent<Cardstat>().pval2;
        if (card.GetComponent<Cardstat>().des2 == "")
        {
            card.GetComponent<Usecard>().Writedes(card.GetComponent<Cardstat>().des1);
        }
        else
        {
            card.GetComponent<Cardstat>().des1 = card.GetComponent<Cardstat>().des2;
            card.GetComponent<Usecard>().Writedes(card.GetComponent<Cardstat>().des1);
        }
    }
}