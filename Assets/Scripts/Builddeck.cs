using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Builddeck : MonoBehaviour 
{
    public GameObject card;
    public List<GameObject> deck;   
    public UIGrid deckgrid;
    public GameObject hand;
    public GameObject gy;
    
    void Start () 
	{
        Copydeck();
        Startturn();
    }
    public void Copydeck()
    {
        for (int i = 0; i < Deckmanager.instance().orideck.Count; i++)
        {
            GameObject c;
            c = Instantiate(card);
            c.GetComponent<BoxCollider>().enabled = false;
            c.GetComponent<Usecard>().gy = GameObject.Find("Graveyard");
            Deckmanager.instance().Givestat(Deckmanager.instance().orideck[i].GetComponent<Cardstat>().index - 1, c);
            c.transform.parent = deckgrid.transform;
            c.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            c.GetComponent<UISprite>().depth = 0;
            deck.Add(c);
        }
    }
    public void Drawacard()
    {
        if (deck.Count != 0)
        {
            int i = Random.Range(0, deck.Count);
            hand.GetComponent<Hand>().handlist.Add(deck[i]);
            deck[i].transform.parent = hand.GetComponentInChildren<UIGrid>().transform;
            deck[i].transform.localScale = new Vector3(1, 1, 1);
            deck[i].transform.localPosition = Vector3.zero;
            deck[i].GetComponent<BoxCollider>().enabled = true;
            deck[i].GetComponent<UISprite>().depth = hand.GetComponent<Hand>().handlist.Count;
            
            hand.GetComponentInChildren<UIGrid>().enabled = true;
            deck.RemoveAt(i);
        }
        else
        {
            if (gy.GetComponent<Gyard>().gylist.Count != 0)
            {
                gy.GetComponent<Gyard>().Backtodeck();
                Drawacard();
            }
            else
            {
                print("no more card");
                return;
            }
        }
    }
    public void Drawing(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Drawacard();
        }
    }
    public void Startturn()
    {
        for (int i = 0; i < 5; i++)
        {
            Drawacard();
        }
        Datamanager.i().curmana = Datamanager.i().inmaxmana;
        if (Datamanager.i().b == true)
        {
            Datamanager.i().curmana--;
            Datamanager.i().bnum--;
            if (Datamanager.i().bnum == 0)
            {
                Datamanager.i().b = false;
            }
        }
    }
}