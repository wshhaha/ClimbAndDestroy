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
        for (int i = 0; i < 4; i++)
        {
            Createcard(9);
        }
        Createcard(1);
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
}