using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour 
{
    public GameObject menulist;
    public GameObject card;
    public GameObject item;
    public UIGrid cardg;
    public UIGrid itemg;

    private void Start()
    {        
        Closestore();
        Createshop();
    }
    public void Openstore()
    {
        menulist.SetActive(true);
    }
    public void Closestore()
    {
        menulist.SetActive(false);
    }
    void Createshop()
    {
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(1, 9);
            GameObject c = Instantiate(card);
            Deckmanager.instance().Givestat(j, c);            
            c.transform.parent = cardg.gameObject.transform;
            c.transform.localScale = new Vector3(.8f, .8f, .8f);
            c.GetComponent<Cardstat>().gold = Random.Range(30, 50);
            c.GetComponentInChildren<UILabel>().enabled = true;
            c.GetComponentInChildren<UILabel>().text = "" + c.GetComponent<Cardstat>().gold;
            cardg.enabled = true;
        }
        for (int i = 0; i < 2; i++)
        {
            int j = Random.Range(10, 16);
            GameObject c = Instantiate(card);
            Deckmanager.instance().Givestat(j, c);            
            c.transform.parent = cardg.gameObject.transform;
            c.transform.localScale = new Vector3(.8f, .8f, .8f);
            c.GetComponent<Cardstat>().gold = Random.Range(40, 80);
            c.GetComponentInChildren<UILabel>().enabled = true;
            c.GetComponentInChildren<UILabel>().text = "" + c.GetComponent<Cardstat>().gold;
            cardg.enabled = true;
        }
        for (int i = 0; i < 1; i++)
        {
            int j = Random.Range(16, 20);
            GameObject c = Instantiate(card);
            Deckmanager.instance().Givestat(j, c);            
            c.transform.parent = cardg.gameObject.transform;
            c.transform.localScale = new Vector3(.8f, .8f, .8f);
            c.GetComponent<Cardstat>().gold = Random.Range(60, 110);
            c.GetComponentInChildren<UILabel>().enabled = true;
            c.GetComponentInChildren<UILabel>().text = "" + c.GetComponent<Cardstat>().gold;
            cardg.enabled = true;
        }
        for (int i = 0; i < 2; i++)
        {
            int j = Random.Range(0, 2);
            GameObject t = Instantiate(item);
            Itemmanager.instance().Itemstat(j, t);
            t.GetComponent<BoxCollider>().enabled = true;
            t.transform.parent = itemg.gameObject.transform;
            t.transform.localScale = new Vector3(1, 1, 1);
            t.GetComponent<Iteminfo>().gold = Random.Range(100, 150);
            t.GetComponentInChildren<UILabel>().enabled = true;
            t.GetComponentInChildren<UILabel>().text = "" + t.GetComponent<Iteminfo>().gold;
            itemg.enabled = true;
        }
        for (int i = 0; i < 1; i++)
        {
            int j = Random.Range(2, 4);
            GameObject t = Instantiate(item);
            Itemmanager.instance().Itemstat(j, t);
            t.GetComponent<BoxCollider>().enabled = true;
            t.transform.parent = itemg.gameObject.transform;
            t.transform.localScale = new Vector3(1, 1, 1);
            t.GetComponent<Iteminfo>().gold = Random.Range(220, 300);
            t.GetComponentInChildren<UILabel>().enabled = true;
            t.GetComponentInChildren<UILabel>().text = "" + t.GetComponent<Iteminfo>().gold;
            itemg.enabled = true;
        }
    }
}