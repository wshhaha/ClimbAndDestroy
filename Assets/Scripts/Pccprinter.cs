using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pccprinter : MonoBehaviour 
{
    public GameObject printer;
    public UIGrid pgrid;
    public UILabel strl;
    public UILabel weakl;
    public UILabel lockonl;
    public UILabel dotl;
    public UILabel refl;
    public UILabel insl;
    public UILabel genl;
    public UIGrid icon;
    public GameObject str;
    public GameObject gen;
    public GameObject weak;
    public GameObject lockon;
    public GameObject dot;
    public GameObject reflect;
    public GameObject ins;
    public GameObject shd;
    public UILabel shdl;

    public void Showcc()
    {   
        if (Datamanager.i().str != 0)
        {
            strl.gameObject.SetActive(true);
            strl.text = "Your character power is " + Datamanager.i().str;
        }
        else
        {
            strl.gameObject.SetActive(false);
        }
        if (Datamanager.i().genamr == true)
        {
            genl.gameObject.SetActive(true);
            genl.text = "Gain " + Datamanager.i().gennum + "armor for each turn";
        }
        else
        {
            genl.gameObject.SetActive(false);
        }
        if (Datamanager.i().w == true)
        {
            weakl.gameObject.SetActive(true);
            weakl.text = "Deal 75% damage during " + Datamanager.i().wnum + "turn";
        }
        else
        {
            weakl.gameObject.SetActive(false);
        }
        if (Datamanager.i().l == true)
        {
            lockonl.gameObject.SetActive(true);
            lockonl.text = "Receives 150% damage during " + Datamanager.i().lnum + "turn";
        }
        else
        {
            lockonl.gameObject.SetActive(false);
        }
        if (Datamanager.i().d == true)
        {
            dotl.gameObject.SetActive(true);
            dotl.text = "Receive 2 damage at the end of turn during " + Datamanager.i().dnum + "turn";
        }
        else
        {
            dotl.gameObject.SetActive(false);
        }
        if (Datamanager.i().r == true)
        {
            refl.gameObject.SetActive(true);
            refl.text = "Reflect damage of " + Datamanager.i().rnum + "during this turn";
        }
        else
        {
            refl.gameObject.SetActive(false);
        }
        if (Datamanager.i().ins == true)
        {
            insl.gameObject.SetActive(true);
            insl.text = "Get " + Datamanager.i().insnum + "power during this turn";
        }
        else
        {
            insl.gameObject.SetActive(false);
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
        if (Datamanager.i().shd > 0)
        {
            shd.SetActive(true);
            shdl.text = Datamanager.i().shd + "";
        }
        else
        {
            shd.SetActive(false);
            shdl.text = "";
        }
        if (Datamanager.i().str != 0)
        {
            str.SetActive(true);
        }
        else
        {
            str.SetActive(false);
        }
        if (Datamanager.i().genamr == true)
        {
            gen.SetActive(true);
        }
        else
        {
            gen.SetActive(false);
        }
        if (Datamanager.i().w == true)
        {
            weak.SetActive(true);
        }
        else
        {
            weak.SetActive(false);
        }
        if (Datamanager.i().l == true)
        {
            lockon.SetActive(true);
        }
        else
        {
            lockon.SetActive(false);
        }
        if (Datamanager.i().d == true)
        {
            dot.SetActive(true);
        }
        else
        {
            dot.SetActive(false);
        }
        if (Datamanager.i().r == true)
        {
            reflect.SetActive(true);
        }
        else
        {
            reflect.SetActive(false);
        }
        if (Datamanager.i().ins == true)
        {
            ins.SetActive(true);
        }
        else
        {
            ins.SetActive(false);
        }
        icon.enabled = true;
    }
}
