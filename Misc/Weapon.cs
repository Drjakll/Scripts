using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class Weapon : MonoBehaviour {
    // Use this for initialization
    Collider C;
    private float LastChecked;
    private Reactions EnchancementEffect;
    private bool Triggered;
    [SerializeField] private GameObject SlashEffectLocation;
    [SerializeField] private GameObject SlashEffect;
    void Start () {
        C = GetComponent<BoxCollider>();
        LastChecked = 0; //The last time since it finds the target. 
        EnchancementEffect = ApplyNoEffect;
        Triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public bool TargetFind(Player Target, Reactions TargetReact, Reactions TargetReactToBlock)
    {
        if (!Triggered && Target != null)
        {
            Collider[] CC = Physics.OverlapBox(C.transform.position, C.bounds.extents);
            foreach (Collider L in CC)
            {
                if(L.gameObject.name == Target.gameObject.name || (L.GetComponentInParent<Player>().name == Target.GetComponentInParent<Player>().name && L.GetComponent<Weapon>() != null))
                {
                    ApplySlashEffect(Target);
                    Triggered = true;
                    LastChecked = Time.time;
                    if (Target.GetComponent<ActionCaster>().isBlocking())
                    {
                        TargetReactToBlock.Invoke(Target);
                        return true;
                    }
                    TargetReact.Invoke(Target);
                    EnchancementEffect.Invoke(Target);
                    return true;
                }
            }
        }
        return false;
    }

    public void ApplyEnchant(Reactions Effect)
    {
        EnchancementEffect = Effect;
    }

    public void DoneDamage()
    {
        Triggered = false;
    }

    public void ApplyNoEffect(Player Target)
    {

    }

    private void ApplySlashEffect(Player Target)
    {
        GameObject BamSlash = Instantiate(SlashEffect);
        BamSlash.transform.parent = Target.transform; 
        BamSlash.transform.localPosition = new Vector3(0, 1.25f, 0);
        StartCoroutine(SlashDone(BamSlash));
    }

    private IEnumerator SlashDone(GameObject Slash)
    {
        yield return new WaitForSeconds(.7f);
        Object.Destroy(Slash);
    }
}
