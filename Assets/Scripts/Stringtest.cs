using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stringtest : MonoBehaviour 
{
    public string a = "deal val1 damage";
    public int b = 1234567890;
    int s;
    string t;
    string returnstring;
    private void Start()
    {
        print(a);
        Changestring();
    }
    void Changestring()
    {
        List<string> temp = new List<string>();
        List<int> space = new List<int>();

        for (int i = 0; i < a.Length; i++)
        {
            char c = a[i];
            if(c==' ')
            {
                space.Add(i);
            }
        }
        space.Add(a.Length);
        for (int i = 0; i < space.Count; i++)
        {
            for (int j = s; j < space[i]; j++)
            {
                t += a[j];
            }
            temp.Add(t);
            t = null;
            s = space[i];
        }

        for (int i = 0; i < temp.Count; i++)
        {
            if(temp[i]==" val1")
            {
                temp[i] = " " + b;
            }
        }
        for (int i = 0; i < temp.Count; i++)
        {
            returnstring += temp[i];
        }
        print(returnstring);
    }
}
