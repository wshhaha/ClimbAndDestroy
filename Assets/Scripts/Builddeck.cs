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
            c.GetComponent<Usecard>().back.enabled = true;
            deck.Add(c);
        }
        StartCoroutine(Startturn());
    }
    IEnumerator Drawmotion()
    {
        if (deck.Count != 0)
        {
            int i = Random.Range(0, deck.Count);
            GameObject target = deck[i];
            hand.GetComponent<Hand>().handlist.Add(target);
            deck.Remove(target);
            target.GetComponent<Usecard>().back.enabled = false;
            target.GetComponent<UIPanel>().depth = hand.GetComponent<Hand>().handlist.Count + 2;
            target.GetComponentInChildren<BoxCollider>().enabled = true;
            target.transform.parent = hand.GetComponentInChildren<UIGrid>().transform;
            target.transform.localScale = new Vector3(1, 1, 1);
            yield return StartCoroutine(Cardm(target));
            hand.GetComponentInChildren<UIGrid>().enabled = true;
        }
        else
        {
            if (gy.GetComponent<Gyard>().gylist.Count != 0)
            {
                gy.GetComponent<Gyard>().Backtodeck();
                for (int i = 0; i < deck.Count; i++)
                {
                    deck[i].transform.localPosition = Vector3.zero;
                    yield return new WaitForEndOfFrame();
                }
                StartCoroutine(Drawmotion());
            }
            else
            {
                print("no more card");
                yield break;
            }
        }
    }
    IEnumerator Cardm(GameObject card)
    {
        card.transform.Rotate(0, 0, -90);
        Vector3 ori = new Vector3(-1.5f, -0.7f, 0);
        float factor = 1;
        while (factor > 0)
        {
            card.transform.position = new Vector3(ori.x * factor,ori.y,ori.z);
            factor -= 0.2f;
            yield return new WaitForEndOfFrame();
        }
        card.transform.Rotate(0, 0, 90);
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
        Player p = GameObject.Find("Player").GetComponent<Player>();
        p.te = false;
    }
}