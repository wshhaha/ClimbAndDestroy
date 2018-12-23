using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wtf : MonoBehaviour 
{
    float ct = 0.05f;
    float timer;
	void Start () 
	{
        GetComponent<ParticleSystem>().playbackSpeed = 100;
	}	
	void Update () 
	{
        timer += Time.deltaTime;
        if (timer >= ct)
        {
            GetComponent<ParticleSystem>().playbackSpeed = 1;
        }
	}
}
