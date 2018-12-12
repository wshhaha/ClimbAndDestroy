using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menucnt : MonoBehaviour 
{
    public GameObject menu;
	public void Callmenu()
    {
        menu.SetActive(true);
    }
    public void Resume()
    {
        menu.SetActive(false);
    }
    public void Gototitle()
    {
        SceneManager.LoadScene("1_Title");
    }
    public void Exitgame()
    {
        Application.Quit();
    }
}
