using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eccprinter : MonoBehaviour 
{
    public GameObject printer;
    public UIGrid pgrid;
    public UILabel strl;
    public UILabel weakl;
    public UILabel lockonl;
    public UILabel dotl;
    public UIGrid icon;
    public GameObject str;
    public GameObject weak;
    public GameObject lockon;
    public GameObject dot;
    Enemy e;
    public GameObject shd;
    public UILabel shdl;

    private void Start()
    {
        e = GetComponent<Enemy>();
    }
    public void Showcc()
    {
        if (e.str != 0)
        {
            strl.gameObject.SetActive(true);
            strl.text = "This enemy power is " + e.str;
        }
        else
        {
            strl.gameObject.SetActive(false);
        }
        if (e.w == true)
        {
            weakl.gameObject.SetActive(true);
            weakl.text = "Deal 75% damage during " + e.wnum + "turn";
        }
        else
        {
            weakl.gameObject.SetActive(false);
        }
        if (e.l == true)
        {
            lockonl.gameObject.SetActive(true);
            lockonl.text = "Receives 150% damage during " + e.lnum + "turn";
        }
        else
        {
            lockonl.gameObject.SetActive(false);
        }
        if (e.d == true)
        {
            dotl.gameObject.SetActive(true);
            dotl.text = "Receive 2 damage at the end of turn during " + e.dnum + "turn";
        }
        else
        {
            dotl.gameObject.SetActive(false);
        }
        pgrid.gameObject.SetActive(true);
        printer.SetActive(true);
    }
    public void Offcc()
    {
        printer.SetActive(false);
    }
    private void Update()
    {
        if (e.str != 0)
        {
            str.SetActive(true);
        }
        else
        {
            str.SetActive(false);
        }
        if (e.w == true)
        {
            weak.SetActive(true);
        }
        else
        {
            weak.SetActive(false);
        }
        if (e.l == true)
        {
            lockon.SetActive(true);
        }
        else
        {
            lockon.SetActive(false);
        }
        if (e.d == true)
        {
            dot.SetActive(true);
        }
        else
        {
            dot.SetActive(false);
        }
        icon.enabled = true;
        if (e.shd > 0)
        {
            shd.SetActive(true);
            shdl.text = e.shd + "";
        }
        else
        {
            shd.SetActive(false);
            shdl.text = "";
        }
    }
}
