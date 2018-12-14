using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class Poke : Actions {

    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        A.SetBool("Poke", true);
        A.SetBool("Idle", false);
        return true;
    }

    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        return A.GetCurrentAnimatorStateInfo(1).IsName("PokeStraight") && A.GetCurrentAnimatorStateInfo(1).normalizedTime > 1.0f;
    }
}
