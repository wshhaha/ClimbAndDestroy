using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextstair : MonoBehaviour 
{    
	public void Next()
    {        
        Datamanager.i().stage++;
        if (Datamanager.i().stage % 10 == 1)
        {
            SceneManager.LoadScene("Battle");
            return;
        }
        if (Datamanager.i().stage % 10 == 5)
        {
            SceneManager.LoadScene("Treasure");
            return;
        }
        if (Datamanager.i().stage % 10 == 9)
        {
            SceneManager.LoadScene("Rest");
            return;
        }
        int i = Random.Range(0, 100);
        int j = 0;
        if (i >= 0 && i < 75)
        {
            j = 0;
        }
        if (i >= 75 && i < 85)
        {
            j = 1;
        }
        if (i >= 85 && i < 95)
        {
            j = 2;
        }
        if (i >= 95 && i < 100)
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
