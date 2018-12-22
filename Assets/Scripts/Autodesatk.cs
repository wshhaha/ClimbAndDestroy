using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodesatk : MonoBehaviour 
{
    float timer;
    bool once = false;
    public float shaketime;
    private void Start()
    {
        Destroy(gameObject, 1);
    }
    void Update () 
	{
        if (shaketime == 0)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= shaketime&&once==false)
        {
            once = true;
            Camerashake.instance().Startshake();
        }
	}
}
