using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextstair : MonoBehaviour 
{
    public GameObject cardlist;
    public List<GameObject> viewlist;
    public GameObject blind;

    void Start()
    {
        Effectsound.instance().bgm.clip = Effectsound.instance().bgmlist[1];
        Effectsound.instance().bgm.Play();
        Copydeck();
        Closelist();
    }
    void Copydeck()
    {
        for (int i = Deckmanager.instance().orideck.Count - 1; i > -1; i--)
        {
            viewlist.Add(Deckmanager.instance().orideck[i]);
            Deckmanager.instance().orideck[i].transform.parent = cardlist.GetComponentInChildren<UIGrid>().gameObject.transform;
            Deckmanager.instance().orideck[i].transform.localPosition = Vector3.zero;
            Deckmanager.instance().orideck[i].transform.localScale = new Vector3(1, 1, 1);
            Deckmanager.instance().orideck[i].GetComponent<Usecard>().mana.SetActive(true);
            Deckmanager.instance().orideck[i].SetActive(true);
            Deckmanager.instance().orideck.Remove(Deckmanager.instance().orideck[i]);
            cardlist.GetComponentInChildren<UIGrid>().enabled = true;
        }
    }
    void Returndeck()
    {
        for (int i = viewlist.Count - 1; i > -1; i--)
        {
            Deckmanager.instance().orideck.Add(viewlist[i]);
            viewlist[i].transform.parent = Deckmanager.instance().gameObject.transform;
            viewlist[i].transform.localScale = new Vector3(1, 1, 1);
            viewlist[i].transform.localPosition = Vector3.zero;
            viewlist[i].SetActive(false);
            viewlist.Remove(viewlist[i]);
        }
    }
    public void Next()
    {
        Returndeck();
        Datamanager.i().curscore++;
        Datamanager.i().stage++;
        if (Datamanager.i().stage % 10 == 1)
        {
            SceneManager.LoadScene("Battle");
            return;
        }
        if (Datamanager.i().stage % 10 == 5)
        {
            SceneManager.LoadScene("Treasure");
            return;
        }
        if (Datamanager.i().stage % 10 == 0)
        {
            SceneManager.LoadScene("Rest");
            return;
        }
        int i = Random.Range(0, 100);
        int j = 0;
        if (i >= 0 && i < 70)
        {
            j = 0;
        }
        if (i >= 70 && i < 80)
        {
            j = 1;
        }
        if (i >= 80 && i < 95)
        {
            j = 2;
        }
        if (i >= 95 && i < 100)
        {
            j = 3;
        }
        switch (j)
        {
            case 0:
                SceneManager.LoadScene("Battle");
                break;
            case 1:
                SceneManager.LoadScene("Rest");
                break;
            case 2:
                SceneManager.LoadScene("Store");
                break;
            case 3:
                SceneManager.LoadScene("Treasure");
                break;
        }        
    }    
    public void Viewcard()
    {
        blind.SetActive(true);
        cardlist.GetComponent<UITweener>().PlayForward();
    }
    public void Closelist()
    {
        cardlist.GetComponent<UITweener>().PlayReverse();
        blind.SetActive(false);
    }
}
