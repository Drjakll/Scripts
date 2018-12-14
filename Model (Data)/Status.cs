using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour {
    //Return an integer that represent that status: 0 = everything ok, 1 = stunned
    public GameObject[] VisualEffects;
    public virtual void Init(Transform T) { }
    public virtual float CurrentStatus(Animator A, MoveXY MXY) { return 0; }
    public virtual void EffectIntensify(float AddDuration) { }
}
