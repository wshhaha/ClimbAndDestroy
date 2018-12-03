using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectsound : MonoBehaviour 
{
    static Effectsound _instance;
    public static Effectsound instance()
    {
        return _instance;
    }
    void Start () 
	{
        if (_instance == null)
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject.transform.parent);
	}	
	void Update () 
	{
		
	}
    public void Sfxplay(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
