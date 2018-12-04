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

    public List<int> deck;
    
	void Start () 
	{
        if (_instance == null)
        {
            _instance = this;
        }        
        Starterdeck();
        DontDestroyOnLoad(gameObject);        
    }
    
    public void Starterdeck()
    {        
        for (int i = 0; i < 5; i++)
        {
            deck.Add(0);            
        }
        for (int i = 0; i < 4; i++)
        {
            deck.Add(9);
        }
        deck.Add(1);
    }
}