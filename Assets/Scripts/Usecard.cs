using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usecard : MonoBehaviour 
{
    public GameObject gy;
    public GameObject p;
	public void Usingcard()
    {
        p = GameObject.Find("Player");
        if(p.GetComponent<Player>().turn==false)
        {
            return;
        }
        gy.GetComponent<Gyard>().gylist.Add(gameObject);
        transform.parent = gy.GetComponentInChildren<UIGrid>().transform;
        transform.localScale = new Vector3(.5f, .5f, .5f);
        transform.localPosition = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
        GameObject h = GameObject.Find("Hand");
        h.GetComponentInChildren<UIGrid>().enabled = true;
        h.GetComponent<Hand>().handlist.Remove(gameObject);
    }
}