using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public bool turn;
    public GameObject h;
    public GameObject gy;
    public GameObject spawner;
	void Start () 
	{
        h = GameObject.Find("Hand");
        gy = GameObject.Find("Graveyard");
        spawner = GameObject.Find("Enemyspawner");
        turn = true;
        Datamanager.i().inmaxmana = Datamanager.i().maxmana;
    }
	public void Turnend()
    {
        turn = false;
        Throwcard();
        if (spawner.GetComponent<Enemyspawner>().slot1.gameObject.activeSelf == true)
        {
            spawner.GetComponent<Enemyspawner>().eturn1 = true;
        }
        else
        {
            spawner.GetComponent<Enemyspawner>().eturn2 = true;
        }
        spawner.GetComponent<Enemyspawner>().Enemyturn();
    }
    void Throwcard()
    {
        while (h.GetComponent<Hand>().handlist.Count!=0)
        {
            int i = 0;
            gy.GetComponent<Gyard>().gylist.Add(h.GetComponent<Hand>().handlist[i]);
            h.GetComponent<Hand>().handlist[i].transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
            h.GetComponent<Hand>().handlist[i].GetComponent<UISprite>().depth = 0;
            h.GetComponent<Hand>().handlist[i].transform.localScale = new Vector3(.5f, .5f, .5f);
            h.GetComponent<Hand>().handlist[i].transform.localPosition = Vector3.zero;
            h.GetComponent<Hand>().handlist[i].GetComponent<BoxCollider>().enabled = false;
            h.GetComponent<Hand>().handlist.Remove(h.GetComponent<Hand>().handlist[i]);            
        }
    }
}
