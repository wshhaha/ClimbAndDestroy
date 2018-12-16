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
    }
    public void Copydeck()
    {
        for (int i = 0; i < Deckmanager.instance().orideck.Count; i++)
        {
            GameObject c;
            c = Instantiate(card);
            c.GetComponentInChildren<BoxCollider>().enabled = false;
            c.GetComponent<Usecard>().gy = GameObject.Find("Graveyard");
            Deckmanager.instance().Givestat(Deckmanager.instance().orideck[i].GetComponent<Cardstat>().index - 1, c);
            c.transform.parent = deckgrid.transform;
            c.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            c.GetComponent<UIPanel>().depth = 2;
            deck.Add(c);
        }
        StartCoroutine(Startturn());
    }
    IEnumerator Drawmotion()
    {
        if (deck.Count != 0)
        {
            int i = Random.Range(0, deck.Count);
            hand.GetComponent<Hand>().handlist.Add(deck[i]);
            deck[i].transform.parent = hand.GetComponentInChildren<UIGrid>().transform;
            deck[i].transform.localScale = new Vector3(1, 1, 1);
            deck[i].transform.localPosition = Vector3.zero;
            deck[i].GetComponentInChildren<BoxCollider>().enabled = true;
            deck[i].GetComponent<UIPanel>().depth = hand.GetComponent<Hand>().handlist.Count + 2;
            hand.GetComponentInChildren<UIGrid>().enabled = true;
            deck.RemoveAt(i);
        }
        else
        {
            if (gy.GetComponent<Gyard>().gylist.Count != 0)
            {
                gy.GetComponent<Gyard>().Backtodeck();
                StartCoroutine(Drawmotion());
            }
            else
            {
                print("no more card");
                yield break;
            }
        }
    }
    IEnumerator Cardm(int i)
    {
        Vector3 ori = deck[i].transform.position;
        float factor = 1;
        while (factor > 0)
        {
            deck[i].transform.localPosition = ori * factor;
            factor -= 0.01f;
            yield return new WaitForEndOfFrame();
        }
    }
    public void Drawing(int num)
    {
        for (int i = 0; i < num; i++)
        {
            StartCoroutine(Drawmotion());
        }
    }
    public void St()
    {
        StartCoroutine(Startturn());
    }
    IEnumerator Startturn()
    {
        for (int i = 0; i < 5; i++)
        {   
            yield return StartCoroutine(Drawmotion());
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