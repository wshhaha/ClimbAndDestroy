﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Backtomap : MonoBehaviour 
{
	public void Callmap()
    {
        SceneManager.LoadScene("Map");
    }
}
