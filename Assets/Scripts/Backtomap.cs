using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Backtomap : MonoBehaviour 
{
	public void Callmap()
    {
        if (Datamanager.i().stage % 10 == 0)
        {
            Admanager.instance().ShowRewardedAd();
        }
        SceneManager.LoadScene("2_Map");
    }
}