using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Actions {
    private bool Cycle = false;
    
    public override bool CastAction(Animator A, GameObject Weapon, Player Target)
    {
        if (!aim)
        {
            if (Align(A.transform, Target.transform))
                aim = true;
            return true;
        }
        A.SetBool("Idle", false);
        A.SetBool("CastSpell", true);
        Vector3 InitialPosition = A.GetComponent<ImportantBodyParts>().T[0].position;

        if (A.GetCurrentAnimatorStateInfo(1).IsName("SpellCast") && A.GetCurrentAnimatorStateInfo(1).normalizedTime < .5f)
        {
            Cycle = false;
        }
        else if (A.GetCurrentAnimatorStateInfo(1).IsName("SpellCast") && A.GetCurrentAnimatorStateInfo(1).normalizedTime > .5f && !Cycle)
        {
            CharacterHealth CharHealth= A.GetComponent<CharacterHealth>();
            if (CharHealth.CurrentMana < 5.0f)
                return false;
            GameObject Obj = Instantiate(CurrentSpell);
            Obj.transform.position = InitialPosition;
            Obj.GetComponent<Spells>().Facing = A.transform.forward;
            Obj.GetComponent<Spells>().Caster = A.gameObject.name;
            Cycle = true;
            CharHealth.UpdateMana(-10.0f);
        }
        return true;
    }

    public override bool FinishCasting(Animator A, GameObject Weapon, Player Target)
    {
        return A.GetCurrentAnimatorStateInfo(1).IsName("SpellCast") && A.GetCurrentAnimatorStateInfo(1).normalizedTime > 1.0f;
    }
}
