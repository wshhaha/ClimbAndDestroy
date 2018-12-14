using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public GameObject JoyStickpos;
    public float JoystickX;
    public float JoystickY;
    
    void Start()
    {
        //Vector3 direction = new Vector3(JoystickX,0,JoystickY);
       // direction.Normalize();
    }

    void Update()
    {
        Vector3 JoystickDir = new Vector3(JoystickX, 0, JoystickY);
        Vector3 PlayerDir = new Vector3(transform.position.x, 0, transform.position.y);
        JoystickDir.Normalize();
        PlayerDir.Normalize();
        

        JoystickX = JoyStickpos.GetComponent<UIJoystick>().position.x;
        JoystickY = JoyStickpos.GetComponent<UIJoystick>().position.y;
        if(JoystickX!=0&&JoystickY!=0)
        {
            Playerturn();
        }
        
    }
    public void Playerturn()
    {
        transform.LookAt(new Vector3(JoystickX*100f,0,JoystickY*100f));
    }
}

