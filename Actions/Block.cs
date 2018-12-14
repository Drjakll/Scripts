using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class Block : Actions {

    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        if (!Align(A.gameObject.transform, Target.gameObject.transform))
            return false;

        A.SetBool("Block", true);
        A.SetBool("Idle", false);
        return true;
    }

    public override bool Blocking(Animator A, GameObject Weapon)
    {
        foreach(KeyValuePair<string, Status> S in A.GetComponent<Player>().GetStatusList())
        {
            if(S.Value.CurrentStatus(A, A.GetComponent<MoveXY>()) == 100.0f)
            {
                return false;
            }
        }
        return true;
    }

    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        return false;
    }
}
