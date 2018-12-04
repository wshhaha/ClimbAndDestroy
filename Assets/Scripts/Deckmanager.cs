using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Deckmanager : MonoBehaviour 
{
    static Deckmanager _instance;
    public static Deckmanager instance()
    {
        return _instance;
    }

    public TextAsset warriordeck;
    public TextAsset wizarddeck;
    public GameObject card;
    public List<GameObject> deck;
    
	void Start () 
	{
        if (_instance == null)
        {
            _instance = this;
        }
        var wa = JSON.Parse(warriordeck.text);
        var wi = JSON.Parse(wizarddeck.text);
        Starterdeck();
        DontDestroyOnLoad(gameObject);
    }
    void Starterdeck()
    {
        var wa = JSON.Parse(warriordeck.text);
        var wi = JSON.Parse(wizarddeck.text);
        int j = PlayerPrefs.GetInt("character");
        switch (j)
        {
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    GameObject c = Instantiate(card);
                    c.AddComponent<Cardstat>();
                    c.GetComponent<Cardstat>().cname = wa[0]["name"];
                    c.GetComponent<Cardstat>().mana = wa[0]["mana"];
                    c.GetComponent<Cardstat>().pmana = wa[0]["pmana"];
                    c.GetComponent<Cardstat>().eft1 = wa[0]["effect1"];
                    c.GetComponent<Cardstat>().val1 = wa[0]["value1"];
                    c.GetComponent<Cardstat>().pval1 = wa[0]["pvalue1"];
                    c.GetComponent<Cardstat>().eft2 = wa[0]["effect2"];
                    c.GetComponent<Cardstat>().val2 = wa[0]["value2"];
                    c.GetComponent<Cardstat>().pval2 = wa[0]["pvalue2"];
                    c.GetComponent<Cardstat>().ex = wa[0]["extinction"];
                    c.GetComponent<Cardstat>().grade = wa[0]["grade"];
                    deck.Add(c);
                }
                break;
        }
    }
}