using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logintest : MonoBehaviour 
{
	void Start () 
	{
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                string user = Social.localUser.userName;
                GetComponent<UILabel>().text = user;
            }
            else
            {
                GetComponent<UILabel>().text  = "fail";
            }
        });
    }	
}
