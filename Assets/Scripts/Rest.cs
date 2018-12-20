﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour 
{
    public GameObject gotomap;
    public GameObject cardlist;
    public List<GameObject> reinlist;
    public GameObject target;
    public GameObject yesno;

    private void Start()
    {
        cardlist.SetActive(false);
        yesno.SetActive(false);
        gotomap.SetActive(false); 
    }
    public void Camp()
    {
        Datamanager.i().curhp += (int)(Datamanager.i().maxhp * 0.3f);
        if(Datamanager.i().curhp>= Datamanager.i().maxhp)
        {
            Datamanager.i().curhp = Datamanager.i().maxhp;
        }
        gotomap.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Reinforce()
    {
        StartCoroutine(Reinforcecrad());
    }
    IEnumerator Reinforcecrad()
    {
        Classcard();
        cardlist.SetActive(true);
        gotomap.SetActive(true);        
        yield return new WaitForEndOfFrame();
    }
    void Classcard()
    {   
        for (int i = Deckmanager.instance().orideck.Count-1; i > -1; i--)
        {
            if (Deckmanager.instance().orideck[i].GetComponent<Cardstat>().up == false)
            {
                reinlist.Add(Deckmanager.instance().orideck[i]);
                Deckmanager.instance().orideck[i].transform.parent = cardlist.GetComponentInChildren<UIGrid>().gameObject.transform;
                Deckmanager.instance().orideck[i].transform.localPosition = Vector3.zero;
                Deckmanager.instance().orideck[i].transform.localScale = new Vector3(1, 1, 1);
                Deckmanager.instance().orideck[i].SetActive(true);
                Deckmanager.instance().orideck.Remove(Deckmanager.instance().orideck[i]);
                cardlist.GetComponentInChildren<UIGrid>().enabled = true;
            }
        }
    }   
    public void Acceptbtn()
    {   
        target.GetComponent<Cardstat>().up = true;
        target.GetComponent<Cardstat>().mana = target.GetComponent<Cardstat>().pmana;
        target.GetComponent<Cardstat>().val1 = target.GetComponent<Cardstat>().pval1;
        target.GetComponent<Cardstat>().val2 = target.GetComponent<Cardstat>().pval2;
        if (target.GetComponent<Cardstat>().des2 == "")
        {
            target.GetComponent<Usecard>().Writedes(target.GetComponent<Cardstat>().des1);
        }
        else
        {
            target.GetComponent<Cardstat>().des1 = target.GetComponent<Cardstat>().des2;
            target.GetComponent<Usecard>().Writedes(target.GetComponent<Cardstat>().des1);
        }
        for (int i = reinlist.Count-1; i > -1; i--)
        {
            Deckmanager.instance().orideck.Add(reinlist[i]);
            reinlist[i].transform.parent = Deckmanager.instance().gameObject.transform;
            reinlist[i].transform.localScale = new Vector3(1, 1, 1);
            reinlist[i].transform.localPosition = Vector3.zero;
            reinlist[i].SetActive(false);
            reinlist.Remove(reinlist[i]);
        }
        yesno.SetActive(false);
        cardlist.SetActive(false);
        gameObject.SetActive(false);
    }
    public void Cancelbtn()
    {
        yesno.SetActive(false);
    }
}