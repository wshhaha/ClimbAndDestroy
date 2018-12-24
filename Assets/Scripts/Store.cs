using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour 
{
    public GameObject menulist;
    public GameObject card;
    public GameObject item;
    public GameObject blind;
    public UIGrid cardg;
    public UIGrid itemg;

    private void Start()
    {
        Effectsound.instance().bgm.clip = Effectsound.instance().bgmlist[4];
        Effectsound.instance().bgm.Play();
        Closestore();
        Createshop();
    }
    public void Openstore()
    {
        blind.SetActive(true);
        menulist.GetComponent<UITweener>().PlayForward();
    }
    public void Closestore()
    {
        menulist.GetComponent<UITweener>().PlayReverse();
        blind.SetActive(false);
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
            c.GetComponent<Cardstat>().gold = Random.Range(70, 110);
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
            c.GetComponent<Cardstat>().gold = Random.Range(130, 170);
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
            t.GetComponent<Iteminfo>().gold = Random.Range(150, 190);
            t.GetComponentInChildren<UILabel>().enabled = true;
            t.GetComponentInChildren<UILabel>().text = "" + t.GetComponent<Iteminfo>().gold;
            print(j);
            Seticon(t, j);
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
            t.GetComponent<Iteminfo>().gold = Random.Range(250, 300);
            t.GetComponentInChildren<UILabel>().enabled = true;
            t.GetComponentInChildren<UILabel>().text = "" + t.GetComponent<Iteminfo>().gold;
            print(j);
            Seticon(t, j);
            itemg.enabled = true;
        }
    }
    void Seticon(GameObject trea,int num)
    {
        switch (num)
        {
            case 0:
                trea.GetComponent<UISprite>().spriteName = "item_ring_soul";
                trea.GetComponent<UIButton>().normalSprite = "item_ring_soul";
                break;
            case 1:
                trea.GetComponent<UISprite>().spriteName = "item_shield";
                trea.GetComponent<UIButton>().normalSprite = "item_shield";
                break;
            case 2:
                trea.GetComponent<UISprite>().spriteName = "item_ring_mana";
                trea.GetComponent<UIButton>().normalSprite = "item_ring_mana";
                break;
            case 3:
                trea.GetComponent<UISprite>().spriteName = "item_potion_red";
                trea.GetComponent<UIButton>().normalSprite = "item_potion_red";
                break;
        }
    }
}