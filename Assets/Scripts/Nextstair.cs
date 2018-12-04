using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextstair : MonoBehaviour 
{    
	public void Next()
    {        
        Datamanager.instance().stage++;
        if (Datamanager.instance().stage % 10 == 5)
        {
            SceneManager.LoadScene("Treasure");
            return;
        }
        if (Datamanager.instance().stage % 10 == 9)
        {
            SceneManager.LoadScene("Rest");
            return;
        }
        int i = Random.Range(0, 100);
        int j = 0;
        if (i >= 0 && i < 60)
        {
            j = 0;
        }
        if (i >= 60 && i < 75)
        {
            j = 1;
        }
        if (i >= 75 && i < 90)
        {
            j = 2;
        }
        if (i >= 90 && i < 100)
        {
            j = 3;
        }
        switch (j)
        {
            case 0:
                SceneManager.LoadScene("Battle");
                break;
            case 1:
                SceneManager.LoadScene("Rest");
                break;
            case 2:
                SceneManager.LoadScene("Store");
                break;
            case 3:
                SceneManager.LoadScene("Treasure");
                break;
        }        
    }    
}
