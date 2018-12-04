using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour 
{
    public GameObject rewards;
    private void Start()
    {
        rewards.SetActive(false);
    }
    public void Openbox()
    {        
        GetComponentInChildren<UIButton>().enabled = false;
        rewards.SetActive(true);
    }
}
