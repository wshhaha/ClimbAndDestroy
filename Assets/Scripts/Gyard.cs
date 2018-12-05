using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyard : MonoBehaviour 
{
    public List<GameObject> gylist;
    public GameObject deck;
    public void Backtodeck()
    {        
        while (gylist.Count!=0)
        {
            int i = 0;
            gylist[i].transform.parent = deck.GetComponentInChildren<UIGrid>().transform;
            gylist[i].transform.localPosition = Vector3.zero;
            deck.GetComponent<Builddeck>().deck.Add(gylist[i]);
            gylist.RemoveAt(i);
            i++;
        }
    }
}