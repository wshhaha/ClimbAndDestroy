using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteminfo : MonoBehaviour 
{
    public string itemname;
    public string eft;
    public int val;
    public int tier;
    public int gold;

    public void Buyitem()
    {
        if (gold > Datamanager.i().gold)
        {
            return;
        }
        transform.parent = Itemmanager.instance().gameObject.transform;
        Itemmanager.instance().inven.Add(gameObject);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = Vector3.zero;
        Datamanager.i().gold -= gold;
        GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<UILabel>().enabled = false;
    }
}