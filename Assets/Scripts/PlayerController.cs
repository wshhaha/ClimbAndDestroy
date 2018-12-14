using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject JoyStickpos;
    
    public float JoystickX;
    public float JoystickY;
    public float moveSpeed;
   


	void Start () {
  
    }
	

	void Update () {
        JoystickX = JoyStickpos.GetComponent<UIJoystick>().position.x;
        JoystickY = JoyStickpos.GetComponent<UIJoystick>().position.y;
        transform.Translate(JoystickX * Time.deltaTime*0.2f, 0, JoystickY * Time.deltaTime*0.2f);
        //transform.LookAt(new Vector3(JoystickX, 0, JoystickY));
  	}
}
