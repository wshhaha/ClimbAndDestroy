using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerashake : MonoBehaviour 
{
    Vector3 originPos;
    public float a;
    public float d;
    static Camerashake _instance;
    public static Camerashake instance()
    {
        return _instance;
    }
    void Start()
    {
        originPos = transform.localPosition;
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitSphere * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }
    public void Startshake()
    {
        StartCoroutine(Shake(a, d));
    }
}
