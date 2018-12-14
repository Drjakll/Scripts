using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class HeavySwingLeft : Actions {

    private float MaxDamage = 150.0f;
    private float MinDamage = 100.0f;

    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        if (!aim)
        {
            if (Align(A.gameObject.transform, Target.gameObject.transform))
                aim = true;
            return false;
        }
        else if (!DestinationReached)
        {
            if (!RunToTarget(A.transform, Target.transform))
                return false;
            DestinationReached = true;
        }
        A.SetBool("PowerSwingLeft", true);
        A.SetBool("Idle", false);
        Weapon.GetComponent<Weapon>().TargetFind(Target, TargetReact, TargetBlockReact);
        return true;
    }

    public void TargetReact(Player Target)
    {
        Target.UploadStatus(new Stunned(Stuns.Hit, Target.GetComponent<Animator>()), "Stun");
        Target.GetComponent<Animator>().SetBool("GetHitRight", true);
        Target.GetComponent<CharacterHealth>().UpdateHealth(-Random.Range(MinDamage, MaxDamage));
    }

    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        return A.GetCurrentAnimatorStateInfo(1).IsName("CombatBigSwingLeft") && A.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f;
    }

    public void TargetBlockReact(Player Target)
    {
        Target.GetComponent<Player>().UploadStatus(new Stunned(Stuns.GuardAttacked, Target.GetComponent<Animator>()), "Stun");
        Target.GetComponent<Animator>().SetBool("GuardAttacked", true);
    }
}
