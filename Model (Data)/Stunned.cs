using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class Stunned : Status {

    private Stuns KindOfStuns = Stuns.NotStunned;

    public Stunned(Stuns KOS, Animator A)
    {
        KindOfStuns = KOS;
        A.SetBool("GetHitLeft", false);
        A.SetBool("GetHitRight", false);
        A.SetBool("GuardAttacked", false);
        A.SetBool("GuardDestroyed", false);
        A.SetBool("KnockBack", false);
    }

    public override float CurrentStatus(Animator A, MoveXY MXY)
    {
        float CurrentAnimatedTime = A.GetCurrentAnimatorStateInfo(2).normalizedTime;
        switch (KindOfStuns)
        {
            case Stuns.GuardAttacked:
                if(A.GetCurrentAnimatorStateInfo(2).IsName("GuardAttacked") && CurrentAnimatedTime <= 1.0f)
                {
                    MXY.Move(0, 0, 0);
                }
                else if (A.GetCurrentAnimatorStateInfo(2).IsName("GuardAttacked"))
                {
                    A.SetBool("GuardAttacked", false);
                    return 0; 
                }
                break;
            case Stuns.Hit:
                if ((A.GetCurrentAnimatorStateInfo(2).IsName("GetHitLeft") || A.GetCurrentAnimatorStateInfo(2).IsName("GetHitRight")) && CurrentAnimatedTime <= 1.0f)
                {
                    MXY.Move(0, 0, 0);
                }
                else if (A.GetCurrentAnimatorStateInfo(2).IsName("GetHitLeft") || A.GetCurrentAnimatorStateInfo(2).IsName("GetHitRight"))
                {
                    A.SetBool("GetHitLeft", false);
                    A.SetBool("GetHitRight", false);
                    return 0;
                }
                break;
            case Stuns.GuardDestroyed:
                if (A.GetCurrentAnimatorStateInfo(2).IsName("GuardDestroy") && CurrentAnimatedTime <= 1.0f)
                {
                    MXY.Move(0, 0, 0);
                }
                else if(A.GetCurrentAnimatorStateInfo(2).IsName("GuardDestroy"))
                {
                    A.SetBool("GuardDestroyed", false);
                    return 0;
                }
                break;
            case Stuns.KnockBack:
                if(A.GetCurrentAnimatorStateInfo(2).IsName("KnockBack") && CurrentAnimatedTime <= 1.0f)
                {
                    MXY.Move(0, 0, 0);
                }
                else if (A.GetCurrentAnimatorStateInfo(2).IsName("KnockBack"))
                {
                    A.SetBool("KnockBack", false);
                    return 0;
                }
                break;
            default:
                return 0;
        }
        return 100.0f;
    }
}
