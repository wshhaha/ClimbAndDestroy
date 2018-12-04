using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Builddeck : MonoBehaviour 
{
    public GameObject card;
    public List<GameObject> deck;
    public TextAsset warriordeck;
    public TextAsset wizarddeck;
    public UIGrid grid;

    void Start () 
	{
        Createdeck();
    }
    void Createdeck()
    {
        for (int i = 0; i < Deckmanager.instance().deck.Count; i++)
        {
            GameObject c = Instantiate(card);
            c.transform.parent = grid.gameObject.transform;
            c.transform.localPosition = Vector3.zero;
            c.AddComponent<Cardstat>();
            deck.Add(c);
            Givestat(i);
            c.SetActive(false);
        }
    }
    public void Givestat(int num)
    {
        var wa = JSON.Parse(warriordeck.text);
        var wi = JSON.Parse(wizarddeck.text);
        if (PlayerPrefs.GetInt("character") == 1)
        {
            deck[num].GetComponent<Cardstat>().cname = wa[Deckmanager.instance().deck[num]]["name"];
            deck[num].GetComponent<Cardstat>().mana = wa[Deckmanager.instance().deck[num]]["mana"];
            deck[num].GetComponent<Cardstat>().pmana = wa[Deckmanager.instance().deck[num]]["pmana"];
            deck[num].GetComponent<Cardstat>().eft1 = wa[Deckmanager.instance().deck[num]]["effect1"];
            deck[num].GetComponent<Cardstat>().val1 = wa[Deckmanager.instance().deck[num]]["value1"];
            deck[num].GetComponent<Cardstat>().pval1 = wa[Deckmanager.instance().deck[num]]["pvalue1"];
            deck[num].GetComponent<Cardstat>().eft2 = wa[Deckmanager.instance().deck[num]]["effect2"];
            deck[num].GetComponent<Cardstat>().val2 = wa[Deckmanager.instance().deck[num]]["value2"];
            deck[num].GetComponent<Cardstat>().pval2 = wa[Deckmanager.instance().deck[num]]["pvalue2"];
            deck[num].GetComponent<Cardstat>().ex = wa[Deckmanager.instance().deck[num]]["extinction"];
            deck[num].GetComponent<Cardstat>().grade = wa[Deckmanager.instance().deck[num]]["grade"];
        }
        if (PlayerPrefs.GetInt("character") == 2)
        {
            deck[num].GetComponent<Cardstat>().cname = wi[Deckmanager.instance().deck[num]]["name"];
            deck[num].GetComponent<Cardstat>().mana = wi[Deckmanager.instance().deck[num]]["mana"];
            deck[num].GetComponent<Cardstat>().pmana = wi[Deckmanager.instance().deck[num]]["pmana"];
            deck[num].GetComponent<Cardstat>().eft1 = wi[Deckmanager.instance().deck[num]]["effect1"];
            deck[num].GetComponent<Cardstat>().val1 = wi[Deckmanager.instance().deck[num]]["value1"];
            deck[num].GetComponent<Cardstat>().pval1 = wi[Deckmanager.instance().deck[num]]["pvalue1"];
            deck[num].GetComponent<Cardstat>().eft2 = wi[Deckmanager.instance().deck[num]]["effect2"];
            deck[num].GetComponent<Cardstat>().val2 = wi[Deckmanager.instance().deck[num]]["value2"];
            deck[num].GetComponent<Cardstat>().pval2 = wi[Deckmanager.instance().deck[num]]["pvalue2"];
            deck[num].GetComponent<Cardstat>().ex = wi[Deckmanager.instance().deck[num]]["extinction"];
            deck[num].GetComponent<Cardstat>().grade = wi[Deckmanager.instance().deck[num]]["grade"];
        }
    }
}