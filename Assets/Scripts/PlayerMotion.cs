using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {
    public GameObject Joystick;
    public Animator animator;
    public bool AtkCheck;
    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    public float AtkTimeCheck;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("RunningCheck", false);
        AtkCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Joystick.transform.localPosition.x != 0 && Joystick.transform.localPosition.y != 0)
        {
            animator.SetBool("RunningCheck", true);
        }
        else
        {
            animator.SetBool("RunningCheck", false);
        }
        if(AtkCheck)
        {
            animator.SetBool("AttackCheck", true);
            if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                animator.SetBool("AttackCheck", false);
                print("gg");
                AtkCheck = false;
            }
        }
        if(AtkCheck==false&&Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }
    public void Attack()
    {
        AtkCheck = true;
    }
}
