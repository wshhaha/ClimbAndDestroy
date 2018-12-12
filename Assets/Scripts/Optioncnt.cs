using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optioncnt : MonoBehaviour 
{
    public UIProgressBar bgm;
    public UIProgressBar sound;
    float oribgmvol;
    float orisoundvol;
    public GameObject option;
    
    void Start () 
	{   
        oribgmvol = Effectsound.instance().bgm.volume;
        orisoundvol = Effectsound.instance().sound.volume;
    }
    public void Calloption()
    {
        option.SetActive(true);
        oribgmvol = Effectsound.instance().bgm.volume;
        orisoundvol = Effectsound.instance().sound.volume;
    }
    public void BGMvol()
    {
        Effectsound.instance().bgm.volume = bgm.value;
    }
    public void Soundvol()
    {
        Effectsound.instance().sound.volume = sound.value;
    }
    public void Opok()
    {
        option.SetActive(false);
    }
    public void Opcancel()
    {
        Effectsound.instance().bgm.volume = oribgmvol;
        Effectsound.instance().sound.volume = orisoundvol;
        bgm.value = oribgmvol;
        sound.value = orisoundvol;
        option.SetActive(false);
    }
}