using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyard : MonoBehaviour 
{
    public List<GameObject> gylist;
    public GameObject deck;
    public void Backtodeck()
    {
        for (int i = 0; i < gylist.Count; i++)
        {
            gylist[i].transform.parent = deck.GetComponentInChildren<UIGrid>().transform;
            deck.GetComponent<Builddeck>().deck.Add(gylist[i]);
            //gylist[i].transform.localPosition = Vector3.zero;
        }
        gylist.RemoveRange(0, gylist.Count);
    }
}