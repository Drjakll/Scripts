using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class Idle : Actions {

    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        if (!A.GetBool("Idle"))
        {
            A.SetBool("Idle", true);
            A.SetBool("SwingLeft", false);
            A.SetBool("SwingRight", false);
            A.SetBool("PowerSwingLeft", false);
            A.SetBool("PowerSwingRight", false);
            A.SetBool("Poke", false);
            A.SetBool("Block", false);
            A.SetBool("CastSpell", false);
            A.SetBool("Charge", false);
        }
        return true;
    }

    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        return true;
    }
}
