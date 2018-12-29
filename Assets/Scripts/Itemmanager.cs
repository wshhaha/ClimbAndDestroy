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
    public void Loaditem()
    {
        string itemlist = PlayerPrefs.GetString("item");
        List<string> temp = new List<string>();
        List<int> space = new List<int>();
        int s = 0;
        string t = null;
        for (int i = 0; i < itemlist.Length; i++)
        {
            char c = itemlist[i];
            if (c == ' ')
            {
                space.Add(i);
            }
        }
        for (int i = 0; i < space.Count; i++)
        {
            for (int j = s; j < space[i]; j++)
            {
                t += itemlist[j];
            }
            temp.Add(t);
            t = null;
            s = space[i];
        }
        for (int i = 0; i < temp.Count; i++)
        {
            int c = 0;
            int.TryParse(temp[i], out c);
            Itemcreate(c);
        }
    }
    public void Itemcreate(int num)
    {
        GameObject i = Instantiate(item);
        i.transform.parent = transform;
        i.transform.localPosition = Vector3.zero;
        i.transform.localScale = new Vector3(1, 1, 1);
        inven.Add(i);
        Itemstat(num, i);
        if (i.GetComponent<Iteminfo>().eft == "maxhp")
        {
            Datamanager.i().maxhp += 7;
        }
    }
    public void Itemstat(int num,GameObject i)
    {
        var tem = JSON.Parse(itemlist.text);
        i.GetComponent<Iteminfo>().index = tem[num]["index"];
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
    public int Returnstack(string item)
    {
        int stack = 0;
        for (int i = 0; i < inven.Count; i++)
        {
            if (inven[i].GetComponent<Iteminfo>().itemname == item)
            {
                stack++;
            }
        }
        return stack;
    }
    public string Convertitem()
    {
        string itemlist = null;
        if (inven.Count == 0)
        {
            print(itemlist);
            return null;
        }
        foreach (GameObject item in inven)
        {
            itemlist += (item.GetComponent<Iteminfo>().index - 1) + " ";
        }
        print(itemlist);
        return itemlist;
    }
    public void Removeinven()
    {
        for (int i = 0; i < inven.Count; i++)
        {
            Destroy(inven[i]);
        }
        inven.RemoveRange(0, inven.Count);
    }
}