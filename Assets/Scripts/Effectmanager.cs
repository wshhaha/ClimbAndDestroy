using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmanager : MonoBehaviour 
{
    public List<GameObject> eftlist;
    public List<GameObject> eeftlist;
    public List<AudioClip> sfxlist;
    public GameObject eftpos;

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
        DontDestroyOnLoad(gameObject);
    }

    public GameObject Starteft(int num)
    {   
        GameObject eft = Instantiate(eftlist[num]);
        eft.transform.parent = eftpos.transform;
        eft.transform.localScale = new Vector3(1, 1, 1);
        eft.transform.localPosition = Vector3.zero;
        eft.transform.parent = transform;
        return eft;
    }
    public void Startsfx(int num)
    {
        Effectsound.instance().Sfxplay(sfxlist[num]);
    }
}
