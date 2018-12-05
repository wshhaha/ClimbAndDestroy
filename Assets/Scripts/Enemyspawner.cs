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
    public GameObject p;
    public GameObject d;
    public int e;
    public bool eturn1;
    public bool eturn2;
    public bool eturn3;
    public GameObject target;
    void Start () 
	{
        d = GameObject.Find("Deck");
        p = GameObject.Find("Player");
        rewards.SetActive(false);
        gomap.gameObject.SetActive(false);
        slot1.gameObject.SetActive(false);
        slot2.gameObject.SetActive(false);
        slot3.gameObject.SetActive(false);
        eturn1 = false;
        eturn2 = false;
        eturn3 = false;
        Decideenum();
    }
    void Decideenum()
    {
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
    public void Enemyturn()
    {
        if (eturn1 == true)
        {
            eturn1 = slot1.gameObject.GetComponent<Enemy>().Eaction();
            eturn2 = true;
            print("1");
            eturn2 = slot2.gameObject.GetComponent<Enemy>().Eaction();
            eturn3 = true;
            print("2");
            eturn3 = slot3.gameObject.GetComponent<Enemy>().Eaction();
            print("3");            
        }
        if (eturn2 == true)
        {
            eturn2 = slot2.gameObject.GetComponent<Enemy>().Eaction();
            eturn3 = true;
            print("2");
            if (slot3.gameObject.activeSelf == true)
            {
                eturn3 = slot3.gameObject.GetComponent<Enemy>().Eaction();
                print("3");
            }            
        }
        p.GetComponent<Player>().turn = true;
        d.GetComponent<Builddeck>().Startturn();
    }
    public void Targetlock(GameObject e)
    {
        target = e;
    }
}