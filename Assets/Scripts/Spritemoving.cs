using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spritemoving : MonoBehaviour 
{
    Vector3 ori;
	void Start () 
	{
        ori = transform.localPosition;
	}	
	void Update () 
	{
        if (GetComponent<UITweener>().enabled == false)
        {
            transform.localPosition = ori;
        }
    }
}
