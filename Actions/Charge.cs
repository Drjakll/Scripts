using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

//One of the abilities that allows the character to run to enemy target at high speed
public class Charge : Actions {
    private float MaxDamage = 100.0f;
    private float MinDamage = 60.0f;
    private Vector3 TargetLocation;
    private bool AttackPhase = false;
    private bool TargetFound = false;

    public override void Init()
    {
        aim = false;
        RotateSpeed = 200.0f;
        AttackPhase = false;
        TargetFound = false;
    }

    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        if (A.GetComponent<CharacterHealth>().CurrentMana < 20.0f) //It utilizes mana, if not enough mana, return false and it will not cast
            return false;
        A.SetBool("Idle", false);
        GameObject Avatar = A.gameObject;
        GameObject EAvatar = Target.gameObject;
        
        //Notice that I didn't use the base class align search method because this ability is sort of unique
        if (AttackPhase && !TargetFound)
        {
            Collider[] DetectTarget = Physics.OverlapSphere(A.gameObject.transform.position, 3.0f);

            foreach(Collider C in DetectTarget)
            {
                if(C.GetComponent<Player>() != null)
                {
                    TargetFound = true;
                    A.SetBool("PowerSwingRight", true);
                }
            }
            A.SetBool("Charge", false);
            TargetFound = true;
            A.GetComponent<CharacterHealth>().UpdateMana(-20.0f);
        }
        else if (!aim)
        {
            if(Align(Avatar.transform, EAvatar.transform)){
                aim = true;
                TargetLocation = EAvatar.transform.position;
            }
        }
        else if(aim && !AttackPhase)
        {
            A.SetBool("Charge", true);
            A.Play("Charge", 0);
            if ((Avatar.transform.position - TargetLocation).magnitude < 2.0f)
            {
                AttackPhase = true;
                return true;
            }
            Avatar.transform.Translate(A.transform.forward * 100.0f * Time.deltaTime, Space.World);
        }
       
        return true;
    }

    //Check to see if animation finish casting
    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        if(TargetFound && (A.GetCurrentAnimatorStateInfo(1).IsName("CombatBigSwingRight") || A.GetCurrentAnimatorStateInfo(0).IsName("StopRunning")))
        {
            if (A.GetCurrentAnimatorStateInfo(1).normalizedTime >= .95f)
            {
                return true;
            }
            Weapon.GetComponent<Weapon>().TargetFind(Target, React, TargetBlockReact);
            A.SetBool("Charge", false);
            A.transform.Translate(A.transform.forward * 0.25f * Time.deltaTime);
        }
        return false;
    }

    //Callback method that use by one of the method in Weapon.cs class
    public void React(Player Target)
    {
        Target.UploadStatus(new Stunned(Stuns.Hit, Target.GetComponent<Animator>()), "Stun");
        Target.GetComponent<Animator>().SetBool("GetHitLeft", true);
        Target.GetComponent<CharacterHealth>().UpdateHealth(-Random.Range(MinDamage, MaxDamage));
    }

    //Callback method that use by one of the method in Weapon.cs class
    public void TargetBlockReact(Player Target)
    {
        Target.GetComponent<Player>().UploadStatus(new Stunned(Stuns.GuardAttacked, Target.GetComponent<Animator>()), "Stun");
        Target.GetComponent<Animator>().SetBool("GuardAttacked", true);
    }
}
