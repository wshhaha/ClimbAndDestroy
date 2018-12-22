using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmanager : MonoBehaviour 
{
    public List<GameObject> eftlist;
    public List<AudioClip> sfxlist;
    public Vector3 eftpos;

    static Effectmanager _instance;
    public static Effectmanager i()
    {
        return _instance;
    }

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Starteft(int num)
    {
        GameObject eft = Instantiate(eftlist[num]);

    }
    public void Startsfx(int num)
    {
        Effectsound.instance().Sfxplay(sfxlist[num]);
    }
}
