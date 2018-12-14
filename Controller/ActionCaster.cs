using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// This class take charge of casting attacks
/// </summary>
public class ActionCaster : MonoBehaviour {
    private Animator A;
    private Actions Act;
    private Weapon weapon;
    private GameObject EnchantVisual;
    private Player Target = null;
    private bool OnOff = false;
    private CharacterHealth TargetHealth = null;
    // Use this for initialization
    void Start () {
        A = GetComponent<Animator>(); 
        weapon = GetComponentInChildren<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Target != null)
            Act.CastAction(A, weapon.gameObject, Target);
	}

    public bool isBlocking()
    {
        return Act.Blocking(A, weapon.gameObject);
    }

    public void GiveCommand(Actions s)
    {
        s.Init();
        Act = s;
    }

    public void SetTarget(Player Enemy, CharacterHealth CharHealth)
    {
        TargetHealth = CharHealth;
        Target = Enemy;
    }

    public bool IsFinishCasting()
    {
        if (Target == null)
            return true;
        bool Done = Act.FinishCasting(A, weapon.gameObject, Target);
        if (Done)
            weapon.DoneDamage();
        return Done;
    }

    public void EnchantWeapon(Status Effect, string Type)
    {
        if (!OnOff)
        {
            EnchantVisual = Instantiate(Effect.VisualEffects[0]);
            EnchantVisual.transform.SetParent(weapon.transform);
            EnchantVisual.transform.localPosition = new Vector3(0, -.1f, 0);
            EnchantVisual.transform.localEulerAngles = Vector3.zero;
            OnOff = true;
            weapon.ApplyEnchant(delegate (Player target) {
                target.UploadStatus(Effect, Type);
            });
        }
        else
        {
            GameObject.Destroy(EnchantVisual);
            weapon.ApplyEnchant(weapon.ApplyNoEffect);
            OnOff = false;
        }
    }

    public bool IsWeaponEnchanted()
    {
        return OnOff;
    }

    public Player GetTarget()
    {
        return Target;
    }
}
