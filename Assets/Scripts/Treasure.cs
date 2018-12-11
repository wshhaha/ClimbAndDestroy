﻿using System.Collections;
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
        Rewardt();
        rewards.SetActive(true);
    }
    void Rewardt()
    {
        int lotto = Random.Range(0, 100);
        int num = 0;
        if (lotto >= 0 && lotto < 90)
        {
            num = Random.Range(0, 2);
        }
        if (lotto >= 90 && lotto < 100)
        {
            num = Random.Range(2, 4);
        }
        rewards.GetComponent<Rewards>().Addreward("treasure", num);
    }
}
