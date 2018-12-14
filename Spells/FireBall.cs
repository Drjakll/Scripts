using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Functions;

public class FireBall : Spells {
    public float InitialVelocity = 0;
    public float Acceleration = 9.8f;
    public float ExplosionAcceleration = 19.6f;
    private float StartingTime;
    private float Velocity;
    private bool Collide = false;
    private bool Shrink = false;
    private float ColliderRadius;
    private float minDmg = 25.0f;
    private float maxDmg = 50.0f;
	// Use this for initialization
	void Start () {
        StartingTime = Time.time;
        ColliderRadius = GetComponent<Collider>().bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {

        if (!Collide)
        {
            Collider[] CC = Physics.OverlapSphere(transform.position, ColliderRadius);
            foreach (Collider C in CC)
            {
                if (C.gameObject.name != Caster && C.gameObject.name != gameObject.name)
                {
                    StartingTime = Time.time;
                    Collide = true;
                    Animator A = C.GetComponentInParent<Animator>();
                    if (A != null)
                    {
                        A.GetComponent<CharacterHealth>().UpdateHealth(-Random.Range(minDmg, maxDmg));
                        ActionCaster AC = A.GetComponent<ActionCaster>();
                        if (AC != null && AC.isBlocking())
                        {
                            A.GetComponent<Player>().UploadStatus(new Stunned(Stuns.GuardDestroyed, A), "Stun");
                            A.SetBool("GuardDestroyed", true);
                            return;
                        }
                        A.GetComponent<Player>().UploadStatus(new Stunned(Stuns.KnockBack, A), "Stun");
                        A.SetBool("KnockBack", true);
                    }
                }
            }
            Velocity = Acceleration * (Time.time - StartingTime) + InitialVelocity;
            Vector3 components = new Vector3(Facing.x * Velocity, 0, Facing.z * Velocity);
            transform.Translate(components * Time.deltaTime, Space.World);
        }
        else
        {
            Velocity = ExplosionAcceleration * (Time.time - StartingTime);
            Vector3 components = new Vector3(Velocity, Velocity, Velocity);
            if (Shrink)
            {
                transform.localScale = transform.localScale - components * (Time.deltaTime / 16);
                if(transform.localScale.y < 0.25f)
                {
                    Object.Destroy(this.gameObject);
                }
                return;
            }
            transform.localScale = transform.localScale  + components * Time.deltaTime;
            if(transform.localScale.magnitude > 2.0f)
            {
                Shrink = true;
                StartingTime = Time.time;
            }
        }
	}
}
