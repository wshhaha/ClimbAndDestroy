using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewardbtninfo : MonoBehaviour 
{
    public int val;

    public void Onclick()
    {
        switch (gameObject.name)
        {
            case "gold":
                Datamanager.i().gold += val;
                Effectmanager.i().Startsfx(35);
                break;
            case "card":
                Deckmanager.instance().Createcard(val);
                break;
            case "treasure":
                Itemmanager.instance().Itemcreate(val);
                break;
        }
        gameObject.SetActive(false);
    }
}
