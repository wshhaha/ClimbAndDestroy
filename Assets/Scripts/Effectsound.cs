using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectsound : MonoBehaviour 
{
    static Effectsound _instance;
    public AudioSource bgm;
    public AudioSource sound;

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
        DontDestroyOnLoad(gameObject);
	}
    public void Sfxplay(AudioClip clip)
    {
        sound.PlayOneShot(clip);
    }
}
