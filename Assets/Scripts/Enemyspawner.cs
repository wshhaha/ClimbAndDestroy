using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawner : MonoBehaviour 
{
    public UISprite gomap;
    public GameObject rewards;
    public UISprite slot1;
    public UISprite slot2;
    public UISprite slot3;
    public int e;
    void Start () 
	{
        rewards.SetActive(false);
        gomap.gameObject.SetActive(false);
        slot1.gameObject.SetActive(false);
        slot2.gameObject.SetActive(false);
        slot3.gameObject.SetActive(false);
        e = Random.Range(1, 4);
        switch (e)
        {
            case 3:
                slot1.gameObject.SetActive(true);
                slot2.gameObject.SetActive(true);
                slot3.gameObject.SetActive(true);
                break;
            case 2:
                slot1.gameObject.SetActive(false);
                slot2.gameObject.SetActive(true);
                slot3.gameObject.SetActive(true);
                break;
            case 1:
                slot1.gameObject.SetActive(false);
                slot2.gameObject.SetActive(true);
                slot3.gameObject.SetActive(false);
                break;
        }
    }
}