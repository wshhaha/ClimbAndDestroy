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
        UISprite box = GameObject.Find("box").GetComponent<UISprite>();
        GetComponentInChildren<UIButton>().enabled = false;
        Rewardt();
        rewards.SetActive(true);
        box.spriteName = "open";
        box.MakePixelPerfect();
    }
    void Rewardt()
    {
        int rang = Random.Range(50, 80);
        rewards.GetComponent<Rewards>().Addreward("gold", rang);
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