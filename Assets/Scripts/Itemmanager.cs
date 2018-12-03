using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemmanager : MonoBehaviour 
{
    static Itemmanager _instance;
    public static Itemmanager instance()
    {
        return _instance;
    }
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
