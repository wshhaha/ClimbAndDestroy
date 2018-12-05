using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public GameObject spawner;    
    public void Discount()
    {
        spawner.GetComponent<Enemyspawner>().e--;
        if(spawner.GetComponent<Enemyspawner>().e <= 0)
        {
            spawner.GetComponent<Enemyspawner>().gomap.gameObject.SetActive(true);
            spawner.GetComponent<Enemyspawner>().rewards.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public bool Eaction()
    {
        return false;
    }
}
