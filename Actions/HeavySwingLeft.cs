using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

//Cast heavy swing attack, deals more damage than regular swing attacks
public class HeavySwingLeft : Actions {

    private float MaxDamage = 150.0f;
    private float MinDamage = 100.0f;

    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        //Check to see if character is facing enemy target
        if (!aim)
        {
            if (Align(A.gameObject.transform, Target.gameObject.transform))
                aim = true;
            return false;
        }//If aim is true, then it will check to see if it reaches within attacking range of its target
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

    ////Callback method that use by one of the method in Weapon.cs class
    public void TargetReact(Player Target)
    {
        Target.UploadStatus(new Stunned(Stuns.Hit, Target.GetComponent<Animator>()), "Stun");
        Target.GetComponent<Animator>().SetBool("GetHitRight", true);
        Target.GetComponent<CharacterHealth>().UpdateHealth(-Random.Range(MinDamage, MaxDamage));
    }

    //Check to see if it's done casting its animation
    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        return A.GetCurrentAnimatorStateInfo(1).IsName("CombatBigSwingLeft") && A.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f;
    }

    //Callback method that use by one of the method in Weapon.cs class
    public void TargetBlockReact(Player Target)
    {
        Target.GetComponent<Player>().UploadStatus(new Stunned(Stuns.GuardAttacked, Target.GetComponent<Animator>()), "Stun");
        Target.GetComponent<Animator>().SetBool("GuardAttacked", true);
    }
}
