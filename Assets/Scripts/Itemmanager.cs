using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Itemmanager : MonoBehaviour 
{
    public GameObject item;
    public List<GameObject> inven;
    static Itemmanager _instance;
    public TextAsset itemlist;

    public static Itemmanager instance()
    {
        return _instance;
    }
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        DontDestroyOnLoad(transform.root.gameObject);
    }
    public void Itemcreate(int num)
    {
        
        GameObject i = Instantiate(item);
        i.transform.parent = transform;
        i.transform.localPosition = Vector3.zero;
        i.transform.localScale = new Vector3(1, 1, 1);
        inven.Add(i);
        Itemstat(num, i);
    }
    public void Itemstat(int num,GameObject i)
    {
        var tem = JSON.Parse(itemlist.text);
        i.GetComponent<Iteminfo>().itemname = tem[num]["name"];
        i.name = tem[num]["name"];
        i.GetComponent<Iteminfo>().eft = tem[num]["eft"];
        i.GetComponent<Iteminfo>().val = tem[num]["val"];
        i.GetComponent<Iteminfo>().tier = tem[num]["tier"];
    }
    public string Returnname(int num)
    {
        string itemname=null;
        var tem = JSON.Parse(itemlist.text);
        itemname = tem[num]["name"];
        return itemname;
    }
}