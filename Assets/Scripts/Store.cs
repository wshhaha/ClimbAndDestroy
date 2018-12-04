using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour 
{
    public GameObject menulist;
    
    private void Start()
    {        
        Closestore();
    }
    public void Openstore()
    {
        menulist.SetActive(true);
    }
    public void Closestore()
    {
        menulist.SetActive(false);
    }
}
